using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class ICDVersionModel
    {
        public int ICD_VersionID { get; set; }
        public string ICD_VersionName { get; set; }
        public string ICD_Description { get; set; }
        public int ICD_Type { get; set; }

        public List<ICDModel> ICDList { get; set; }
    }
}
