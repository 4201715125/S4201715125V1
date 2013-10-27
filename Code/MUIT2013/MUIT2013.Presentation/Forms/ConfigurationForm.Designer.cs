namespace MUIT2013.Presentation.Forms
{
    partial class ConfigurationForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpProjectInfo = new System.Windows.Forms.TabPage();
            this.tpProjectConfig = new System.Windows.Forms.TabPage();
            this.dgvAttributeDefinition = new System.Windows.Forms.DataGridView();
            this.dgvcName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcIsCondition = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcIsDecision = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcValidationStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.gbToolBar = new System.Windows.Forms.GroupBox();
            this.btnCreateMapTable = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gvAttributeProperties = new System.Windows.Forms.GroupBox();
            this.pgAttributeDefinition = new System.Windows.Forms.PropertyGrid();
            this.cbColumnType = new System.Windows.Forms.ComboBox();
            this.txtProjectDescription = new System.Windows.Forms.RichTextBox();
            this.lblProjectDescription = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tpProjectInfo.SuspendLayout();
            this.tpProjectConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttributeDefinition)).BeginInit();
            this.gbToolBar.SuspendLayout();
            this.gvAttributeProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpProjectInfo);
            this.tabControl.Controls.Add(this.tpProjectConfig);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(862, 468);
            this.tabControl.TabIndex = 0;
            // 
            // tpProjectInfo
            // 
            this.tpProjectInfo.Controls.Add(this.txtProjectDescription);
            this.tpProjectInfo.Controls.Add(this.lblProjectDescription);
            this.tpProjectInfo.Controls.Add(this.txtProjectName);
            this.tpProjectInfo.Controls.Add(this.lblProjectName);
            this.tpProjectInfo.Location = new System.Drawing.Point(4, 22);
            this.tpProjectInfo.Name = "tpProjectInfo";
            this.tpProjectInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpProjectInfo.Size = new System.Drawing.Size(854, 442);
            this.tpProjectInfo.TabIndex = 0;
            this.tpProjectInfo.Text = "Project Info";
            this.tpProjectInfo.UseVisualStyleBackColor = true;
            // 
            // tpProjectConfig
            // 
            this.tpProjectConfig.Controls.Add(this.dgvAttributeDefinition);
            this.tpProjectConfig.Controls.Add(this.gbToolBar);
            this.tpProjectConfig.Controls.Add(this.gvAttributeProperties);
            this.tpProjectConfig.Location = new System.Drawing.Point(4, 22);
            this.tpProjectConfig.Name = "tpProjectConfig";
            this.tpProjectConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpProjectConfig.Size = new System.Drawing.Size(854, 442);
            this.tpProjectConfig.TabIndex = 1;
            this.tpProjectConfig.Text = "Configuration";
            this.tpProjectConfig.UseVisualStyleBackColor = true;
            // 
            // dgvAttributeDefinition
            // 
            this.dgvAttributeDefinition.AllowUserToAddRows = false;
            this.dgvAttributeDefinition.AllowUserToDeleteRows = false;
            this.dgvAttributeDefinition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttributeDefinition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcName,
            this.dgvcDescription,
            this.dgvcIsCondition,
            this.dgvcIsDecision,
            this.dgvcValidationStatus,
            this.dgvcAction});
            this.dgvAttributeDefinition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttributeDefinition.Location = new System.Drawing.Point(3, 49);
            this.dgvAttributeDefinition.Name = "dgvAttributeDefinition";
            this.dgvAttributeDefinition.ReadOnly = true;
            this.dgvAttributeDefinition.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttributeDefinition.Size = new System.Drawing.Size(565, 390);
            this.dgvAttributeDefinition.TabIndex = 4;
            this.dgvAttributeDefinition.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAttributeDefinition_CellContentClick);
            this.dgvAttributeDefinition.SelectionChanged += new System.EventHandler(this.dgvAttributeDefinition_SelectionChanged);
            // 
            // dgvcName
            // 
            this.dgvcName.DataPropertyName = "RawName";
            this.dgvcName.HeaderText = "Attribute Name";
            this.dgvcName.MinimumWidth = 200;
            this.dgvcName.Name = "dgvcName";
            this.dgvcName.ReadOnly = true;
            this.dgvcName.Width = 300;
            // 
            // dgvcDescription
            // 
            this.dgvcDescription.DataPropertyName = "Description";
            this.dgvcDescription.HeaderText = "Description";
            this.dgvcDescription.Name = "dgvcDescription";
            this.dgvcDescription.ReadOnly = true;
            // 
            // dgvcIsCondition
            // 
            this.dgvcIsCondition.DataPropertyName = "IsIdentifier";
            this.dgvcIsCondition.HeaderText = "Identifier?";
            this.dgvcIsCondition.Name = "dgvcIsCondition";
            this.dgvcIsCondition.ReadOnly = true;
            // 
            // dgvcIsDecision
            // 
            this.dgvcIsDecision.DataPropertyName = "IsDecision";
            this.dgvcIsDecision.HeaderText = "Decision?";
            this.dgvcIsDecision.Name = "dgvcIsDecision";
            this.dgvcIsDecision.ReadOnly = true;
            // 
            // dgvcValidationStatus
            // 
            this.dgvcValidationStatus.DataPropertyName = "ValidationStatus";
            this.dgvcValidationStatus.HeaderText = "Validation Status";
            this.dgvcValidationStatus.Name = "dgvcValidationStatus";
            this.dgvcValidationStatus.ReadOnly = true;
            // 
            // dgvcAction
            // 
            this.dgvcAction.HeaderText = "Action";
            this.dgvcAction.Name = "dgvcAction";
            this.dgvcAction.ReadOnly = true;
            this.dgvcAction.Text = "Check";
            this.dgvcAction.UseColumnTextForButtonValue = true;
            // 
            // gbToolBar
            // 
            this.gbToolBar.Controls.Add(this.btnCreateMapTable);
            this.gbToolBar.Controls.Add(this.btnSave);
            this.gbToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbToolBar.Location = new System.Drawing.Point(3, 3);
            this.gbToolBar.Name = "gbToolBar";
            this.gbToolBar.Size = new System.Drawing.Size(565, 46);
            this.gbToolBar.TabIndex = 5;
            this.gbToolBar.TabStop = false;
            this.gbToolBar.Text = "Tool Bar";
            // 
            // btnCreateMapTable
            // 
            this.btnCreateMapTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateMapTable.Location = new System.Drawing.Point(373, 17);
            this.btnCreateMapTable.Name = "btnCreateMapTable";
            this.btnCreateMapTable.Size = new System.Drawing.Size(105, 23);
            this.btnCreateMapTable.TabIndex = 3;
            this.btnCreateMapTable.Text = "Create Map Table";
            this.btnCreateMapTable.UseVisualStyleBackColor = true;
            this.btnCreateMapTable.Click += new System.EventHandler(this.btnCreateMapTable_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(484, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gvAttributeProperties
            // 
            this.gvAttributeProperties.Controls.Add(this.pgAttributeDefinition);
            this.gvAttributeProperties.Controls.Add(this.cbColumnType);
            this.gvAttributeProperties.Dock = System.Windows.Forms.DockStyle.Right;
            this.gvAttributeProperties.Location = new System.Drawing.Point(568, 3);
            this.gvAttributeProperties.Name = "gvAttributeProperties";
            this.gvAttributeProperties.Size = new System.Drawing.Size(283, 436);
            this.gvAttributeProperties.TabIndex = 3;
            this.gvAttributeProperties.TabStop = false;
            this.gvAttributeProperties.Text = "Attribute Properties";
            // 
            // pgAttributeDefinition
            // 
            this.pgAttributeDefinition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgAttributeDefinition.Location = new System.Drawing.Point(3, 37);
            this.pgAttributeDefinition.Name = "pgAttributeDefinition";
            this.pgAttributeDefinition.Size = new System.Drawing.Size(277, 396);
            this.pgAttributeDefinition.TabIndex = 0;
            this.pgAttributeDefinition.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgAttributeDefinition_PropertyValueChanged);
            // 
            // cbColumnType
            // 
            this.cbColumnType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbColumnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumnType.FormattingEnabled = true;
            this.cbColumnType.Items.AddRange(new object[] {
            "String",
            "Numeric"});
            this.cbColumnType.Location = new System.Drawing.Point(3, 16);
            this.cbColumnType.Name = "cbColumnType";
            this.cbColumnType.Size = new System.Drawing.Size(277, 21);
            this.cbColumnType.TabIndex = 2;
            this.cbColumnType.SelectedValueChanged += new System.EventHandler(this.cbColumnType_SelectedValueChanged);
            // 
            // txtProjectDescription
            // 
            this.txtProjectDescription.Location = new System.Drawing.Point(286, 144);
            this.txtProjectDescription.Name = "txtProjectDescription";
            this.txtProjectDescription.ReadOnly = true;
            this.txtProjectDescription.Size = new System.Drawing.Size(240, 86);
            this.txtProjectDescription.TabIndex = 16;
            this.txtProjectDescription.Text = "";
            // 
            // lblProjectDescription
            // 
            this.lblProjectDescription.AutoSize = true;
            this.lblProjectDescription.Location = new System.Drawing.Point(157, 144);
            this.lblProjectDescription.Name = "lblProjectDescription";
            this.lblProjectDescription.Size = new System.Drawing.Size(96, 13);
            this.lblProjectDescription.TabIndex = 15;
            this.lblProjectDescription.Text = "Project Description";
            // 
            // lblProjectName
            // 
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.Location = new System.Drawing.Point(157, 115);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(71, 13);
            this.lblProjectName.TabIndex = 9;
            this.lblProjectName.Text = "Project Name";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(286, 112);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(240, 20);
            this.txtProjectName.TabIndex = 10;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 468);
            this.Controls.Add(this.tabControl);
            this.Name = "ConfigurationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tpProjectInfo.ResumeLayout(false);
            this.tpProjectInfo.PerformLayout();
            this.tpProjectConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttributeDefinition)).EndInit();
            this.gbToolBar.ResumeLayout(false);
            this.gvAttributeProperties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpProjectInfo;
        private System.Windows.Forms.TabPage tpProjectConfig;
        private System.Windows.Forms.PropertyGrid pgAttributeDefinition;
        private System.Windows.Forms.GroupBox gvAttributeProperties;
        private System.Windows.Forms.DataGridView dgvAttributeDefinition;
        private System.Windows.Forms.GroupBox gbToolBar;
        private System.Windows.Forms.ComboBox cbColumnType;
        private System.Windows.Forms.Button btnCreateMapTable;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcDescription;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcIsCondition;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcIsDecision;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcValidationStatus;
        private System.Windows.Forms.DataGridViewButtonColumn dgvcAction;
        private System.Windows.Forms.RichTextBox txtProjectDescription;
        private System.Windows.Forms.Label lblProjectDescription;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label lblProjectName;
    }
}