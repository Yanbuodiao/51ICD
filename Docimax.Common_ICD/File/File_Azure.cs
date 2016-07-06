using Docimax.Interface_ICD.Interface;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Docimax.Common_ICD.File
{
    public class File_Azure : IFile
    {
        public string Container { get; private set; }
        /// <summary>
        /// 保存用户的附件文件
        /// </summary>
        /// <param name="file">用户上传的文件</param>
        /// <returns>保存后地址</returns>
        public string SaveUserAttachFile(HttpPostedFileBase file)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(Container);

            return string.Empty;
        }
        /// <summary>
        /// 保存用户开通服务时的附件文件
        /// </summary>
        /// <param name="file">用户上传的文件</param>
        /// <returns>保存后地址</returns>
        public string SaveUserServiceAttachFile(HttpPostedFileBase file)
        {
            return string.Empty;
        }
    }
}
