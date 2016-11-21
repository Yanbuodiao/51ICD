using Docimax.Interface_ICD.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class CodeOrderModel : BaseModel
    {
        public int CodeOrderID { get; set; }
        public string PlatformOrderCode { get; set; }
        [Required]
        [Display(Name = "病案号")]
        public string CaseNum { get; set; }
        public ICDOrderState OrderStatus { get; set; }
        public OrderTypeEnum OrderType { get; set; }
        public string ORGCode { get; set; }
        public string ORGName { get; set; }
        public string ORGSubName { get; set; }
        public int ORGID { get; set; }
        public int ORGSubID { get; set; }
        public DateTime PickedTime { get; set; }
        public string PickedUserID { get; set; }
        public int ServiceID { get; set; }
        /// <summary>
        /// 需要上传的上传项目列表
        /// </summary>
        public List<ItemModel> ItemList { get; set; }
        /// <summary>
        /// 用户上传的项目列表
        /// </summary>
        public List<UploadedItemModel> UploadedList { get; set; }
        public List<Code_Diagnosis> DiagnosisList { get; set; }
        public List<Code_Operate> OperateList { get; set; }
    }
}
