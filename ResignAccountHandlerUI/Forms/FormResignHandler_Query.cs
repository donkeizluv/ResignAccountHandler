using ResignAccountHandlerUI.UIController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ResignAccountHandlerUI.Model;

namespace ResignAccountHandlerUI.Forms
{
    public partial class FormResignHandler : Form
    {
        public Color NormalRow { get; set; } = Color.Empty;
        public Color UpdateRowColor { get; set; } = Color.Cyan;
        public Color DeleteCellForeColor { get; set; } = Color.LightSalmon;
        public Color NewRowColor { get; set; } = Color.LightGreen;
        private FormCommitLog _formCommit = new FormCommitLog();

        private void ButtonFillQueryGrid_Click(object sender, EventArgs e)
        {
            if (Controller.CommandList.Count > 0 && MessageBox.Show($"There are {Controller.CommandList.Count} not commited changes, revert?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
            {
                return;
            }
            ClearUncommitedChanges();
            ShowLoadingDialog(this);
            if (radioButtonFilterStatus.Checked)
            {
                Controller.DisplayQuery((RecordStatus)Enum.Parse(typeof(RecordStatus), statusQueryComboBox.SelectedItem.ToString()));
            }
            else
            {
                Controller.DisplayQuery(textBoxFilterAd.Text);
            }
            HideLoadingDialog();
        }

        private void ClearUncommitedChanges()
        {
            Controller.CommandList.Clear();
            _rowResignationDictionary.Clear();
            _dirtyIndex.Clear();
        }

        private Dictionary<DataGridViewRow, Resignation> _rowResignationDictionary = new Dictionary<DataGridViewRow, Resignation>();
        private List<int> _dirtyIndex = new List<int>();

        private void UpdateQueryGridRow(DataGridViewRow row, Resignation resign)
        {
            row.SetValues(resign.ToArray(false));
            _rowResignationDictionary[row] = resign; //update row
        }

        public DataGridViewRow AddToQueryGrid(Resignation resign, string result = null, Color? bgColor = null)
        {
            var row = (DataGridViewRow)dataGridViewQuery.RowTemplate.Clone();
            row.CreateCells(dataGridViewQuery, resign.ToArray(false));
            if (bgColor != null)
                row.DefaultCellStyle.BackColor = bgColor ?? Color.Empty;
            if (!string.IsNullOrEmpty(result))
                SetTooltip(row.Cells, result);
            dataGridViewQuery.Rows.Add(row);
            _rowResignationDictionary.Add(row, resign);
            return row;
        }

        public DataGridViewRow AddToCommitLog(Resignation resign, string commitResult = null, Color? bgColor = null)
        {
            return _formCommit.AddToCommitLog(resign, commitResult, bgColor);
        }

        private void SetTooltip(DataGridViewCellCollection cellCollection, string tooltip)
        {
            foreach (var item in cellCollection)
            {
                var cell = (DataGridViewCell)item;
                cell.ToolTipText = tooltip;
            }
        }

        private void ButtonCommitQueryGrid_Click(object sender, EventArgs e)
        {
            if (Controller.CommandList.Count < 1)
            {
                ShowMsgBox("Make some changes first!");
                return;
            }
            if (!ConfirmCommit()) return;
            int success = Controller.CommitChanges();
            Controller.CommandList.Clear();
            ForceRefetch();
            //ShowMsgBox($"{success} success");
            ButtonFillQueryGrid_Click(this, EventArgs.Empty); //reload
            _formCommit.Show();
        }

        /// <summary>
        /// disable execute buttons
        /// </summary>
        private void ForceRefetch()
        {
            buttonExecuteDelete.Enabled = false;
            buttonExecuteDisable.Enabled = false;
        }

        /// <summary>
        /// show confirm dialog on commiting changes
        /// </summary>
        /// <returns></returns>
        private bool ConfirmCommit()
        {
            var sumDict = GetCommitSummary();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Are you sure to commit:");
            foreach (var commitType in sumDict)
            {
                stringBuilder.Append($"\n{commitType.Key.ToString()}: {commitType.Value}");
            }
            return MessageBox.Show(stringBuilder.ToString(), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
        }

        private Dictionary<DbAction, int> GetCommitSummary()
        {
            var sumDict = new Dictionary<DbAction, int>();
            foreach (var action in Enum.GetValues(typeof(DbAction)))
            {
                sumDict.Add((DbAction)action, 0);
            }
            foreach (var cmd in Controller.CommandList)
            {
                sumDict[cmd.ActionType]++;
            }
            return sumDict;
        }

        //private static IEnumerable<T> GetValues<T>()
        //{
        //    return Enum.GetValues(typeof(T)).Cast<T>();
        //}
        /// <summary>
        /// return row index related to GridView
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private DataGridViewRow GetRowOfCell(DataGridViewCell cell)
        {
            return cell.DataGridView.Rows[cell.RowIndex];
        }

        private void DataGridViewQuery_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Delete)
            //{
            //}
        }

        //edit hotkey
        private void DataGridViewQuery_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ButtonDeleteRecord_Click(this, EventArgs.Empty);
            }
            if (e.KeyCode == Keys.F2)
            {
                ButtonUpdateRecord_Click(this, EventArgs.Empty);
            }
        }

