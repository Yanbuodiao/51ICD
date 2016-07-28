﻿using Docimax.Interface_ICD.Enum;
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
        public string ORGCode { get; set; }
        public string ORGName { get; set; }
        public string ORGSubName { get; set; }
        public int ORGID { get; set; }
        public int ORGSubID { get; set; }
        /// <summary>
        /// 需要上传的上传项目列表
        /// </summary>
        public List<ItemModel> ItemList { get; set; }
        /// <summary>
        /// 用户上传的项目列表
        /// </summary>
        public List<UploadedItemModel> UploadedList { get; set; }
    }
}
