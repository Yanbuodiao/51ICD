using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model.Log
{
    public class BaseLog
    {
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseStr { get; set; }
        public string LogContent { get; set; }        
    }
}
