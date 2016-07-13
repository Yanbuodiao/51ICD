using Docimax.Interface_ICD.Interface;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Web;

namespace Docimax.Common_ICD.File
{
    public class File_Azure : IFile
    {
        //本平台的文件所在的容器名称
        private string Container { get { return CloudConfigurationManager.GetSetting("51ICDContainer"); } }
        /// <summary>
        /// 保存上传的文件，物理保存
        /// </summary>
        /// <param name="file">通过MVC上传的文件</param>
        /// <param name="virtualDirectory">要保存到的文件目录（不含文件名称）</param>
        /// <returns>文件保存后返回的地址</returns>
        public string SaveFile(HttpPostedFileBase file, string virtualDirectory)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(Container);
            container.CreateIfNotExists();
            var fileName = string.Format("{0}{1}", DateTime.Now.ToString("yyMMdd-HHmmssfff"), System.IO.Path.GetExtension(file.FileName));
            var filePath = string.Format("{0}{1}", virtualDirectory, fileName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(filePath);
            blockBlob.UploadFromStream(file.InputStream);
            return filePath;
        }
    }
}
