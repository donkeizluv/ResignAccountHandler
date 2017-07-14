using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ResignAccountHandlerUI.Forms
{
    public partial class FormCommitLog : Form
    {
        public FormCommitLog()
        {
            InitializeComponent();
        }

        private void FormCommitLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void ClearGrid()
        {
            commitLogdataGridView.Rows.Clear();
        }

        public DataGridViewRow AddToCommitLog(Resignation resign, string commitResult = null, Color? bgColor = null)
        {
            var row = (DataGridViewRow)commitLogdataGridView.RowTemplate.Clone();

            var itemArray = resign.ToArray(false).ToList();
            itemArray.Add(commitResult ?? string.Empty);

            row.CreateCells(commitLogdataGridView, itemArray.ToArray());
            if (bgColor != null)
                row.DefaultCellStyle.BackColor = bgColor ?? Color.Empty;
            commitLogdataGridView.Rows.Add(row);
            return row;
        }
    }
}