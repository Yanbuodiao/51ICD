using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class ICDViewModel
    {
        public int ICDID { get; set; }
        public string ICD_Code { get; set; }
        public string ICD_Name { get; set; }
        public int ICD_VersionID { get; set; }
        public string ICD_VersionName { get; set; }
        public int HitCount { get; set; }
        public string DisplayText { get { return string.Format("{0}-{1}", ICD_Code, ICD_Name); } }
    }
}
