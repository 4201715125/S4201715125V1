namespace MUIT2013.Presentation
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.handlersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discretizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reductsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickReductsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mutualEntropyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aCOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decisionPartitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.approximationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discernibleMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.handlerReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.approximationsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.handlersToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(820, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripMenuItem1,
            this.configurationToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // CreateToolStripMenuItem
            // 
            this.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem";
            this.CreateToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.CreateToolStripMenuItem.Text = "Create";
            this.CreateToolStripMenuItem.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 6);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Enabled = false;
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Enabled = false;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Enabled = false;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(145, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // handlersToolStripMenuItem
            // 
            this.handlersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.discretizationToolStripMenuItem,
            this.reductsToolStripMenuItem,
            this.decisionPartitionToolStripMenuItem,
            this.approximationsToolStripMenuItem,
            this.discernibleMatrixToolStripMenuItem,
            this.toolStripMenuItem3,
            this.handlerReportToolStripMenuItem});
            this.handlersToolStripMenuItem.Name = "handlersToolStripMenuItem";
            this.handlersToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.handlersToolStripMenuItem.Text = "Handlers";
            // 
            // discretizationToolStripMenuItem
            // 
            this.discretizationToolStripMenuItem.Name = "discretizationToolStripMenuItem";
            this.discretizationToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.discretizationToolStripMenuItem.Text = "Discretization";
            // 
            // reductsToolStripMenuItem
            // 
            this.reductsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickReductsToolStripMenuItem,
            this.mutualEntropyToolStripMenuItem,
            this.aCOToolStripMenuItem});
            this.reductsToolStripMenuItem.Name = "reductsToolStripMenuItem";
            this.reductsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.reductsToolStripMenuItem.Text = "Reducts";
            // 
            // quickReductsToolStripMenuItem
            // 
            this.quickReductsToolStripMenuItem.Name = "quickReductsToolStripMenuItem";
            this.quickReductsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.quickReductsToolStripMenuItem.Text = "Quick Reducts";
            this.quickReductsToolStripMenuItem.Click += new System.EventHandler(this.quickReductsToolStripMenuItem_Click);
            // 
            // mutualEntropyToolStripMenuItem
            // 
            this.mutualEntropyToolStripMenuItem.Name = "mutualEntropyToolStripMenuItem";
            this.mutualEntropyToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.mutualEntropyToolStripMenuItem.Text = "Mutual Entropy";
            // 
            // aCOToolStripMenuItem
            // 
            this.aCOToolStripMenuItem.Name = "aCOToolStripMenuItem";
            this.aCOToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.aCOToolStripMenuItem.Text = "ACO";
            // 
            // decisionPartitionToolStripMenuItem
            // 
            this.decisionPartitionToolStripMenuItem.Name = "decisionPartitionToolStripMenuItem";
            this.decisionPartitionToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.decisionPartitionToolStripMenuItem.Text = "Decision Partition";
            // 
            // approximationsToolStripMenuItem
            // 
            this.approximationsToolStripMenuItem.Name = "approximationsToolStripMenuItem";
            this.approximationsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.approximationsToolStripMenuItem.Text = "Approximations";
            this.approximationsToolStripMenuItem.Click += new System.EventHandler(this.approximationsToolStripMenuItem_Click);
            // 
            // discernibleMatrixToolStripMenuItem
            // 
            this.discernibleMatrixToolStripMenuItem.Name = "discernibleMatrixToolStripMenuItem";
            this.discernibleMatrixToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.discernibleMatrixToolStripMenuItem.Text = "Discernible Matrix";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(165, 6);
            // 
            // handlerReportToolStripMenuItem
            // 
            this.handlerReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reductToolStripMenuItem,
            this.approximationsToolStripMenuItem1});
            this.handlerReportToolStripMenuItem.Name = "handlerReportToolStripMenuItem";
            this.handlerReportToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.handlerReportToolStripMenuItem.Text = "Handler Report";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // reductToolStripMenuItem
            // 
            this.reductToolStripMenuItem.Name = "reductToolStripMenuItem";
            this.reductToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.reductToolStripMenuItem.Text = "Reduct";
            this.reductToolStripMenuItem.Click += new System.EventHandler(this.reductToolStripMenuItem_Click);
            // 
            // approximationsToolStripMenuItem1
            // 
            this.approximationsToolStripMenuItem1.Name = "approximationsToolStripMenuItem1";
            this.approximationsToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.approximationsToolStripMenuItem1.Text = "Approximations";
            this.approximationsToolStripMenuItem1.Click += new System.EventHandler(this.approximationsToolStripMenuItem1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 472);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem handlersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem approximationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discretizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reductsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickReductsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mutualEntropyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aCOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decisionPartitionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discernibleMatrixToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem handlerReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reductToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem approximationsToolStripMenuItem1;
    }
}

