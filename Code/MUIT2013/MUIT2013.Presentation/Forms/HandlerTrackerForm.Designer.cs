namespace MUIT2013.Presentation.Forms
{
    partial class HandlerTrackerForm
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
            this.btnViewData = new System.Windows.Forms.Button();
            this.btnChoose = new System.Windows.Forms.Button();
            this.dgvHandlerTracker = new System.Windows.Forms.DataGridView();
            this.dgvcTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcCreatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcPreviousTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHandlerTracker)).BeginInit();
            this.SuspendLayout();
            // 
            // btnViewData
            // 
            this.btnViewData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewData.Location = new System.Drawing.Point(299, 444);
            this.btnViewData.Name = "btnViewData";
            this.btnViewData.Size = new System.Drawing.Size(104, 23);
            this.btnViewData.TabIndex = 9;
            this.btnViewData.Text = "View Data";
            this.btnViewData.UseVisualStyleBackColor = true;
            this.btnViewData.Click += new System.EventHandler(this.btnViewData_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoose.Location = new System.Drawing.Point(409, 444);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(75, 23);
            this.btnChoose.TabIndex = 8;
            this.btnChoose.Text = "Choose";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // dgvHandlerTracker
            // 
            this.dgvHandlerTracker.AllowUserToAddRows = false;
            this.dgvHandlerTracker.AllowUserToDeleteRows = false;
            this.dgvHandlerTracker.AllowUserToOrderColumns = true;
            this.dgvHandlerTracker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHandlerTracker.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHandlerTracker.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcTableName,
            this.dgvcContent,
            this.dgvcCreatedDate,
            this.dgvcPreviousTableName});
            this.dgvHandlerTracker.Location = new System.Drawing.Point(12, 24);
            this.dgvHandlerTracker.MultiSelect = false;
            this.dgvHandlerTracker.Name = "dgvHandlerTracker";
            this.dgvHandlerTracker.ReadOnly = true;
            this.dgvHandlerTracker.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHandlerTracker.Size = new System.Drawing.Size(845, 414);
            this.dgvHandlerTracker.TabIndex = 2;
            // 
            // dgvcTableName
            // 
            this.dgvcTableName.DataPropertyName = "TableName";
            this.dgvcTableName.Frozen = true;
            this.dgvcTableName.HeaderText = "Table Name";
            this.dgvcTableName.MinimumWidth = 100;
            this.dgvcTableName.Name = "dgvcTableName";
            this.dgvcTableName.ReadOnly = true;
            this.dgvcTableName.Width = 200;
            // 
            // dgvcContent
            // 
            this.dgvcContent.DataPropertyName = "Content";
            this.dgvcContent.HeaderText = "Content";
            this.dgvcContent.MinimumWidth = 200;
            this.dgvcContent.Name = "dgvcContent";
            this.dgvcContent.ReadOnly = true;
            this.dgvcContent.Width = 200;
            // 
            // dgvcCreatedDate
            // 
            this.dgvcCreatedDate.DataPropertyName = "CreatedDate";
            this.dgvcCreatedDate.HeaderText = "Created Date";
            this.dgvcCreatedDate.MinimumWidth = 100;
            this.dgvcCreatedDate.Name = "dgvcCreatedDate";
            this.dgvcCreatedDate.ReadOnly = true;
            this.dgvcCreatedDate.Width = 200;
            // 
            // dgvcPreviousTableName
            // 
            this.dgvcPreviousTableName.HeaderText = "Previous Table Name";
            this.dgvcPreviousTableName.Name = "dgvcPreviousTableName";
            this.dgvcPreviousTableName.ReadOnly = true;
            this.dgvcPreviousTableName.Width = 200;
            // 
            // HandlerTrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 479);
            this.Controls.Add(this.btnViewData);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.dgvHandlerTracker);
            this.Name = "HandlerTrackerForm";
            this.Text = "Tracker";
            this.Load += new System.EventHandler(this.HandlerTrackerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHandlerTracker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHandlerTracker;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcCreatedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPreviousTableName;
        private System.Windows.Forms.Button btnViewData;
        private System.Windows.Forms.Button btnChoose;
    }
}