using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class Code_Diagnosis:BaseModel
    {
        public int CodeResultID { get; set; }
        public int CodeOrderID { get; set; }
        public int DiagnosisIndex { get; set; }
        public string ICD_Code { get; set; }
        public string ICD_Content { get; set; }

    }
}
