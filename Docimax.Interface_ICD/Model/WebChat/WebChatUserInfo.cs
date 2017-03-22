using System;

namespace Docimax.Interface_ICD.Model
{
    public class WebChatUserInfo
    {
        public string avatarUrl { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public int gender { get; set; }
        public string nickName { get; set; }
        public string province { get; set; }
        public string openId { get; set; }
        public string unionId { get; set; }

        public WebChatWaterMark watermark { get; set; }
    }

    public class WebChatUserLoginInfo
    {
        public string encryptedData { get; set; }
        public string iv { get; set; }
        public string rawData { get; set; }
        public string signature { get; set; }
        public WebChatUserInfo userInfo { get; set; }
    }

    public class WebChatWaterMark
    {
        public string appid { get; set; }
        public double timestamp { get; set; }

        public DateTime WebChatDateTime
        {
            get
            {
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(timestamp).ToLocalTime();
                return dtDateTime;
            }
        }
    }
}
