using ResignAccountHandlerUI.UIController;
using System;
using System.Windows.Forms;
using ResignAccountHandlerUI.ResignExtractor;

namespace ResignAccountHandlerUI.Forms
{
    public partial class FormResignHandler : Form, IResignAccountHanlderViewer
    {
        //private enum RowState
        //{
        //    Normal,
        //    New,
        //    Update,
        //    Delete
        //}

        private LoadingDialog _loadingDialog = new LoadingDialog();
        //private IDbAdapter _adapter = new DbAdapter($@"{AssemblyDirectory}\db.dat");
        //private IExecutioner _executioner;

        public IResignAccountHanlderController Controller { get; set; }

        private DataGridViewRow SelectedQueryRow
        {
            get
            {
                if (dataGridViewQuery.SelectedCells.Count < 1)
                    return null;
                return dataGridViewQuery.Rows[dataGridViewQuery.SelectedCells[0].RowIndex];
            }
        }

        public DataGridView DataGridView_Update
        {
            get
            {
                return dataGridViewUpdate;
            }
        }

        public DataGridView DataGridView_Disable
        {
            get
            {
                return dataGridViewDisable;
            }
        }

        public DataGridView DataGridView_Delete
        {
            get
            {
                return dataGridViewDelete;
            }
        }

        public DataGridView DataGridView_Query
        {
            get
            {
                return dataGridViewQuery;
            }
        }

        public FormResignHandler()
        {
            //login & create Executioner
            //_executioner = new Executioner("blabla", "blabla");
            InitializeComponent();
            Text = $"{Text} - {DateTime.Today.ToShortDateString()}";
            PopulateStatusComboBox(statusQueryComboBox.Items);
            statusQueryComboBox.SelectedIndex = 0;
            //_formCommit = new FormCommitLog();
            //PopulateStatusComboBox(dataGridViewStatusColumn.Items);
        }

        //private void SetStatus(string s)
        //{
        //    statusLabel.Text = s;
        //}
        private void PopulateStatusComboBox(ComboBox.ObjectCollection collection)
        {
            var values = Enum.GetValues(typeof(RecordStatus));
            foreach (var value in values)
            {
                collection.Add(value.ToString());
            }
        }

        private void PopulateStatusComboBox(DataGridViewComboBoxCell.ObjectCollection collection)
        {
            var values = Enum.GetValues(typeof(RecordStatus));
            foreach (var value in values)
            {
                collection.Add(value.ToString());
            }
        }

        /// <summary>
        /// show simple msgbox
        /// </summary>
        /// <param name="s"></param>
        private void ShowMsgBox(string s)
        {
            MessageBox.Show(s);
        }

        /// <summary>
        /// show loading dialog & lock parent
        /// </summary>
        /// <param name="parent"></param>
        private void ShowLoadingDialog(Form parent)
        {
            _loadingDialog.DisableParent = parent;
            _loadingDialog.CalculateCenterPosition();
            _loadingDialog.Show();
            parent.Enabled = false;
        }

        /// <summary>
        /// hide loading & release parent
        /// </summary>
        private void HideLoadingDialog()
        {
            _loadingDialog.DisableParent.TopMost = true; //work around for parent form lose focus on hiding loading form
            _loadingDialog.Hide();
            _loadingDialog.DisableParent.Enabled = true;
            _loadingDialog.DisableParent.TopMost = false;
        }

        private DialogResult ShowConfirmMsg(string message)
        {
            return MessageBox.Show(message,
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }

    }
}