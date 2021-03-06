﻿using MUIT2013.Business;
using MUIT2013.Data.Enums;
using MUIT2013.Data.Models;
using MUIT2013.Data.ViewModels;
using MUIT2013.Presentation.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUIT2013.Presentation
{
    public partial class MainForm : Form
    {
        private ProjectService projectService;        
        public MainForm()
        {
            InitializeComponent();
            ActivateMenuItems(false);
            projectService = new ProjectService();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ConfigurationForm();
            form.MdiParent = this;
            form.Show();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ImportForm();
            form.MdiParent = this;
            form.Show();
        }

        #region Connect to Project
        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new CreateProjectForm();
            form.MdiParent = this;
            form.ProjectCreated += ProjectConnected;
            form.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ListProjectsForm();
            form.MdiParent = this;
            form.ProjectOpened += ProjectConnected;
            form.Show();
        }

        void ProjectConnected(object sender, EventArgs e)
        {
            ActivateMenuItems(true);
        }
        #endregion
     
        #region Control Menu Items
        private void ActivateMenuItems(bool IsActivated)
        {
            configurationToolStripMenuItem.Enabled = IsActivated;
            importToolStripMenuItem.Enabled = IsActivated;
            exportToolStripMenuItem.Enabled = IsActivated;
            handlersToolStripMenuItem.Enabled = IsActivated;
        }
        #endregion

        private void approximationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var decisionTableHistoryService = new DecisionTableHistoryService();
            var dtHistoryViews = decisionTableHistoryService.GetList(p => p.Action == EDTHistoryAction.QuickReduct);
            var form = new DecisionTableHistoryForm(dtHistoryViews);
            form.MdiParent = this;
            form.SelectDecisionTableHistory += formApproximation_SelectHandlerTracker;
            form.Show();
        }

        void formApproximation_SelectHandlerTracker(object sender, EventArgs e)
        {
            var decisionTableHistoryView = (DecisionTableHistoryView)sender;
            DataMiningService service = new DataMiningService();
            DataFileService dataFileService = new DataFileService();
            AttributeDefinitionService attributeDefinitionService = new AttributeDefinitionService();                        
            
            var activateDataFile = dataFileService.GetActivedDataFile();            
            var attributeDefs = attributeDefinitionService.GetList(activateDataFile.Id);

            service.Approximation(attributeDefs, decisionTableHistoryView.Id);
            MessageBox.Show("Run Approximation successfully", "Approximation");
            //var quickReductResult = service.QuickReduct(attributeDefs, decisionTableHistoryView.Id);
            //var dtHistory = quickReductResult.Item1;
            //var reducts = quickReductResult.Item2;
            //var form = new QuickReductResultForm(dtHistory, reducts);
            //form.MdiParent = this;
            //form.Show();
        }

        #region Quick Reduct
        private void quickReductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var decisionTableHistoryService = new DecisionTableHistoryService();
            var dtHistoryViews = decisionTableHistoryService.GetList(p => p.Action == EDTHistoryAction.CreateMapTable);
            var form = new DecisionTableHistoryForm(dtHistoryViews);
            form.MdiParent = this;

            form.SelectDecisionTableHistory += formQuickReduct_SelectHandlerTracker;
            form.Show();
        }

        void formQuickReduct_SelectHandlerTracker(object sender, EventArgs e)
        {
            var decisionTableHistoryView = (DecisionTableHistoryView)sender;
            DataMiningService service = new DataMiningService();
            DataFileService dataFileService = new DataFileService();
            AttributeDefinitionService attributeDefinitionService = new AttributeDefinitionService();

            var activateDataFile = dataFileService.GetActivedDataFile();
            var attributeDefs = attributeDefinitionService.GetList(activateDataFile.Id);
            var quickReductResult = service.QuickReduct(attributeDefs, decisionTableHistoryView.Id);
            var dtHistory = quickReductResult.Item1;
            var reducts = quickReductResult.Item2;
            var form = new QuickReductResultForm(dtHistory, reducts);
            form.MdiParent = this;
            form.Show();            
        }
        #endregion

        private void reductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var decisionTableHistoryService = new DecisionTableHistoryService();
            var dtHistoryViews = decisionTableHistoryService.GetList(p => p.Action == EDTHistoryAction.QuickReduct);
            var form = new DecisionTableHistoryForm(dtHistoryViews);
            form.MdiParent = this;            
            form.Show();
        }

        private void approximationsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var decisionTableHistoryService = new DecisionTableHistoryService();
            var dtHistoryViews = decisionTableHistoryService.GetList(p => p.Action == EDTHistoryAction.Approximation);
            var form = new DecisionTableHistoryForm(dtHistoryViews);
            form.MdiParent = this;
            form.Show();
        }
    }
}
