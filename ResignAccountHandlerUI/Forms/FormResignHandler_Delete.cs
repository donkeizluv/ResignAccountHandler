using System;
using System.Drawing;
using System.Windows.Forms;
using ResignAccountHandlerUI.Model;

namespace ResignAccountHandlerUI.Forms
{
    public partial class FormResignHandler : Form
    {
        private void ButtonFillDeleteGrid_Click(object sender, EventArgs e)
        {
            ShowLoadingDialog(this);
            Controller.DisplayTodayDeletes();
            buttonExecuteDelete.Enabled = Controller.DeleteList.Count > 0;
            HideLoadingDialog();
        }

        public DataGridViewRow AddToDeleteGrid(Resignation resign, string result = null, Color? bgColor = null)
        {
            var row = (DataGridViewRow)dataGridViewDelete.RowTemplate.Clone();
            row.CreateCells(dataGridViewDelete, resign.ToArray());
            if (bgColor != null)
                row.DefaultCellStyle.BackColor = bgColor ?? Color.Empty;
            if (!string.IsNullOrEmpty(result))
                row.Cells[row.Cells.Count - 1].Value = result;
            dataGridViewDelete.Rows.Add(row);
            return row;
        }

        private void ButtonActionDelete_Click(object sender, EventArgs e)
        {
            if (ShowConfirmMsg($"Do you want to delete {Controller.DeleteList.Count} accounts?") != DialogResult.Yes)
                return;
            Controller.DeleteAccounts();
            buttonExecuteDelete.Enabled = false;
        }
    }
}