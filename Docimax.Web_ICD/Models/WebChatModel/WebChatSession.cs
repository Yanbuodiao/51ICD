using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Docimax.Web_ICD.Models
{
    public class WebChatSession
    {
        public string openid { get; set; }
        public string session_key { get; set; }
        public int expires_in { get; set; }

        public string ICD_Session_Key { get; private set; }

        public void InitializeSessionKey()
        {
            ICD_Session_Key = Guid.NewGuid().ToString("N");
        }
    }
}