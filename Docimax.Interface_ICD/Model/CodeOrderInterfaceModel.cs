using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class CodeOrderInterfaceModel : BaseModel
    {
        public int CodeOrderID { get; set; }
        public string PlatformOrderCode { get; set; }
        public string CaseNum { get; set; }
        public string MedicalRecordPath { get; set; }
    }
}
