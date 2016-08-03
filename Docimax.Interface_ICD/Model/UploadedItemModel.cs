using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class UploadedItemModel : BaseModel
    {
        public int CodeOrderItemID { get; set; }
        public int CodeOrderID { get; set; }
        public int ItemID { get; set; }
        public int GIndex { get; set; }
        public string ContentType { get; set; }
        public string AttachURL { get; set; }
        public string AttachFileName { get; set; }
    }
}
