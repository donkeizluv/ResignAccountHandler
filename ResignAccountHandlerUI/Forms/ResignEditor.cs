using System;
using System.Windows.Forms;

namespace ResignAccountHandlerUI.Forms
{
    public partial class ResignEditor : Form
    {
        private Resignation _currentResign;

        private Resignation CurrentResign
        {
            get => _currentResign;
            set
            {
                _currentResign = value;
                textBoxAd.Text = value.ADName;
                textBoxHrCode.Text = value.HRCode;
                dateTimePickerReceiveDay.Value = value.ReceiveDate;
                dateTimePickerResignDay.Value = value.ResignDay;
                textBoxError.Text = value.ErrorMessage;
                comboBoxStatus.SelectedItem = value.Status.ToString();
            }
        }

        private ResignEditor(bool dummy)
        {
            //Parent = parent;
            InitializeComponent();
            //dateTimePickerReceiveDay.Value = DateTime.Today;
            //dateTimePickerResignDay.Value = DateTime.Today;
            PopulateStatusComboBox(comboBoxStatus.Items);
            comboBoxStatus.SelectedIndex = 0;
            DialogResult = DialogResult.None;
        }

        //insert
        public ResignEditor() : this(true)
        {
        }

        public ResignEditor(string caption) : this(true)
        {
            Text = caption;
        }

        //edit
        public ResignEditor(Resignation resign, string caption) : this(true)
        {
            CurrentResign = resign;
            Text = caption;
        }

        private void PopulateStatusComboBox(ComboBox.ObjectCollection collection)
        {
            var values = Enum.GetValues(typeof(RecordStatus));
            foreach (var value in values)
            {
                collection.Add(value.ToString());
            }
        }

        private bool VadidateNewRecordFields(out string error)
        {
            error = string.Empty;
            if (textBoxAd.Text.Length < 1)
            {
                error = "Missing AD.";
                return false;
            }
            if (textBoxHrCode.Text.Length < 1)
            {
                error = "Missing HR Code.";
                return false;
            }

            return true;
        }

        private bool VadidateUpdateFields(out string error)
        {
            error = string.Empty;
            return true;
        }

        public bool GetResign(out Resignation resign, bool update = false)
        {
            ShowDialog(Parent);
            if (VadidateNewRecordFields(out var error))
            {
                resign = new Resignation()
                {
                    ADName = textBoxAd.Text,
                    HRCode = textBoxHrCode.Text,
                    ReceiveDate = dateTimePickerReceiveDay.Value,
                    ResignDay = dateTimePickerResignDay.Value,
                    Status = (RecordStatus)Enum.Parse(typeof(RecordStatus), comboBoxStatus.SelectedItem.ToString()),
                };
                if (update)
                    resign.Id = _currentResign.Id;
                resign.SetErrorMessage(textBoxError.Text);
                //_currentResign = resign; //????
                return true;
            }
            else
            {
                if (DialogResult == DialogResult.OK)
                    MessageBox.Show(error);
                DialogResult = DialogResult.None;
                resign = null;
                return false;
            }
        }

        //bassicly same with create
        //public Resignation UpdateResign(out string errorMess)
        //{
        //    ShowDialog(Parent);
        //    errorMess = string.Empty;
        //    if (VadidateUpdateFields(out var error))
        //    {
        //        errorMess = error;
        //        var resign = new Resignation()
        //        {
        //            ADName = textBoxAd.Text,
        //            HRCode = textBoxHrCode.Text,
        //            ReceiveDate = dateTimePickerReceiveDay.Value,
        //            ResignDay = dateTimePickerResignDay.Value,
        //            Status = (RecordStatus)Enum.Parse(typeof(RecordStatus), comboBoxStatus.SelectedItem.ToString()),
        //        };
        //        resign.SetErrorMessage("lol");
        //        return resign;
        //    }
        //    else
        //    {
        //        if (DialogResult == DialogResult.OK)
        //            MessageBox.Show(error);
        //        DialogResult = DialogResult.None;
        //        return null;
        //    }
        //}

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormInsert_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                buttonCancel_Click(this, EventArgs.Empty);
                Close();
            }
        }
    }
}