namespace MUIT2013.Presentation.Forms
{
    partial class QuickReductResultForm
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
            this.txtQuickRedultDescription = new System.Windows.Forms.TextBox();
            this.dgvReductResults = new System.Windows.Forms.DataGridView();
            this.dgvcReductType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReductResults)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quick Reduct";
            // 
            // txtQuickRedultDescription
            // 
            this.txtQuickRedultDescription.Location = new System.Drawing.Point(111, 6);
            this.txtQuickRedultDescription.Multiline = true;
            this.txtQuickRedultDescription.Name = "txtQuickRedultDescription";
            this.txtQuickRedultDescription.Size = new System.Drawing.Size(595, 39);
            this.txtQuickRedultDescription.TabIndex = 7;
            // 
            // dgvReductResults
            // 
            this.dgvReductResults.AllowUserToAddRows = false;
            this.dgvReductResults.AllowUserToDeleteRows = false;
            this.dgvReductResults.AllowUserToOrderColumns = true;
            this.dgvReductResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReductResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReductResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcReductType,
            this.dgvcDescription});
            this.dgvReductResults.Location = new System.Drawing.Point(12, 51);
            this.dgvReductResults.MultiSelect = false;
            this.dgvReductResults.Name = "dgvReductResults";
            this.dgvReductResults.ReadOnly = true;
            this.dgvReductResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReductResults.Size = new System.Drawing.Size(694, 348);
            this.dgvReductResults.TabIndex = 8;
            // 
            // dgvcReductType
            // 
            this.dgvcReductType.DataPropertyName = "ReductType";
            this.dgvcReductType.Frozen = true;
            this.dgvcReductType.HeaderText = "Reduct Type";
            this.dgvcReductType.MinimumWidth = 100;
            this.dgvcReductType.Name = "dgvcReductType";
            this.dgvcReductType.ReadOnly = true;
            this.dgvcReductType.Width = 200;
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
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(256, 405);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(337, 405);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // QuickReductResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 440);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvReductResults);
            this.Controls.Add(this.txtQuickRedultDescription);
            this.Controls.Add(this.label1);
            this.Name = "QuickReductResultForm";
            this.Text = "Quick Reduct Result";
            this.Load += new System.EventHandler(this.QuickReductResultForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReductResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuickRedultDescription;
        private System.Windows.Forms.DataGridView dgvReductResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcReductType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}