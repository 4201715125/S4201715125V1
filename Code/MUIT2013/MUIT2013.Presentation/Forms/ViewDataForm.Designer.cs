﻿namespace MUIT2013.Presentation.Forms
{
    partial class ViewDataForm
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
            this.dgvGridData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGridData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGridData
            // 
            this.dgvGridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGridData.Location = new System.Drawing.Point(0, 0);
            this.dgvGridData.Name = "dgvGridData";
            this.dgvGridData.Size = new System.Drawing.Size(798, 429);
            this.dgvGridData.TabIndex = 0;
            // 
            // ViewDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 429);
            this.Controls.Add(this.dgvGridData);
            this.Name = "ViewDataForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "View Data";
            this.Load += new System.EventHandler(this.ViewDataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGridData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGridData;
    }
}