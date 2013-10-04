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
        public List<AttributeDefinition> AttributeDefinitionCollection;
        private AttributeDefinitionViewFactory cdvFactory;
        public ConfigurationForm()
        {
            InitializeComponent();
            dgvAttributeDefinition.AutoGenerateColumns = false;
            ActivateDataFile = dataFileService.GetActivedDataFile();
            cdvFactory = new AttributeDefinitionViewFactory();
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
                LoadAttributeDefinitions();
            }
        }

        private void dgvAttributeDefinition_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAttributeDefinition.SelectedRows.Count == 0) return;
            AttributeDefinition selectedAttributeDefinition = (AttributeDefinition)dgvAttributeDefinition.SelectedRows[0].DataBoundItem;
            cbColumnType.SelectedItem = selectedAttributeDefinition.ColumnType;
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
        private void LoadAttributeDefinitions()
        {
            if (ActivateDataFile!=null)
            {
                dgvAttributeDefinition.DataSource = this.AttributeDefinitionCollection = AttributeDefinitionService.GetList(ActivateDataFile.Id);                 
            }            
        }

        private void LoadColumnProperties()
        {            
            if (dgvAttributeDefinition.SelectedRows.Count == 0) return;
            var columnType = cbColumnType.SelectedItem as string;
            if (string.IsNullOrEmpty(columnType)) return;
            AttributeDefinition selectedAttributeDefinition = (AttributeDefinition)dgvAttributeDefinition.SelectedRows[0].DataBoundItem;
            selectedAttributeDefinition.ColumnType = columnType;
            pgAttributeDefinition.SelectedObject = cdvFactory.Create(selectedAttributeDefinition);            
        }

        private void Save()
        {
            this.AttributeDefinitionCollection.ForEach(p =>
            {
                var AttributeDefinitionView = cdvFactory.Create(p);
                p.MapRules = new List<MapRule>();
                if (AttributeDefinitionView is StringRuleAttributeDefinitionView)
                {
                    var cdv = AttributeDefinitionView as StringRuleAttributeDefinitionView;
                    cdv.RuleCollection.ForEach(k =>
                    {
                        p.MapRules.Add(new MapRule
                        {
                            AttributeDefinitionId = p.Id,
                            RuleType = k.GetRuleType(),
                            RuleContent = k.ToSerialize()
                        });
                    });
                }
                else if (AttributeDefinitionView is NumericRuleAttributeDefinitionView)
                {
                    var cdv = AttributeDefinitionView as NumericRuleAttributeDefinitionView;
                    cdv.RuleCollection.ForEach(k =>
                    {
                        p.MapRules.Add(new MapRule
                        {
                            AttributeDefinitionId = p.Id,
                            RuleType = k.GetRuleType(),
                            RuleContent = k.ToSerialize()
                        });
                    });
                }

            });

            AttributeDefinitionService.BulkUpdate(this.AttributeDefinitionCollection);
        }
        #endregion

        private void btnCreateMapTable_Click(object sender, EventArgs e)
        {
            List<AttributeDefinition> invalidAttributeDefinitions = new List<AttributeDefinition>();
            for (int i = 0; i < this.AttributeDefinitionCollection.Count; i++)
            {
                var p = this.AttributeDefinitionCollection[i];
                var adv = cdvFactory.Create(p);
                if (adv.IsAutoEncoding)
                {
                    continue;
                }
                bool check = false;
                if (adv is StringRuleAttributeDefinitionView)
                {
                    var cdv = adv as StringRuleAttributeDefinitionView;
                    check = dataService.CheckStringRuleInValidationStatus(ActivateDataFile, p, cdv.RuleCollection.ToList());
                }
                else if (adv is NumericRuleAttributeDefinitionView)
                {
                    var cdv = adv as NumericRuleAttributeDefinitionView;
                    check = dataService.CheckNumericRuleInValidationStatus(ActivateDataFile, p, cdv.RuleCollection.ToList());
                }
                p.ValidationStatus = check ? "Valid" : "Invalid";
                if (!check) invalidAttributeDefinitions.Add(p);                
            }
            if (invalidAttributeDefinitions.Count>0)
            {
                string message = string.Join(", ", invalidAttributeDefinitions.Select(p => p.Name));
                MessageBox.Show(string.Format("Some attributes have been applied invalid rules. Please correct them to create map table again."), "Error", MessageBoxButtons.OK);
                return;
            }
            Save();
            dataService.CreateMapTable(ActivateDataFile, this.AttributeDefinitionCollection);
            this.dgvAttributeDefinition.Refresh();  
        }

        private void dgvAttributeDefinition_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
            if (e.ColumnIndex == dgvAttributeDefinition.Columns["dgvcAction"].Index && e.RowIndex >= 0)
            {
                var AttributeDefinition = (AttributeDefinition)dgvAttributeDefinition.Rows[e.RowIndex].DataBoundItem;
                var AttributeDefinitionView = cdvFactory.Create(AttributeDefinition);
                IEnumerable<AppliedRuleValue> appliedRuleValues = null;
                if (AttributeDefinitionView is StringRuleAttributeDefinitionView)
                {
                    var cdv = AttributeDefinitionView as StringRuleAttributeDefinitionView;
                    appliedRuleValues = dataService.GetValuesWithNoRuleAppliedInStringRule(ActivateDataFile, AttributeDefinition, cdv.RuleCollection.ToList());
                }
                else if (AttributeDefinitionView is NumericRuleAttributeDefinitionView)
                {
                    var cdv = AttributeDefinitionView as NumericRuleAttributeDefinitionView;
                    appliedRuleValues = dataService.GetValuesWithNoRuleAppliedInNumericRule(ActivateDataFile, AttributeDefinition, cdv.RuleCollection.ToList());
                }

                using (var form = new NoRuleForm(appliedRuleValues))
                {
                    form.ShowDialog();
                }
                
            }
        }

        private void pgAttributeDefinition_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            dgvAttributeDefinition.Refresh();
        }

       
    }
}
