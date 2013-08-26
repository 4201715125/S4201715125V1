using MUIT2013.Data.Models;
using MUIT2013.Data.ViewModels;
using MUIT2013.DataMining.AttributeRule;
using MUIT2013.Presentation.Shared;
using MUIT2013.Presentation.Shared.Editor;
using MUIT2013.Presentation.Shared.ViewData;
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
    public partial class ConfigurationForm : FormBase
    {
        public List<ColumnDefinition> ColumnDefinitionCollection;
        private ColumnDefinitionViewFactory cdvFactory;
        public ConfigurationForm()
        {
            InitializeComponent();
            dgvColumnDefinition.AutoGenerateColumns = false;
            ActivateDataFile = dataFileService.GetActivedDataFile();
            cdvFactory = new ColumnDefinitionViewFactory();
        }

        #region Events
        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            if (ActivateDataFile == null)
            {
                tabControl.TabPages.Remove(tpProjectConfig);
            }
            else
            {
                LoadColumnDefinitions();
            }
        }

        private void dgvColumnDefinition_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvColumnDefinition.SelectedRows.Count == 0) return;
            ColumnDefinition selectedColumnDefinition = (ColumnDefinition)dgvColumnDefinition.SelectedRows[0].DataBoundItem;
            cbColumnType.SelectedItem = selectedColumnDefinition.ColumnType;
            LoadColumnProperties();
        }

        private void cbColumnType_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadColumnProperties();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        #endregion

        #region Initialize data
        private void LoadColumnDefinitions()
        {
            if (ActivateDataFile!=null)
            {
                dgvColumnDefinition.DataSource = this.ColumnDefinitionCollection = columnDefinitionService.GetList(ActivateDataFile.Id);                 
            }            
        }

        private void LoadColumnProperties()
        {            
            if (dgvColumnDefinition.SelectedRows.Count == 0) return;
            var columnType = cbColumnType.SelectedItem as string;
            if (string.IsNullOrEmpty(columnType)) return;
            ColumnDefinition selectedColumnDefinition = (ColumnDefinition)dgvColumnDefinition.SelectedRows[0].DataBoundItem;
            selectedColumnDefinition.ColumnType = columnType;
            pgColumnDefinition.SelectedObject = cdvFactory.Create(selectedColumnDefinition);            
        }

        private void Save()
        {
            this.ColumnDefinitionCollection.ForEach(p =>
            {
                var columnDefinitionView = cdvFactory.Create(p);
                p.MapRules = new List<MapRule>();
                if (columnDefinitionView is StringRuleColumnDefinitionView)
                {
                    var cdv = columnDefinitionView as StringRuleColumnDefinitionView;
                    cdv.RuleCollection.ForEach(k =>
                    {
                        p.MapRules.Add(new MapRule
                        {
                            ColumnDefinitionId = p.Id,
                            RuleType = k.GetRuleType(),
                            RuleContent = k.ToSerialize()
                        });
                    });
                }
                else if (columnDefinitionView is NumericRuleColumnDefinitionView)
                {
                    var cdv = columnDefinitionView as NumericRuleColumnDefinitionView;
                    cdv.RuleCollection.ForEach(k =>
                    {
                        p.MapRules.Add(new MapRule
                        {
                            ColumnDefinitionId = p.Id,
                            RuleType = k.GetRuleType(),
                            RuleContent = k.ToSerialize()
                        });
                    });
                }

            });

            columnDefinitionService.BulkUpdate(this.ColumnDefinitionCollection);
        }
        #endregion

        private void btnCreateMapTable_Click(object sender, EventArgs e)
        {
            List<ColumnDefinition> invalidColumnDefinitions = new List<ColumnDefinition>();
            for (int i = 0; i < this.ColumnDefinitionCollection.Count; i++)
            {
                var p = this.ColumnDefinitionCollection[i];
                var columnDefinitionView = cdvFactory.Create(p);
                bool check = false;
                if (columnDefinitionView is StringRuleColumnDefinitionView)
                {
                    var cdv = columnDefinitionView as StringRuleColumnDefinitionView;
                    check = dataService.CheckStringRuleInValidationStatus(ActivateDataFile, p, cdv.RuleCollection.ToList());
                }
                else if (columnDefinitionView is NumericRuleColumnDefinitionView)
                {
                    var cdv = columnDefinitionView as NumericRuleColumnDefinitionView;
                    check = dataService.CheckNumericRuleInValidationStatus(ActivateDataFile, p, cdv.RuleCollection.ToList());
                }
                p.ValidationStatus = check ? "Valid" : "Invalid";
                if (!check) invalidColumnDefinitions.Add(p);                
            }
            if (invalidColumnDefinitions.Count>0)
            {
                string message = string.Join(", ", invalidColumnDefinitions.Select(p => p.Name));
                MessageBox.Show(string.Format("Some columns have been applied invalid rules. Please correct them to create map table again."), "Error", MessageBoxButtons.OK);
                return;
            }
            Save();
            dataService.CreateMapTable(ActivateDataFile, this.ColumnDefinitionCollection);
            this.dgvColumnDefinition.Refresh();  
        }

        private void dgvColumnDefinition_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
            if (e.ColumnIndex == dgvColumnDefinition.Columns["dgvcAction"].Index && e.RowIndex >= 0)
            {
                var columnDefinition = (ColumnDefinition)dgvColumnDefinition.Rows[e.RowIndex].DataBoundItem;
                var columnDefinitionView = cdvFactory.Create(columnDefinition);
                IEnumerable<AppliedRuleValue> appliedRuleValues = null;
                if (columnDefinitionView is StringRuleColumnDefinitionView)
                {
                    var cdv = columnDefinitionView as StringRuleColumnDefinitionView;
                    appliedRuleValues = dataService.GetValuesWithNoRuleAppliedInStringRule(ActivateDataFile, columnDefinition, cdv.RuleCollection.ToList());
                }
                else if (columnDefinitionView is NumericRuleColumnDefinitionView)
                {
                    var cdv = columnDefinitionView as NumericRuleColumnDefinitionView;
                    appliedRuleValues = dataService.GetValuesWithNoRuleAppliedInNumericRule(ActivateDataFile, columnDefinition, cdv.RuleCollection.ToList());
                }

                using (var form = new NoRuleForm(appliedRuleValues))
                {
                    form.ShowDialog();
                }
                
            }
        }

        private void pgColumnDefinition_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            dgvColumnDefinition.Refresh();
        }

       
    }
}
