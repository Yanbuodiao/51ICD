using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class CodeBuildModel
    {
        public int BuilderCodeID { get; set; }
        public string PlatformOrderCode { get; set; }
        public string CaseNum { get; set; }
        public Nullable<int> OrderStatus { get; set; }
        public Nullable<int> ORGID { get; set; }
        public Nullable<int> ORGSubID { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public string CreateUserID { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
        public string LastModifyUserID { get; set; }
        public Nullable<int> DeleteFlag { get; set; }
    }
}
