namespace MUIT2013.Presentation.Forms
{
    partial class DecisionTableHistoryForm
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
            this.dgDecisionTableHistory = new System.Windows.Forms.DataGridView();
            this.dgvcTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcCreatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgDecisionTableHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // btnViewData
            // 
            this.btnViewData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewData.Location = new System.Drawing.Point(299, 447);
            this.btnViewData.Name = "btnViewData";
            this.btnViewData.Size = new System.Drawing.Size(104, 23);
            this.btnViewData.TabIndex = 9;
            this.btnViewData.Text = "View Data";
            this.btnViewData.UseVisualStyleBackColor = true;
            this.btnViewData.Click += new System.EventHandler(this.btnViewData_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoose.Location = new System.Drawing.Point(409, 447);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(75, 23);
            this.btnChoose.TabIndex = 8;
            this.btnChoose.Text = "Choose";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // dgDecisionTableHistory
            // 
            this.dgDecisionTableHistory.AllowUserToAddRows = false;
            this.dgDecisionTableHistory.AllowUserToDeleteRows = false;
            this.dgDecisionTableHistory.AllowUserToOrderColumns = true;
            this.dgDecisionTableHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgDecisionTableHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDecisionTableHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcTableName,
            this.dgvcAction,
            this.dgvcCreatedDate,
            this.dgvcDescription});
            this.dgDecisionTableHistory.Location = new System.Drawing.Point(12, 24);
            this.dgDecisionTableHistory.MultiSelect = false;
            this.dgDecisionTableHistory.Name = "dgDecisionTableHistory";
            this.dgDecisionTableHistory.ReadOnly = true;
            this.dgDecisionTableHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDecisionTableHistory.Size = new System.Drawing.Size(845, 414);
            this.dgDecisionTableHistory.TabIndex = 2;
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
            // dgvcAction
            // 
            this.dgvcAction.DataPropertyName = "Action";
            this.dgvcAction.HeaderText = "Action";
            this.dgvcAction.MinimumWidth = 100;
            this.dgvcAction.Name = "dgvcAction";
            this.dgvcAction.ReadOnly = true;
            // 
            // dgvcCreatedDate
            // 
            this.dgvcCreatedDate.DataPropertyName = "CreatedDate";
            this.dgvcCreatedDate.HeaderText = "Created Date";
            this.dgvcCreatedDate.MinimumWidth = 80;
            this.dgvcCreatedDate.Name = "dgvcCreatedDate";
            this.dgvcCreatedDate.ReadOnly = true;
            // 
            // dgvcDescription
            // 
            this.dgvcDescription.DataPropertyName = "Description";
            this.dgvcDescription.HeaderText = "Description";
            this.dgvcDescription.MinimumWidth = 100;
            this.dgvcDescription.Name = "dgvcDescription";
            this.dgvcDescription.ReadOnly = true;
            this.dgvcDescription.Width = 350;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(490, 447);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DecisionTableHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 479);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnViewData);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.dgDecisionTableHistory);
            this.Name = "DecisionTableHistoryForm";
            this.Text = "Tracker";
            this.Load += new System.EventHandler(this.HandlerTrackerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgDecisionTableHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgDecisionTableHistory;
        private System.Windows.Forms.Button btnViewData;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcCreatedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcDescription;
        private System.Windows.Forms.Button btnCancel;
    }
}