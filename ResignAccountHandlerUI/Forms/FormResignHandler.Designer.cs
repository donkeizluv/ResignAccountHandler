namespace ResignAccountHandlerUI.Forms
{
    partial class FormResignHandler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageUpdate = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelResignEmailsCount = new System.Windows.Forms.Label();
            this.labelTotalEmails = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonUpdateOl = new System.Windows.Forms.Button();
            this.dataGridViewUpdate = new System.Windows.Forms.DataGridView();
            this.subjectColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receiveDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeColmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageDisable = new System.Windows.Forms.TabPage();
            this.dataGridViewDisable = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receiveDayColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionResultColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonExecuteDisable = new System.Windows.Forms.Button();
            this.buttonFillDisableGrid = new System.Windows.Forms.Button();
            this.tabPageDelete = new System.Windows.Forms.TabPage();
            this.dataGridViewDelete = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionResultColumnDelete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonExecuteDelete = new System.Windows.Forms.Button();
            this.buttonFetchDeleteGrid = new System.Windows.Forms.Button();
            this.tabPageQuery = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButtonFilterStatus = new System.Windows.Forms.RadioButton();
            this.radioButtonFilterAd = new System.Windows.Forms.RadioButton();
            this.textBoxFilterAd = new System.Windows.Forms.TextBox();
            this.buttonDeleteRecord = new System.Windows.Forms.Button();
            this.buttonUpdateRecord = new System.Windows.Forms.Button();
            this.buttonInsertNewRecord = new System.Windows.Forms.Button();
            this.buttonCommitQueryGrid = new System.Windows.Forms.Button();
            this.statusQueryComboBox = new System.Windows.Forms.ComboBox();
            this.buttonFillQueryGrid = new System.Windows.Forms.Button();
            this.dataGridViewQuery = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnManager = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erorrMessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPageUpdate.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUpdate)).BeginInit();
            this.tabPageDisable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDisable)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabPageDelete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDelete)).BeginInit();
            this.panel4.SuspendLayout();
            this.tabPageQuery.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQuery)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageUpdate);
            this.tabControl1.Controls.Add(this.tabPageDisable);
            this.tabControl1.Controls.Add(this.tabPageDelete);
            this.tabControl1.Controls.Add(this.tabPageQuery);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(754, 580);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPageUpdate
            // 
            this.tabPageUpdate.Controls.Add(this.panel1);
            this.tabPageUpdate.Controls.Add(this.dataGridViewUpdate);
            this.tabPageUpdate.Location = new System.Drawing.Point(4, 22);
            this.tabPageUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageUpdate.Name = "tabPageUpdate";
            this.tabPageUpdate.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageUpdate.Size = new System.Drawing.Size(746, 554);
            this.tabPageUpdate.TabIndex = 0;
            this.tabPageUpdate.Text = "Update";
            this.tabPageUpdate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelResignEmailsCount);
            this.panel1.Controls.Add(this.labelTotalEmails);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonUpdateOl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(742, 41);
            this.panel1.TabIndex = 1;
            // 
            // labelResignEmailsCount
            // 
            this.labelResignEmailsCount.AutoSize = true;
            this.labelResignEmailsCount.Location = new System.Drawing.Point(364, 14);
            this.labelResignEmailsCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelResignEmailsCount.Name = "labelResignEmailsCount";
            this.labelResignEmailsCount.Size = new System.Drawing.Size(13, 13);
            this.labelResignEmailsCount.TabIndex = 4;
            this.labelResignEmailsCount.Text = "0";
            // 
            // labelTotalEmails
            // 
            this.labelTotalEmails.AutoSize = true;
            this.labelTotalEmails.Location = new System.Drawing.Point(154, 14);
            this.labelTotalEmails.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTotalEmails.Name = "labelTotalEmails";
            this.labelTotalEmails.Size = new System.Drawing.Size(13, 13);
            this.labelTotalEmails.TabIndex = 3;
            this.labelTotalEmails.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Resign emails:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total emails:";
            // 
            // buttonUpdateOl
            // 
            this.buttonUpdateOl.Location = new System.Drawing.Point(4, 11);
            this.buttonUpdateOl.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUpdateOl.Name = "buttonUpdateOl";
            this.buttonUpdateOl.Size = new System.Drawing.Size(56, 19);
            this.buttonUpdateOl.TabIndex = 0;
            this.buttonUpdateOl.Text = "Update";
            this.buttonUpdateOl.UseVisualStyleBackColor = true;
            this.buttonUpdateOl.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // dataGridViewUpdate
            // 
            this.dataGridViewUpdate.AllowUserToAddRows = false;
            this.dataGridViewUpdate.AllowUserToDeleteRows = false;
            this.dataGridViewUpdate.AllowUserToResizeRows = false;
            this.dataGridViewUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewUpdate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUpdate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.subjectColumn,
            this.receiveDateColumn,
            this.resultColumn,
            this.codeColmn});
            this.dataGridViewUpdate.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewUpdate.Location = new System.Drawing.Point(2, 49);
            this.dataGridViewUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewUpdate.Name = "dataGridViewUpdate";
            this.dataGridViewUpdate.ReadOnly = true;
            this.dataGridViewUpdate.RowHeadersVisible = false;
            this.dataGridViewUpdate.RowTemplate.Height = 24;
            this.dataGridViewUpdate.ShowEditingIcon = false;
            this.dataGridViewUpdate.Size = new System.Drawing.Size(744, 505);
            this.dataGridViewUpdate.TabIndex = 0;
            // 
            // subjectColumn
            // 
            this.subjectColumn.HeaderText = "Subject";
            this.subjectColumn.Name = "subjectColumn";
            this.subjectColumn.ReadOnly = true;
            this.subjectColumn.Width = 300;
            // 
            // receiveDateColumn
            // 
            this.receiveDateColumn.HeaderText = "Receive";
            this.receiveDateColumn.Name = "receiveDateColumn";
            this.receiveDateColumn.ReadOnly = true;
            // 
            // resultColumn
            // 
            this.resultColumn.HeaderText = "Result";
            this.resultColumn.Name = "resultColumn";
            this.resultColumn.ReadOnly = true;
            this.resultColumn.Width = 250;
            // 
            // codeColmn
            // 
            this.codeColmn.HeaderText = "Code";
            this.codeColmn.Name = "codeColmn";
            this.codeColmn.ReadOnly = true;
            // 
            // tabPageDisable
            // 
            this.tabPageDisable.Controls.Add(this.dataGridViewDisable);
            this.tabPageDisable.Controls.Add(this.panel3);
            this.tabPageDisable.Location = new System.Drawing.Point(4, 22);
            this.tabPageDisable.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageDisable.Name = "tabPageDisable";
            this.tabPageDisable.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageDisable.Size = new System.Drawing.Size(746, 554);
            this.tabPageDisable.TabIndex = 1;
            this.tabPageDisable.Text = "Disable";
            this.tabPageDisable.UseVisualStyleBackColor = true;
            // 
            // dataGridViewDisable
            // 
            this.dataGridViewDisable.AllowUserToAddRows = false;
            this.dataGridViewDisable.AllowUserToDeleteRows = false;
            this.dataGridViewDisable.AllowUserToResizeRows = false;
            this.dataGridViewDisable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDisable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDisable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.AdColumn,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.receiveDayColumn,
            this.statusColumn,
            this.actionResultColumn});
            this.dataGridViewDisable.Location = new System.Drawing.Point(2, 50);
            this.dataGridViewDisable.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewDisable.Name = "dataGridViewDisable";
            this.dataGridViewDisable.ReadOnly = true;
            this.dataGridViewDisable.RowHeadersVisible = false;
            this.dataGridViewDisable.RowTemplate.Height = 24;
            this.dataGridViewDisable.ShowEditingIcon = false;
            this.dataGridViewDisable.Size = new System.Drawing.Size(744, 502);
            this.dataGridViewDisable.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 55;
            // 
            // AdColumn
            // 
            this.AdColumn.HeaderText = "AD";
            this.AdColumn.Name = "AdColumn";
            this.AdColumn.ReadOnly = true;
            this.AdColumn.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn6.HeaderText = "ResignDate";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 88;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "HR Code";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 130;
            // 
            // receiveDayColumn
            // 
            this.receiveDayColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.receiveDayColumn.HeaderText = "Receive";
            this.receiveDayColumn.Name = "receiveDayColumn";
            this.receiveDayColumn.ReadOnly = true;
            this.receiveDayColumn.Width = 72;
            // 
            // statusColumn
            // 
            this.statusColumn.HeaderText = "Status";
            this.statusColumn.Name = "statusColumn";
            this.statusColumn.ReadOnly = true;
            // 
            // actionResultColumn
            // 
            this.actionResultColumn.HeaderText = "Action Result";
            this.actionResultColumn.Name = "actionResultColumn";
            this.actionResultColumn.ReadOnly = true;
            this.actionResultColumn.Width = 150;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonExecuteDisable);
            this.panel3.Controls.Add(this.buttonFillDisableGrid);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(2, 2);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(742, 42);
            this.panel3.TabIndex = 2;
            // 
            // buttonExecuteDisable
            // 
            this.buttonExecuteDisable.Enabled = false;
            this.buttonExecuteDisable.Location = new System.Drawing.Point(106, 11);
            this.buttonExecuteDisable.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExecuteDisable.Name = "buttonExecuteDisable";
            this.buttonExecuteDisable.Size = new System.Drawing.Size(56, 19);
            this.buttonExecuteDisable.TabIndex = 1;
            this.buttonExecuteDisable.Text = "Disable";
            this.buttonExecuteDisable.UseVisualStyleBackColor = true;
            this.buttonExecuteDisable.Click += new System.EventHandler(this.ButtonExecuteDisable_Click);
            // 
            // buttonFillDisableGrid
            // 
            this.buttonFillDisableGrid.Location = new System.Drawing.Point(4, 11);
            this.buttonFillDisableGrid.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFillDisableGrid.Name = "buttonFillDisableGrid";
            this.buttonFillDisableGrid.Size = new System.Drawing.Size(56, 19);
            this.buttonFillDisableGrid.TabIndex = 0;
            this.buttonFillDisableGrid.Text = "Fetch";
            this.buttonFillDisableGrid.UseVisualStyleBackColor = true;
            this.buttonFillDisableGrid.Click += new System.EventHandler(this.ButtonFillDisableGrid_Click);
            // 
            // tabPageDelete
            // 
            this.tabPageDelete.Controls.Add(this.dataGridViewDelete);
            this.tabPageDelete.Controls.Add(this.panel4);
            this.tabPageDelete.Location = new System.Drawing.Point(4, 22);
            this.tabPageDelete.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageDelete.Name = "tabPageDelete";
            this.tabPageDelete.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageDelete.Size = new System.Drawing.Size(746, 554);
            this.tabPageDelete.TabIndex = 2;
            this.tabPageDelete.Text = "Delete";
            this.tabPageDelete.UseVisualStyleBackColor = true;
            // 
            // dataGridViewDelete
            // 
            this.dataGridViewDelete.AllowUserToAddRows = false;
            this.dataGridViewDelete.AllowUserToDeleteRows = false;
            this.dataGridViewDelete.AllowUserToResizeRows = false;
            this.dataGridViewDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDelete.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDelete.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.actionResultColumnDelete});
            this.dataGridViewDelete.Location = new System.Drawing.Point(2, 50);
            this.dataGridViewDelete.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewDelete.Name = "dataGridViewDelete";
            this.dataGridViewDelete.ReadOnly = true;
            this.dataGridViewDelete.RowHeadersVisible = false;
            this.dataGridViewDelete.RowTemplate.Height = 24;
            this.dataGridViewDelete.ShowEditingIcon = false;
            this.dataGridViewDelete.Size = new System.Drawing.Size(744, 505);
            this.dataGridViewDelete.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Id";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 55;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "AD";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn5.HeaderText = "ResignDate";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 88;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "HR Code";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 130;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn9.HeaderText = "Receive";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 72;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Status";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // actionResultColumnDelete
            // 
            this.actionResultColumnDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.actionResultColumnDelete.HeaderText = "Action Result";
            this.actionResultColumnDelete.Name = "actionResultColumnDelete";
            this.actionResultColumnDelete.ReadOnly = true;
            this.actionResultColumnDelete.Width = 95;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.buttonExecuteDelete);
            this.panel4.Controls.Add(this.buttonFetchDeleteGrid);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(2, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(742, 42);
            this.panel4.TabIndex = 3;
            // 
            // buttonExecuteDelete
            // 
            this.buttonExecuteDelete.Enabled = false;
            this.buttonExecuteDelete.Location = new System.Drawing.Point(106, 11);
            this.buttonExecuteDelete.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExecuteDelete.Name = "buttonExecuteDelete";
            this.buttonExecuteDelete.Size = new System.Drawing.Size(56, 19);
            this.buttonExecuteDelete.TabIndex = 2;
            this.buttonExecuteDelete.Text = "Delete";
            this.buttonExecuteDelete.UseVisualStyleBackColor = true;
            this.buttonExecuteDelete.Click += new System.EventHandler(this.ButtonActionDelete_Click);
            // 
            // buttonFetchDeleteGrid
            // 
            this.buttonFetchDeleteGrid.Location = new System.Drawing.Point(4, 11);
            this.buttonFetchDeleteGrid.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFetchDeleteGrid.Name = "buttonFetchDeleteGrid";
            this.buttonFetchDeleteGrid.Size = new System.Drawing.Size(56, 19);
            this.buttonFetchDeleteGrid.TabIndex = 0;
            this.buttonFetchDeleteGrid.Text = "Fetch";
            this.buttonFetchDeleteGrid.UseVisualStyleBackColor = true;
            this.buttonFetchDeleteGrid.Click += new System.EventHandler(this.ButtonFillDeleteGrid_Click);
            // 
            // tabPageQuery
            // 
            this.tabPageQuery.Controls.Add(this.panel2);
            this.tabPageQuery.Controls.Add(this.dataGridViewQuery);
            this.tabPageQuery.Location = new System.Drawing.Point(4, 22);
            this.tabPageQuery.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageQuery.Name = "tabPageQuery";
            this.tabPageQuery.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageQuery.Size = new System.Drawing.Size(746, 554);
            this.tabPageQuery.TabIndex = 3;
            this.tabPageQuery.Text = "Query";
            this.tabPageQuery.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButtonFilterStatus);
            this.panel2.Controls.Add(this.radioButtonFilterAd);
            this.panel2.Controls.Add(this.textBoxFilterAd);
            this.panel2.Controls.Add(this.buttonDeleteRecord);
            this.panel2.Controls.Add(this.buttonUpdateRecord);
            this.panel2.Controls.Add(this.buttonInsertNewRecord);
            this.panel2.Controls.Add(this.buttonCommitQueryGrid);
            this.panel2.Controls.Add(this.statusQueryComboBox);
            this.panel2.Controls.Add(this.buttonFillQueryGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(742, 42);
            this.panel2.TabIndex = 6;
            // 
            // radioButtonFilterStatus
            // 
            this.radioButtonFilterStatus.AutoSize = true;
            this.radioButtonFilterStatus.Location = new System.Drawing.Point(333, 15);
            this.radioButtonFilterStatus.Name = "radioButtonFilterStatus";
            this.radioButtonFilterStatus.Size = new System.Drawing.Size(14, 13);
            this.radioButtonFilterStatus.TabIndex = 15;
            this.radioButtonFilterStatus.UseVisualStyleBackColor = true;
            // 
            // radioButtonFilterAd
            // 
            this.radioButtonFilterAd.AutoSize = true;
            this.radioButtonFilterAd.Checked = true;
            this.radioButtonFilterAd.Location = new System.Drawing.Point(217, 14);
            this.radioButtonFilterAd.Name = "radioButtonFilterAd";
            this.radioButtonFilterAd.Size = new System.Drawing.Size(14, 13);
            this.radioButtonFilterAd.TabIndex = 14;
            this.radioButtonFilterAd.TabStop = true;
            this.radioButtonFilterAd.UseVisualStyleBackColor = true;
            // 
            // textBoxFilterAd
            // 
            this.textBoxFilterAd.Location = new System.Drawing.Point(112, 12);
            this.textBoxFilterAd.Name = "textBoxFilterAd";
            this.textBoxFilterAd.Size = new System.Drawing.Size(100, 20);
            this.textBoxFilterAd.TabIndex = 7;
            this.textBoxFilterAd.TextChanged += new System.EventHandler(this.TextBoxFilterAd_TextChanged);
            // 
            // buttonDeleteRecord
            // 
            this.buttonDeleteRecord.Location = new System.Drawing.Point(571, 11);
            this.buttonDeleteRecord.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDeleteRecord.Name = "buttonDeleteRecord";
            this.buttonDeleteRecord.Size = new System.Drawing.Size(56, 19);
            this.buttonDeleteRecord.TabIndex = 13;
            this.buttonDeleteRecord.Text = "Delete";
            this.buttonDeleteRecord.UseVisualStyleBackColor = true;
            this.buttonDeleteRecord.Click += new System.EventHandler(this.ButtonDeleteRecord_Click);
            // 
            // buttonUpdateRecord
            // 
            this.buttonUpdateRecord.Location = new System.Drawing.Point(498, 11);
            this.buttonUpdateRecord.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUpdateRecord.Name = "buttonUpdateRecord";
            this.buttonUpdateRecord.Size = new System.Drawing.Size(56, 19);
            this.buttonUpdateRecord.TabIndex = 12;
            this.buttonUpdateRecord.Text = "Update";
            this.buttonUpdateRecord.UseVisualStyleBackColor = true;
            this.buttonUpdateRecord.Click += new System.EventHandler(this.ButtonUpdateRecord_Click);
            // 
            // buttonInsertNewRecord
            // 
            this.buttonInsertNewRecord.Location = new System.Drawing.Point(424, 11);
            this.buttonInsertNewRecord.Margin = new System.Windows.Forms.Padding(2);
            this.buttonInsertNewRecord.Name = "buttonInsertNewRecord";
            this.buttonInsertNewRecord.Size = new System.Drawing.Size(56, 19);
            this.buttonInsertNewRecord.TabIndex = 10;
            this.buttonInsertNewRecord.Text = "Insert";
            this.buttonInsertNewRecord.UseVisualStyleBackColor = true;
            this.buttonInsertNewRecord.Click += new System.EventHandler(this.ButtonInsertNewRecord_Click);
            // 
            // buttonCommitQueryGrid
            // 
            this.buttonCommitQueryGrid.Location = new System.Drawing.Point(684, 11);
            this.buttonCommitQueryGrid.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCommitQueryGrid.Name = "buttonCommitQueryGrid";
            this.buttonCommitQueryGrid.Size = new System.Drawing.Size(56, 19);
            this.buttonCommitQueryGrid.TabIndex = 9;
            this.buttonCommitQueryGrid.Text = "Commit";
            this.buttonCommitQueryGrid.UseVisualStyleBackColor = true;
            this.buttonCommitQueryGrid.Click += new System.EventHandler(this.ButtonCommitQueryGrid_Click);
            // 
            // statusQueryComboBox
            // 
            this.statusQueryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusQueryComboBox.FormattingEnabled = true;
            this.statusQueryComboBox.Location = new System.Drawing.Point(236, 11);
            this.statusQueryComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.statusQueryComboBox.Name = "statusQueryComboBox";
            this.statusQueryComboBox.Size = new System.Drawing.Size(92, 21);
            this.statusQueryComboBox.TabIndex = 7;
            // 
            // buttonFillQueryGrid
            // 
            this.buttonFillQueryGrid.Location = new System.Drawing.Point(4, 11);
            this.buttonFillQueryGrid.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFillQueryGrid.Name = "buttonFillQueryGrid";
            this.buttonFillQueryGrid.Size = new System.Drawing.Size(56, 19);
            this.buttonFillQueryGrid.TabIndex = 0;
            this.buttonFillQueryGrid.Text = "Fetch";
            this.buttonFillQueryGrid.UseVisualStyleBackColor = true;
            this.buttonFillQueryGrid.Click += new System.EventHandler(this.ButtonFillQueryGrid_Click);
            // 
            // dataGridViewQuery
            // 
            this.dataGridViewQuery.AllowUserToAddRows = false;
            this.dataGridViewQuery.AllowUserToDeleteRows = false;
            this.dataGridViewQuery.AllowUserToResizeColumns = false;
            this.dataGridViewQuery.AllowUserToResizeRows = false;
            this.dataGridViewQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewQuery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewQuery.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewStatusColumn,
            this.ColumnManager,
            this.erorrMessageColumn});
            this.dataGridViewQuery.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewQuery.Location = new System.Drawing.Point(2, 50);
            this.dataGridViewQuery.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewQuery.MultiSelect = false;
            this.dataGridViewQuery.Name = "dataGridViewQuery";
            this.dataGridViewQuery.ReadOnly = true;
            this.dataGridViewQuery.RowHeadersVisible = false;
            this.dataGridViewQuery.RowTemplate.Height = 24;
            this.dataGridViewQuery.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewQuery.ShowEditingIcon = false;
            this.dataGridViewQuery.Size = new System.Drawing.Size(744, 505);
            this.dataGridViewQuery.TabIndex = 5;
            this.dataGridViewQuery.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DataGridViewQuery_KeyPress);
            this.dataGridViewQuery.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DataGridViewQuery_KeyUp);
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.Frozen = true;
            this.dataGridViewTextBoxColumn11.HeaderText = "Id";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 55;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "AD";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 150;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn13.HeaderText = "ResignDate";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 88;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "HR Code";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 130;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn15.HeaderText = "Receive";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Width = 72;
            // 
            // dataGridViewStatusColumn
            // 
            this.dataGridViewStatusColumn.HeaderText = "Status";
            this.dataGridViewStatusColumn.Name = "dataGridViewStatusColumn";
            this.dataGridViewStatusColumn.ReadOnly = true;
            this.dataGridViewStatusColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ColumnManager
            // 
            this.ColumnManager.HeaderText = "Manager";
            this.ColumnManager.Name = "ColumnManager";
            this.ColumnManager.ReadOnly = true;
            // 
            // erorrMessageColumn
            // 
            this.erorrMessageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.erorrMessageColumn.HeaderText = "Error Message";
            this.erorrMessageColumn.Name = "erorrMessageColumn";
            this.erorrMessageColumn.ReadOnly = true;
            // 
            // FormResignHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 580);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormResignHandler";
            this.Text = "Resign Account Handler";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl1.ResumeLayout(false);
            this.tabPageUpdate.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUpdate)).EndInit();
            this.tabPageDisable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDisable)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tabPageDelete.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDelete)).EndInit();
            this.panel4.ResumeLayout(false);
            this.tabPageQuery.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQuery)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageUpdate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridViewUpdate;
        private System.Windows.Forms.TabPage tabPageDisable;
        private System.Windows.Forms.TabPage tabPageDelete;
        private System.Windows.Forms.TabPage tabPageQuery;
        private System.Windows.Forms.Button buttonUpdateOl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonFillDisableGrid;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonFetchDeleteGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn subjectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn receiveDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeColmn;
        private System.Windows.Forms.Label labelResignEmailsCount;
        private System.Windows.Forms.Label labelTotalEmails;
        private System.Windows.Forms.DataGridView dataGridViewDisable;
        private System.Windows.Forms.DataGridView dataGridViewDelete;
        private System.Windows.Forms.DataGridView dataGridViewQuery;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonFillQueryGrid;
        private System.Windows.Forms.Button buttonExecuteDisable;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn receiveDayColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionResultColumn;
        private System.Windows.Forms.ComboBox statusQueryComboBox;
        private System.Windows.Forms.Button buttonCommitQueryGrid;
        private System.Windows.Forms.Button buttonInsertNewRecord;
        private System.Windows.Forms.Button buttonExecuteDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionResultColumnDelete;
        private System.Windows.Forms.Button buttonDeleteRecord;
        private System.Windows.Forms.Button buttonUpdateRecord;
        private System.Windows.Forms.TextBox textBoxFilterAd;
        private System.Windows.Forms.RadioButton radioButtonFilterStatus;
        private System.Windows.Forms.RadioButton radioButtonFilterAd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnManager;
        private System.Windows.Forms.DataGridViewTextBoxColumn erorrMessageColumn;
    }
}

