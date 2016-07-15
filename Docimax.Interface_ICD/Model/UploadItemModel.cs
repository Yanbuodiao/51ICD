using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class UploadItemModel
    {
        public int UploadItemID { get; set; }
        public string UploadItemName { get; set; }
        public string UploadItemDescription { get; set; }
        public string PinyinShort { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<int> UploadItemIndex { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateUserID { get; set; }
        public List<UploadItemModel> ChildrenList { get; set; }
    }
}
