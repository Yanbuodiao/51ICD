using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model.Log
{
    public class BaseLog
    {
        public DateTime GetRequestTime { get; set; }
        public string AUTHCode { get; set; }
        public string TicketID { get; set; }
        public string PlatformOrderCode { get; set; }
        public string RequestIP { get; set; }
        public string UserAgent { get; set; }
        public DateTime ResponseTime { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseStr { get; set; }
        public string LogContent { get; set; }
    }
}
