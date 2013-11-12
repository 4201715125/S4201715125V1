namespace MUIT2013.Presentation.Forms
{
    partial class ImportForm
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
            this.btnUpload = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dgvDataFile = new System.Windows.Forms.DataGridView();
            this.dgvcName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcCreatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcIsActivated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcIsMapped = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cmsDataFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewRawDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMapDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnActivate = new System.Windows.Forms.Button();
            this.pgDataFile = new System.Windows.Forms.PropertyGrid();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnViewMapData = new System.Windows.Forms.Button();
            this.btnViewRawData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataFile)).BeginInit();
            this.cmsDataFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(472, 12);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // dgvDataFile
            // 
            this.dgvDataFile.AllowUserToAddRows = false;
            this.dgvDataFile.AllowUserToDeleteRows = false;
            this.dgvDataFile.AllowUserToOrderColumns = true;
            this.dgvDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDataFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcName,
            this.dgvcDescription,
            this.dgvcCreatedDate,
            this.dgvcIsActivated,
            this.dgvcIsMapped});
            this.dgvDataFile.ContextMenuStrip = this.cmsDataFile;
            this.dgvDataFile.Location = new System.Drawing.Point(12, 41);
            this.dgvDataFile.MultiSelect = false;
            this.dgvDataFile.Name = "dgvDataFile";
            this.dgvDataFile.ReadOnly = true;
            this.dgvDataFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDataFile.Size = new System.Drawing.Size(744, 370);
            this.dgvDataFile.TabIndex = 1;
            this.dgvDataFile.SelectionChanged += new System.EventHandler(this.dgvDataFile_SelectionChanged);
            // 
            // dgvcName
            // 
            this.dgvcName.DataPropertyName = "Name";
            this.dgvcName.Frozen = true;
            this.dgvcName.HeaderText = "Name";
            this.dgvcName.MinimumWidth = 100;
            this.dgvcName.Name = "dgvcName";
            this.dgvcName.ReadOnly = true;
            this.dgvcName.Width = 200;
            // 
            // dgvcDescription
            // 
            this.dgvcDescription.DataPropertyName = "Description";
            this.dgvcDescription.HeaderText = "Description";
            this.dgvcDescription.MinimumWidth = 200;
            this.dgvcDescription.Name = "dgvcDescription";
            this.dgvcDescription.ReadOnly = true;
            this.dgvcDescription.Width = 300;
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
            // dgvcIsActivated
            // 
            this.dgvcIsActivated.DataPropertyName = "IsActivated";
            this.dgvcIsActivated.HeaderText = "Activated?";
            this.dgvcIsActivated.Name = "dgvcIsActivated";
            this.dgvcIsActivated.ReadOnly = true;
            this.dgvcIsActivated.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvcIsActivated.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dgvcIsMapped
            // 
            this.dgvcIsMapped.DataPropertyName = "IsMapped";
            this.dgvcIsMapped.HeaderText = "Mapped?";
            this.dgvcIsMapped.Name = "dgvcIsMapped";
            this.dgvcIsMapped.ReadOnly = true;
            // 
            // cmsDataFile
            // 
            this.cmsDataFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewRawDataToolStripMenuItem,
            this.viewMapDataToolStripMenuItem});
            this.cmsDataFile.Name = "cmsDataFile";
            this.cmsDataFile.Size = new System.Drawing.Size(154, 48);
            // 
            // viewRawDataToolStripMenuItem
            // 
            this.viewRawDataToolStripMenuItem.Name = "viewRawDataToolStripMenuItem";
            this.viewRawDataToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.viewRawDataToolStripMenuItem.Text = "View Raw Data";
            // 
            // viewMapDataToolStripMenuItem
            // 
            this.viewMapDataToolStripMenuItem.Name = "viewMapDataToolStripMenuItem";
            this.viewMapDataToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.viewMapDataToolStripMenuItem.Text = "View Map Data";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(391, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.Color.LightGray;
            this.txtFileName.Enabled = false;
            this.txtFileName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFileName.Location = new System.Drawing.Point(12, 15);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(373, 20);
            this.txtFileName.TabIndex = 3;
            // 
            // btnActivate
            // 
            this.btnActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivate.Location = new System.Drawing.Point(933, 12);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(75, 23);
            this.btnActivate.TabIndex = 4;
            this.btnActivate.Text = "Activate";
            this.btnActivate.UseVisualStyleBackColor = true;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // pgDataFile
            // 
            this.pgDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgDataFile.Location = new System.Drawing.Point(762, 41);
            this.pgDataFile.Name = "pgDataFile";
            this.pgDataFile.Size = new System.Drawing.Size(249, 369);
            this.pgDataFile.TabIndex = 5;
            this.pgDataFile.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgDataFile_PropertyValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(852, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnViewMapData
            // 
            this.btnViewMapData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewMapData.Location = new System.Drawing.Point(742, 13);
            this.btnViewMapData.Name = "btnViewMapData";
            this.btnViewMapData.Size = new System.Drawing.Size(104, 23);
            this.btnViewMapData.TabIndex = 7;
            this.btnViewMapData.Text = "View Map Data";
            this.btnViewMapData.UseVisualStyleBackColor = true;
            this.btnViewMapData.Click += new System.EventHandler(this.btnViewMapData_Click);
            // 
            // btnViewRawData
            // 
            this.btnViewRawData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewRawData.Location = new System.Drawing.Point(637, 12);
            this.btnViewRawData.Name = "btnViewRawData";
            this.btnViewRawData.Size = new System.Drawing.Size(99, 23);
            this.btnViewRawData.TabIndex = 8;
            this.btnViewRawData.Text = "View Raw Data";
            this.btnViewRawData.UseVisualStyleBackColor = true;
            this.btnViewRawData.Click += new System.EventHandler(this.btnViewRawData_Click);
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 422);
            this.Controls.Add(this.btnViewRawData);
            this.Controls.Add(this.btnViewMapData);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pgDataFile);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.dgvDataFile);
            this.Controls.Add(this.btnUpload);
            this.Name = "ImportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import";
            this.Load += new System.EventHandler(this.ImportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataFile)).EndInit();
            this.cmsDataFile.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpload;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dgvDataFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.ContextMenuStrip cmsDataFile;
        private System.Windows.Forms.ToolStripMenuItem viewRawDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMapDataToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid pgDataFile;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnViewMapData;
        private System.Windows.Forms.Button btnViewRawData;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcCreatedDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcIsActivated;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcIsMapped;
    }
}