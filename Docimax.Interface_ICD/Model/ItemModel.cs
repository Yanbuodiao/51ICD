using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class ItemModel
    {
        #region 配置的上传项目属性

        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string PinyinShort { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<int> ItemIndex { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateUserID { get; set; }
        public List<ItemModel> ChildrenList { get; set; }

        #endregion

        #region 具体一个上传项目的属性
        public List<UploadedItemModel> UploadedItemList { get; set; }

        public string UpLoadedFileName
        {
            get
            {
                if (UploadedItemList != null)
                {
                    string.Join(";", UploadedItemList.Select(e => e.AttachFileName));
                }
                return string.Empty;
            }
        }

        #endregion
    }
}
