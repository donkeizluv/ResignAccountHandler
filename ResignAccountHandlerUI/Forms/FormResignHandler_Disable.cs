using System;
using System.Drawing;
using System.Windows.Forms;
using ResignAccountHandlerUI.Model;

namespace ResignAccountHandlerUI.Forms
{
    public partial class FormResignHandler : Form
    {
        private void ButtonFillDisableGrid_Click(object sender, EventArgs e)
        {
            ShowLoadingDialog(this);
            Controller.DisplayTodayDisables();
            buttonExecuteDisable.Enabled = Controller.DisableList.Count > 0;
            HideLoadingDialog();
        }

        public DataGridViewRow AddToDisableGrid(Resignation resign, string result = null, Color? bgColor = null)
        {
            var row = (DataGridViewRow)dataGridViewDisable.RowTemplate.Clone();
            row.CreateCells(dataGridViewDisable, resign.ToArray());
            if (bgColor != null)
                row.DefaultCellStyle.BackColor = bgColor ?? Color.Empty;
            if (!string.IsNullOrEmpty(result))
                row.Cells[row.Cells.Count - 1].Value = result; //last col is db action result
            dataGridViewDisable.Rows.Add(row);
            return row;
        }

        private void ButtonExecuteDisable_Click(object sender, EventArgs e)
        {
            if (ShowConfirmMsg($"Do you want to disable {Controller.DisableList.Count} accounts?") != DialogResult.Yes)
                return;
            Controller.DisableAccounts();
            buttonExecuteDisable.Enabled = false;
        }
    }
}