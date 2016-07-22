using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class ItemModel
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string PinyinShort { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<int> ItemIndex { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateUserID { get; set; }
        public List<ItemModel> ChildrenList { get; set; }
    }
}
