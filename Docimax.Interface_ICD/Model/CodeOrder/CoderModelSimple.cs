using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model.CodeOrder
{
    public class CodeOrderModelSimple
    {
        public string CaseNum { get; set; }
        public DateTime OutTime { get; set; }
        public int AdmissionTimes { get; set; }
    }
}
