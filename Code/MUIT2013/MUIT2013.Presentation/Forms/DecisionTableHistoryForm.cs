using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MUIT2013.Data.Models;
using MUIT2013.Presentation.Shared;
using MUIT2013.Presentation.Shared.Events;
using MUIT2013.Data.ViewModels;

namespace MUIT2013.Presentation.Forms
{
    public partial class DecisionTableHistoryForm : FormBase
    {
        public event SelectDecisionTableHistory SelectDecisionTableHistory;
        public List<DecisionTableHistoryView> DTHistoryViews { get; set; }
        public DecisionTableHistoryForm(List<DecisionTableHistoryView> dtHistoryViews)
        {
            InitializeComponent();
            dgDecisionTableHistory.AutoGenerateColumns = false;
            this.DTHistoryViews = dtHistoryViews;
        }

        private void HandlerTrackerForm_Load(object sender, EventArgs e)
        {
            dgDecisionTableHistory.DataSource = this.DTHistoryViews;
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            if (dgDecisionTableHistory.SelectedRows.Count == 0) return;
            DecisionTableHistoryView decisionTableHistoryView = (DecisionTableHistoryView)dgDecisionTableHistory.SelectedRows[0].DataBoundItem;
            this.Close();
            if (SelectDecisionTableHistory!=null)
            {
                SelectDecisionTableHistory(decisionTableHistoryView, e);
            }            
        }

        private void btnViewData_Click(object sender, EventArgs e)
        {
            if (dgDecisionTableHistory.SelectedRows.Count == 0) return;
            DecisionTableHistoryView decisionTableHistoryView = (DecisionTableHistoryView)dgDecisionTableHistory.SelectedRows[0].DataBoundItem;
            var form = new ViewDataForm(decisionTableHistoryView.TableName);
            //form.MdiParent = this;
            form.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
