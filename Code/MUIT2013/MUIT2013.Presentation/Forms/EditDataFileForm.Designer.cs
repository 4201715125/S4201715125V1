namespace MUIT2013.Presentation.Forms
{
    partial class EditDataFileForm
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
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtFileDescription = new System.Windows.Forms.RichTextBox();
            this.lblFileDescription = new System.Windows.Forms.Label();
            this.lblIsActivated = new System.Windows.Forms.Label();
            this.txtCreatedDate = new System.Windows.Forms.TextBox();
            this.lblCreatedDate = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.cbIsActivated = new System.Windows.Forms.CheckBox();
            this.cbIsMapped = new System.Windows.Forms.CheckBox();
            this.lblIsMapped = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(222, 222);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(141, 222);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // txtFileDescription
            // 
            this.txtFileDescription.Location = new System.Drawing.Point(141, 125);
            this.txtFileDescription.Name = "txtFileDescription";
            this.txtFileDescription.Size = new System.Drawing.Size(240, 86);
            this.txtFileDescription.TabIndex = 18;
            this.txtFileDescription.Text = "";
            // 
            // lblFileDescription
            // 
            this.lblFileDescription.AutoSize = true;
            this.lblFileDescription.Location = new System.Drawing.Point(12, 125);
            this.lblFileDescription.Name = "lblFileDescription";
            this.lblFileDescription.Size = new System.Drawing.Size(60, 13);
            this.lblFileDescription.TabIndex = 17;
            this.lblFileDescription.Text = "Description";
            // 
            // lblIsActivated
            // 
            this.lblIsActivated.AutoSize = true;
            this.lblIsActivated.Location = new System.Drawing.Point(12, 73);
            this.lblIsActivated.Name = "lblIsActivated";
            this.lblIsActivated.Size = new System.Drawing.Size(58, 13);
            this.lblIsActivated.TabIndex = 15;
            this.lblIsActivated.Text = "Activated?";
            // 
            // txtCreatedDate
            // 
            this.txtCreatedDate.Location = new System.Drawing.Point(141, 44);
            this.txtCreatedDate.Name = "txtCreatedDate";
            this.txtCreatedDate.PasswordChar = '*';
            this.txtCreatedDate.ReadOnly = true;
            this.txtCreatedDate.Size = new System.Drawing.Size(240, 20);
            this.txtCreatedDate.TabIndex = 14;
            // 
            // lblCreatedDate
            // 
            this.lblCreatedDate.AutoSize = true;
            this.lblCreatedDate.Location = new System.Drawing.Point(12, 47);
            this.lblCreatedDate.Name = "lblCreatedDate";
            this.lblCreatedDate.Size = new System.Drawing.Size(70, 13);
            this.lblCreatedDate.TabIndex = 13;
            this.lblCreatedDate.Text = "Created Date";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(12, 21);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(54, 13);
            this.lblFileName.TabIndex = 11;
            this.lblFileName.Text = "File Name";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(141, 18);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(240, 20);
            this.txtProjectName.TabIndex = 12;
            // 
            // cbIsActivated
            // 
            this.cbIsActivated.AutoSize = true;
            this.cbIsActivated.Enabled = false;
            this.cbIsActivated.Location = new System.Drawing.Point(141, 73);
            this.cbIsActivated.Name = "cbIsActivated";
            this.cbIsActivated.Size = new System.Drawing.Size(15, 14);
            this.cbIsActivated.TabIndex = 21;
            this.cbIsActivated.ThreeState = true;
            this.cbIsActivated.UseVisualStyleBackColor = true;
            // 
            // cbIsMapped
            // 
            this.cbIsMapped.AutoSize = true;
            this.cbIsMapped.Enabled = false;
            this.cbIsMapped.Location = new System.Drawing.Point(141, 98);
            this.cbIsMapped.Name = "cbIsMapped";
            this.cbIsMapped.Size = new System.Drawing.Size(15, 14);
            this.cbIsMapped.TabIndex = 23;
            this.cbIsMapped.ThreeState = true;
            this.cbIsMapped.UseVisualStyleBackColor = true;
            // 
            // lblIsMapped
            // 
            this.lblIsMapped.AutoSize = true;
            this.lblIsMapped.Location = new System.Drawing.Point(12, 98);
            this.lblIsMapped.Name = "lblIsMapped";
            this.lblIsMapped.Size = new System.Drawing.Size(52, 13);
            this.lblIsMapped.TabIndex = 22;
            this.lblIsMapped.Text = "Mapped?";
            // 
            // EditDataFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 262);
            this.Controls.Add(this.cbIsMapped);
            this.Controls.Add(this.lblIsMapped);
            this.Controls.Add(this.cbIsActivated);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtFileDescription);
            this.Controls.Add(this.lblFileDescription);
            this.Controls.Add(this.lblIsActivated);
            this.Controls.Add(this.txtCreatedDate);
            this.Controls.Add(this.lblCreatedDate);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.lblFileName);
            this.Name = "EditDataFileForm";
            this.Text = "EditDataFileForm";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RichTextBox txtFileDescription;
        private System.Windows.Forms.Label lblFileDescription;
        private System.Windows.Forms.Label lblIsActivated;
        private System.Windows.Forms.TextBox txtCreatedDate;
        private System.Windows.Forms.Label lblCreatedDate;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.CheckBox cbIsActivated;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.CheckBox cbIsMapped;
        private System.Windows.Forms.Label lblIsMapped;
    }
}