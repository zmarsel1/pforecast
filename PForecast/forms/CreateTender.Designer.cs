namespace PForecast
{
    partial class CreateTender
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtParentDocNum = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpDateDoc = new System.Windows.Forms.DateTimePicker();
            this.cmbRP = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkParentDocument = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(14, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Документ №";
            // 
            // txtParentDocNum
            // 
            this.txtParentDocNum.Enabled = false;
            this.txtParentDocNum.Location = new System.Drawing.Point(92, 84);
            this.txtParentDocNum.Name = "txtParentDocNum";
            this.txtParentDocNum.ReadOnly = true;
            this.txtParentDocNum.Size = new System.Drawing.Size(116, 20);
            this.txtParentDocNum.TabIndex = 8;
            this.txtParentDocNum.Text = "8";
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(214, 82);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(41, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "...";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpDateDoc
            // 
            this.dtpDateDoc.Location = new System.Drawing.Point(54, 8);
            this.dtpDateDoc.Name = "dtpDateDoc";
            this.dtpDateDoc.Size = new System.Drawing.Size(201, 20);
            this.dtpDateDoc.TabIndex = 3;
            // 
            // cmbRP
            // 
            this.cmbRP.FormattingEnabled = true;
            this.cmbRP.Location = new System.Drawing.Point(54, 34);
            this.cmbRP.Name = "cmbRP";
            this.cmbRP.Size = new System.Drawing.Size(201, 21);
            this.cmbRP.TabIndex = 5;
            this.cmbRP.SelectedIndexChanged += new System.EventHandler(this.cmbRP_SelectedIndexChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(180, 111);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 11;
            this.btnCreate.Text = "Создать";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(99, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дата:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "РП:";
            // 
            // chkParentDocument
            // 
            this.chkParentDocument.AutoSize = true;
            this.chkParentDocument.Location = new System.Drawing.Point(17, 61);
            this.chkParentDocument.Name = "chkParentDocument";
            this.chkParentDocument.Size = new System.Drawing.Size(171, 17);
            this.chkParentDocument.TabIndex = 12;
            this.chkParentDocument.Text = "На основе прогноза(заявки)";
            this.chkParentDocument.UseVisualStyleBackColor = true;
            this.chkParentDocument.CheckedChanged += new System.EventHandler(this.chkParentDocument_CheckedChanged);
            // 
            // CreateTender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(258, 137);
            this.Controls.Add(this.chkParentDocument);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.cmbRP);
            this.Controls.Add(this.dtpDateDoc);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtParentDocNum);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(266, 164);
            this.MinimumSize = new System.Drawing.Size(266, 164);
            this.Name = "CreateTender";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Создать заявку";
            this.Load += new System.EventHandler(this.CreateTender_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtParentDocNum;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpDateDoc;
        private System.Windows.Forms.ComboBox cmbRP;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkParentDocument;
    }
}