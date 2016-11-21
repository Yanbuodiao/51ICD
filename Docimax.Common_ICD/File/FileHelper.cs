using Docimax.Interface_ICD.Interface;
using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Docimax.Common_ICD.File
{
    /// <summary>
    /// 平台文件保存类
    /// </summary>
    public static class FileHelper
    {
        #region 私有变量
        private static IFile fileAccess { get; set; }

        private static string userAttachFileDirectory;
        private static string UserAttachFileDirectory
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(userAttachFileDirectory))
                {
                    return userAttachFileDirectory;
                }
                userAttachFileDirectory = CloudConfigurationManager.GetSetting("userAttachFileDirectory");
                if (string.IsNullOrWhiteSpace(userAttachFileDirectory))
                {
                    userAttachFileDirectory = "51icd-sysblob/user-attach/";
                }
                return userAttachFileDirectory;
            }
        }

        private static string userServiceAttachFileDirectory;
        private static string UserServiceAttachFileDirectory
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(userServiceAttachFileDirectory))
                {
                    return userServiceAttachFileDirectory;
                }
                userServiceAttachFileDirectory = CloudConfigurationManager.GetSetting("userAttachFileDirectory");
                if (string.IsNullOrWhiteSpace(userServiceAttachFileDirectory))
                {
                    userServiceAttachFileDirectory = "51icd-sysblob/user-service-attach/";
                }
                return userServiceAttachFileDirectory;
            }
        }

        static FileHelper()
        {
            fileAccess = new File_Azure();//目前用的微软云的服务
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 保存用户信息相关的附件
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <returns>保存后的文件地址</returns>
        public static string SaveUserAttachFile(HttpPostedFileBase file)
        {
            return fileAccess.SaveFile(file, UserAttachFileDirectory);
        }
        /// <summary>
        /// 保存用户服务相关信息
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <returns>文件保存后的目录（含文件名）</returns>
        public static string SaveUserServiceAttachFile(HttpPostedFileBase file)
        {
            return fileAccess.SaveFile(file, UserServiceAttachFileDirectory);
        }
        /// <summary>
        /// 保存病案相关文件
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <param name="organizationCode">医院的机构代码</param>
        /// <param name="medicalRecord">文件所属病案号</param>
        /// <returns>上传后文件地址</returns>
        public static string SaveMedicalRecord(HttpPostedFileBase file, string organizationCode, string medicalRecord)
        {
            return fileAccess.SaveFile(file, string.Format("{0}/{1}/", organizationCode, medicalRecord));
        }
        /// <summary>
        /// 从指定的地址下载图片文件
        /// </summary>
        /// <param name="fileURL">要查看的文件路径（虚拟路径）</param>
        /// <returns>返回文件字节流</returns>
        public static byte[] GetPicFile(string fileURL)
        {
            return fileAccess.GetPicFile(fileURL);
        }
        /// <summary>
        /// 从指定的地址下载文件
        /// </summary>
        /// <param name="fileURL">要查看的文件路径（虚拟路径）</param>
        /// <returns>返回文件字节流</returns>
        public static byte[] GetFile(string fileURL)
        {
            return fileAccess.GetFile(fileURL);
        }

        #endregion
    }
}
