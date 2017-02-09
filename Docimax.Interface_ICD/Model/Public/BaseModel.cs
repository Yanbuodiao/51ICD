using Newtonsoft.Json;
using System;

namespace Docimax.Interface_ICD.Model
{
    public class BaseModel
    {
        [JsonIgnore]
        public Nullable<DateTime> CreateTime { get; set; }
        [JsonIgnore]
        public string CreateUserID { get; set; }
        [JsonIgnore]
        public Nullable<DateTime> LastModifyTime { get; set; }
        [JsonIgnore]
        public string LastModifyUserID { get; set; }
        [JsonIgnore]
        public Nullable<int> DeleteFlag { get; set; }
        [JsonIgnore]
        public string LastModifyStamp { get; set; }
    }
}
