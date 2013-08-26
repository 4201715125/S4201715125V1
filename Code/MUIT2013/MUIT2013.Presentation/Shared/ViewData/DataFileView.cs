using MUIT2013.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Presentation.Shared.ViewData
{
    public class DataFileView
    {           
        [Browsable(true)]
        [ReadOnly(true)]
        [Description("determine whether this file is mapped or not")]
        [Category("Configuration")]
        [DisplayName("Is Mapped")]
        public bool IsMapped {
            get
            {
                return this.DataFile.IsMapped;
            }        
        }

        [Browsable(true)]
        [ReadOnly(true)]
        [Description("determine whether this file is activated or not")]
        [DisplayName("Is Activated")]
        [Category("Configuration")]
        public bool IsActivated {
            get
            {
                return this.DataFile.IsActivated;
            }
        }

        [Browsable(true)]
        [Description("description of this file")]
        [DisplayName("Description")]
        [Category("Configuration")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Description {
            get {
                return this.DataFile.Description;
            }
            set {
                this.DataFile.Description = value;
            }
        }

        [Browsable(true)]
        [ReadOnly(true)]
        [Description("")]
        [DisplayName("Name")]
        [Category("General")]
        public string Name
        {
            get
            {
                return this.DataFile.Name;
            }
        }

        [Browsable(false)]
        public DataFile DataFile { get; set; }                 

        public DataFileView (DataFile dataFile)
	    {
            this.DataFile = dataFile;
	    }        
    }
}
