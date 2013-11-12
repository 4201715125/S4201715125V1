using MUIT2013.Business;
using MUIT2013.Data.Enums;
using MUIT2013.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUIT2013.Presentation.Forms
{
    public partial class QuickReductResultForm : FormBase
    {
        public DecisionTableHistory DTHistory { get; set; }
        public Digitizer Digitizer{ get; set; }
        public IEnumerable<Reduct> Reducts { get; set; }
        public QuickReductResultForm()
        {
            InitializeComponent();
        }

        public QuickReductResultForm(DecisionTableHistory dtHistory, IEnumerable<Reduct> reducts)
        {
            InitializeComponent();
            this.dgvReductResults.AutoGenerateColumns = false;
            this.DTHistory = dtHistory;
            this.Reducts = reducts;
            this.Digitizer = new Business.Digitizer(dtHistory.DecisionTable);
            txtQuickRedultDescription.Text = this.DTHistory.Decription;
        }

        private void QuickReductResultForm_Load(object sender, EventArgs e)
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn { 
                Caption = "ReductType",
                ColumnName = "ReductType"
            });

            dt.Columns.Add(new DataColumn
            {
                Caption = "Description",
                ColumnName = "Description"
            });

            dt.Columns.Add(new DataColumn
            {                
                ColumnName = "ResultIndex"                
            });

            var groups = this.Reducts.GroupBy(p => new { ReductType = p.ReductType, ResultIndex = p.ResultIndex });
            foreach (var group in groups)
            {
                if (group.FirstOrDefault()!=null)
                {
                    var reduct = group.FirstOrDefault();
                    var attributeNames = string.Join(", ",
                                    Digitizer.Translate(
                                        group.Select(p => p.AttributeIndex).OrderBy(x => x)
                                    ).ToArray()
                                );

                    dt.Rows.Add(
                            group.Key.ReductType,
                            string.Format("{2} Reduct {0} with attributes: {1}", group.Key.ResultIndex, attributeNames, group.Key.ReductType),
                            group.Key.ResultIndex
                        ); // end rows
                }
            }                        
            dgvReductResults.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvReductResults.SelectedRows.Count == 0) return;
            var row = ((DataRowView)dgvReductResults.SelectedRows[0].DataBoundItem).Row;
            var reductType = row[0].ToString();
            var resultIndex = int.Parse(row[2].ToString());
            var reducts = this.Reducts.Where(p => p.ReductType == reductType && p.ResultIndex == resultIndex);
            dataMiningService.SaveQuickReductResult(this.DTHistory, reducts);
            this.Close();            
        }        
    }
}
