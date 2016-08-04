using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class Code_Operate : BaseModel
    {
        public int CodeOrderOperateID { get; set; }
        public int CodeOrderID { get; set; }
        public string ICDCode { get; set; }
        public string ICDContent { get; set; }
        public Nullable<System.DateTime> OperateTime { get; set; }
        public string OperateLevel { get; set; }
        public string Opetator { get; set; }
        public string Assistant1 { get; set; }
        public string Assistant2 { get; set; }
        public string Anesthesia { get; set; }
        public string Anesthesiologist { get; set; }
        public Nullable<int> OperateIndex { get; set; }
    }
}
