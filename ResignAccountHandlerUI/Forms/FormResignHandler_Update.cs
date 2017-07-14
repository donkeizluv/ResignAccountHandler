using ResignAccountHandlerUI.UIController;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ResignAccountHandlerUI.Forms
{
    public partial class FormResignHandler : Form
    {
        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            ShowLoadingDialog(this);
            Controller.DoUpdate();
            HideLoadingDialog();
        }

        private void SetUpdateStatusLabels(int total, int resignCount)
        {
            labelTotalEmails.Text = total.ToString();
            labelResignEmailsCount.Text = resignCount.ToString();
        }

        public DataGridViewRow AddRecordToUpdateGrid(string subject, DateTime receive, string result, Code c, Color? color)
        {
            var row = (DataGridViewRow)dataGridViewUpdate.RowTemplate.Clone();
            row.CreateCells(dataGridViewUpdate, new string[] { subject, receive.ToString("dd/MM/yyyy hh:mm:ss tt"), result, c.ToString() });
            row.DefaultCellStyle.BackColor = color ?? Color.White;
            dataGridViewUpdate.Rows.Add(row);
            return row;
        }
    }
}