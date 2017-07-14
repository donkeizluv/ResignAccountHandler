namespace ResignAccountHandlerUI.Forms
{
    partial class FormCommitLog
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
            this.commitLogdataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erorrMessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommitResultColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.commitLogdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // commitLogdataGridView
            // 
            this.commitLogdataGridView.AllowUserToAddRows = false;
            this.commitLogdataGridView.AllowUserToDeleteRows = false;
            this.commitLogdataGridView.AllowUserToResizeColumns = false;
            this.commitLogdataGridView.AllowUserToResizeRows = false;
            this.commitLogdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.commitLogdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewStatusColumn,
            this.erorrMessageColumn,
            this.CommitResultColumn1});
            this.commitLogdataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commitLogdataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.commitLogdataGridView.Location = new System.Drawing.Point(0, 0);
            this.commitLogdataGridView.MultiSelect = false;
            this.commitLogdataGridView.Name = "commitLogdataGridView";
            this.commitLogdataGridView.ReadOnly = true;
            this.commitLogdataGridView.RowHeadersVisible = false;
            this.commitLogdataGridView.RowTemplate.Height = 24;
            this.commitLogdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.commitLogdataGridView.ShowEditingIcon = false;
            this.commitLogdataGridView.Size = new System.Drawing.Size(1054, 507);
            this.commitLogdataGridView.TabIndex = 6;
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
            this.dataGridViewTextBoxColumn13.Width = 111;
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
            this.dataGridViewTextBoxColumn15.Width = 88;
            // 
            // dataGridViewStatusColumn
            // 
            this.dataGridViewStatusColumn.HeaderText = "Status";
            this.dataGridViewStatusColumn.Name = "dataGridViewStatusColumn";
            this.dataGridViewStatusColumn.ReadOnly = true;
            this.dataGridViewStatusColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // erorrMessageColumn
            // 
            this.erorrMessageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.erorrMessageColumn.HeaderText = "Error Message";
            this.erorrMessageColumn.Name = "erorrMessageColumn";
            this.erorrMessageColumn.ReadOnly = true;
            this.erorrMessageColumn.Width = 130;
            // 
            // CommitResultColumn1
            // 
            this.CommitResultColumn1.HeaderText = "Commit";
            this.CommitResultColumn1.Name = "CommitResultColumn1";
            this.CommitResultColumn1.ReadOnly = true;
            // 
            // FormCommitLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 507);
            this.Controls.Add(this.commitLogdataGridView);
            this.Name = "FormCommitLog";
            this.Text = "Commit Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCommitLog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.commitLogdataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView commitLogdataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn erorrMessageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommitResultColumn1;
    }
}