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
        /// <summary>
        /// 从指定的地址下载文件
        /// </summary>
        /// <param name="fileURL">要查看的文件路径（虚拟路径）</param>
        /// <returns>返回文件字节流</returns>
        public byte[] GetFile(string fileURL)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(Container);
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
    }
}
