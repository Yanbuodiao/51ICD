using System;

namespace Docimax.Interface_ICD.Model
{
    public class BaseModel
    {
        public Nullable<DateTime> CreateTime { get; set; }
        public string CreateUserID { get; set; }
        public Nullable<DateTime> LastModifyTime { get; set; }
        public string LastModifyUserID { get; set; }
        public Nullable<int> DeleteFlag { get; set; }
        public byte[] LastModifyStamp { get; set; }
    }
}
