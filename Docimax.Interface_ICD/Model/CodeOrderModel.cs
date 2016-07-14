using Docimax.Interface_ICD.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class CodeOrderModel
    {
        public int CodeOrderID { get; set; }
        public string PlatformOrderCode { get; set; }
        public string CaseNum { get; set; }
        public ICDOrderState OrderStatus { get; set; }
        public int ORGID { get; set; }
        public int ORGSubID { get; set; }
        public DateTime Createtime { get; set; }
        public string CreateUserID { get; set; }
        public DateTime LastModifyTime { get; set; }
        public string LastModifyUserID { get; set; }
        public int DeleteFlag { get; set; }
        List<UploadItemModel> 
    }
}
