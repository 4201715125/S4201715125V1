namespace MUIT2013.Presentation.Forms
{
    partial class NoRuleForm
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
            this.dgvNoRuleValues = new System.Windows.Forms.DataGridView();
            this.dgvcValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcIsApplied = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNoRuleValues)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNoRuleValues
            // 
            this.dgvNoRuleValues.AllowUserToAddRows = false;
            this.dgvNoRuleValues.AllowUserToDeleteRows = false;
            this.dgvNoRuleValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNoRuleValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcValue,
            this.dgvcIsApplied});
            this.dgvNoRuleValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNoRuleValues.Location = new System.Drawing.Point(0, 0);
            this.dgvNoRuleValues.Name = "dgvNoRuleValues";
            this.dgvNoRuleValues.ReadOnly = true;
            this.dgvNoRuleValues.Size = new System.Drawing.Size(660, 357);
            this.dgvNoRuleValues.TabIndex = 0;
            // 
            // dgvcValue
            // 
            this.dgvcValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvcValue.DataPropertyName = "Value";
            this.dgvcValue.HeaderText = "Value";
            this.dgvcValue.Name = "dgvcValue";
            this.dgvcValue.ReadOnly = true;
            // 
            // dgvcIsApplied
            // 
            this.dgvcIsApplied.DataPropertyName = "IsApplied";
            this.dgvcIsApplied.HeaderText = "Is Applied";
            this.dgvcIsApplied.Name = "dgvcIsApplied";
            this.dgvcIsApplied.ReadOnly = true;
            // 
            // NoRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 357);
            this.Controls.Add(this.dgvNoRuleValues);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NoRuleForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check Applied Rules";
            this.Load += new System.EventHandler(this.NoRuleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNoRuleValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNoRuleValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcIsApplied;



    }
}