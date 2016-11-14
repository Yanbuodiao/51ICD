using Docimax.Interface_ICD.Interface;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Web;

namespace Docimax.Common_ICD.File
{
    public class File_Azure : IFile
    {
        private string containerName
        {
            get
            {
                var containerKey = "51ICDContainer";
                if (HttpRuntime.Cache[containerKey] != null)
                {
                    return HttpRuntime.Cache[containerKey].ToString();
                }
                var tempContainerKey = CloudConfigurationManager.GetSetting("51ICDContainer");
                HttpRuntime.Cache.Insert(containerKey, tempContainerKey, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
                return tempContainerKey;
            }
        }

        private string storageAccountString
        {
            get
            {
                var storageAccountKey = "StorageConnectionString";
                if (HttpRuntime.Cache[storageAccountKey] != null)
                {
                    return HttpRuntime.Cache[storageAccountKey].ToString();
                }
                var tempContainerKey = CloudConfigurationManager.GetSetting("StorageConnectionString");
                HttpRuntime.Cache.Insert(storageAccountKey, tempContainerKey, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
                return tempContainerKey;
            }
        }

        /// <summary>
        /// 保存上传的文件，物理保存
        /// </summary>
        /// <param name="file">通过MVC上传的文件</param>
        /// <param name="virtualDirectory">要保存到的文件目录（不含文件名称）</param>
        /// <returns>文件保存后返回的地址</returns>
        public string SaveFile(HttpPostedFileBase file, string virtualDirectory)
        {
            CloudBlobContainer container = getCloudBlobContainer();
            container.CreateIfNotExists();
            var fileName = string.Format("{0}{1}", DateTime.Now.ToString("yyMMdd-HHmmssfff"), System.IO.Path.GetExtension(file.FileName));
            var filePath = string.Format("{0}{1}", virtualDirectory, fileName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(filePath);
            blockBlob.UploadFromStream(file.InputStream);
            return filePath;
        }

        public string SaveMedicalFile(byte[] fileBytes, string virtualFilePah)
        {
            CloudBlobContainer container = getCloudBlobContainer();
            container.CreateIfNotExists();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(virtualFilePah);
            blockBlob.UploadFromByteArray(fileBytes, 0, fileBytes.Length);
            return virtualFilePah;
        }

        /// <summary>
        /// 从指定的地址下载文件
        /// </summary>
        /// <param name="fileURL">要查看的文件路径（虚拟路径）</param>
        /// <returns>返回文件字节流</returns>
        public byte[] GetFile(string fileURL)
        {
            CloudBlobContainer container = getCloudBlobContainer();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileURL);
            using (var stream = new MemoryStream())
            {
                blockBlob.DownloadToStream(stream);
                using (var img = Image.FromStream(stream))
                {
                    if (IsPixelFormatIndexed(img.PixelFormat))
                    {
                        using (Bitmap bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb))
                        {
                            using (Graphics g = Graphics.FromImage(bmp))
                            {
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                                ImageConverter converter = new ImageConverter();
                                return (byte[])converter.ConvertTo(bmp, typeof(byte[]));
                            }
                        }
                    }
                }
                return stream.ToArray();
            }
        }

        #region 私有方法

        private CloudBlobContainer getCloudBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageAccountString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            return container;
        }
        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            foreach (PixelFormat pf in indexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat)) return true;
            }
            return false;
        }

        private static PixelFormat[] indexedPixelFormats = { 
                                                               PixelFormat.Undefined, 
                                                               PixelFormat.DontCare,
                                                               PixelFormat.Format16bppArgb1555,
                                                               PixelFormat.Format1bppIndexed,
                                                               PixelFormat.Format4bppIndexed,
                                                               PixelFormat.Format8bppIndexed 
                                                           };

        #endregion
    }
}
