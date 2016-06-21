using System.Configuration;

namespace Docimax.Interface_ICD.Configurations
{
    public class SMSConfig : ConfigurationSection
    {
        /// <summary>
        /// 注册时是否需要开启短信验证
        /// </summary>
        [ConfigurationProperty("requireValid", DefaultValue = "true", IsRequired = true)]
        public bool RequireValid
        {
            get
            {
                return (bool)this["requireValid"];
            }
            set
            {
                this["requireValid"] = value;
            }
        }
        /// <summary>
        /// 短信服务器地址
        /// </summary>
        [ConfigurationProperty("server", IsRequired = true)]
        public string Server
        {
            get
            {
                return (string)this["server"];
            }
            set
            {
                this["server"] = value;
            }
        }
        /// <summary>
        /// 在短信平台开通的机构号
        /// </summary>
        [ConfigurationProperty("uid", IsRequired = true)]
        public string Uid
        {
            get
            {
                return (string)this["uid"];
            }
            set
            {
                this["uid"] = value;
            }
        }
        /// <summary>
        /// 在短信平台开通的用户名
        /// </summary>
        [ConfigurationProperty("uname", IsRequired = true)]
        public string UName
        {
            get
            {
                return (string)this["uname"];
            }
            set
            {
                this["uname"] = value;
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        [ConfigurationProperty("pwd", IsRequired = true)]
        public string Pwd
        {
            get
            {
                return (string)this["pwd"];
            }
            set
            {
                this["pwd"] = value;
            }
        }
    }
}