        //insert new record
        private void ButtonInsertNewRecord_Click(object sender, EventArgs e)
        {
            var insertForm = new ResignEditor("Insert");
            if (!insertForm.GetResign(out var resign))
                return;
            if (CheckDuplicate(resign.ADName, out var checkDup))
            {
                if (!ShowWarning()) return;
            }
            var newRecord = AddToQueryGrid(resign, "New record", NewRowColor);
            Controller.CommandList.Add(new Command() { ActionType = DbAction.Insert, Resign = resign });
            _dirtyIndex.Add(newRecord.Index);
        }

        //update selected record
        private void ButtonUpdateRecord_Click(object sender, EventArgs e)
        {            var selectedRow = SelectedQueryRow;
            if (selectedRow == null) return;
            if (IsRowDirty(selectedRow))
            {
                //no delete dirt row
                ShowMsgBox("Cant delete not commited changes");
                return;
            }
            var editForm = new ResignEditor(_rowResignationDictionary[selectedRow], "Edit");
            if (!editForm.GetResign(out var resign, true))
                return;
            if (CheckDuplicate(resign.ADName, out var checkDup) &&
                _rowResignationDictionary[selectedRow].ADName != resign.ADName &&
                _rowResignationDictionary[selectedRow].HRCode != resign.HRCode)
            {
                if (!ShowWarning()) return;
            }
            //add to controller
            Controller.CommandList.Add(new Command() { ActionType = DbAction.Update, Resign = resign });
            UpdateQueryGridRow(selectedRow, resign);
            _dirtyIndex.Add(selectedRow.Index);
            selectedRow.DefaultCellStyle.BackColor = UpdateRowColor;
        }

        //delete record
        private void ButtonDeleteRecord_Click(object sender, EventArgs e)
        {
            var selectedRow = SelectedQueryRow;
            if (selectedRow == null) return;
            if (IsRowDirty(selectedRow))
            {
                //no delete dirt row
                ShowMsgBox("Cant delete not commited changes");
                return;
            }
            selectedRow.DefaultCellStyle.BackColor = DeleteCellForeColor;
            selectedRow.DefaultCellStyle.SelectionBackColor = DeleteCellForeColor;
            Controller.CommandList.Add(new Command() { ActionType = DbAction.Delete, Resign = _rowResignationDictionary[selectedRow] });
            _dirtyIndex.Add(selectedRow.Index);
        }

        /// <summary>
        /// show duplicate record warning
        /// </summary>
        /// <returns></returns>
        private bool ShowWarning()
        {
            return MessageBox.Show("AD and HR Code is already exist still want to change?", "Info",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        private bool CheckDuplicate(string ad, out IEnumerable<Resignation> resigns)
        {
            return Controller.Find(ad, out resigns);
        }

        private bool IsRowDirty(DataGridViewRow row)
        {
            return _dirtyIndex.Contains(row.Index);
        }
        //tick ad radio
        private void TextBoxFilterAd_TextChanged(object sender, EventArgs e)
        {
            radioButtonFilterAd.Checked = true;
        }
    }
}