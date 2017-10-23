namespace ResignAccountHandlerUI.Forms
{
    partial class ResignEditor
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
            this.textBoxAd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerReceiveDay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxHrCode = new System.Windows.Forms.TextBox();
            this.dateTimePickerResignDay = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxError = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxManager = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxAd
            // 
            this.textBoxAd.Location = new System.Drawing.Point(97, 10);
            this.textBoxAd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxAd.Name = "textBoxAd";
            this.textBoxAd.Size = new System.Drawing.Size(108, 20);
            this.textBoxAd.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "AD:";
            // 
            // dateTimePickerReceiveDay
            // 
            this.dateTimePickerReceiveDay.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            this.dateTimePickerReceiveDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerReceiveDay.Location = new System.Drawing.Point(301, 10);
            this.dateTimePickerReceiveDay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dateTimePickerReceiveDay.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerReceiveDay.Name = "dateTimePickerReceiveDay";
            this.dateTimePickerReceiveDay.Size = new System.Drawing.Size(162, 20);
            this.dateTimePickerReceiveDay.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "HR Code:";
            // 
            // textBoxHrCode
            // 
            this.textBoxHrCode.Location = new System.Drawing.Point(97, 32);
            this.textBoxHrCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxHrCode.Name = "textBoxHrCode";
            this.textBoxHrCode.Size = new System.Drawing.Size(108, 20);
            this.textBoxHrCode.TabIndex = 3;
            // 
            // dateTimePickerResignDay
            // 
            this.dateTimePickerResignDay.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            this.dateTimePickerResignDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerResignDay.Location = new System.Drawing.Point(301, 32);
            this.dateTimePickerResignDay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dateTimePickerResignDay.Name = "dateTimePickerResignDay";
            this.dateTimePickerResignDay.Size = new System.Drawing.Size(162, 20);
            this.dateTimePickerResignDay.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Receive Day:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(232, 35);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Resign Day:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 58);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Note(Erorr mess):";
            // 
            // textBoxError
            // 
            this.textBoxError.Location = new System.Drawing.Point(97, 55);
            this.textBoxError.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxError.Name = "textBoxError";
            this.textBoxError.Size = new System.Drawing.Size(108, 20);
            this.textBoxError.TabIndex = 8;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(135, 118);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(56, 19);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(304, 118);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(56, 19);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(97, 78);
            this.comboBoxStatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(108, 21);
            this.comboBoxStatus.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 80);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Status:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(248, 58);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Manager";
            // 
            // textBoxManager
            // 
            this.textBoxManager.Location = new System.Drawing.Point(301, 56);
            this.textBoxManager.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxManager.Name = "textBoxManager";
            this.textBoxManager.Size = new System.Drawing.Size(162, 20);
            this.textBoxManager.TabIndex = 14;
            // 
            // ResignEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 146);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxManager);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxError);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePickerResignDay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxHrCode);
            this.Controls.Add(this.dateTimePickerReceiveDay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxAd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ResignEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editor";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormInsert_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerReceiveDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxHrCode;
        private System.Windows.Forms.DateTimePicker dateTimePickerResignDay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxError;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxManager;
    }
}