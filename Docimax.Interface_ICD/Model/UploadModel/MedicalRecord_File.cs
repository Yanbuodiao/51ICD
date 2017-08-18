using Docimax.Interface_ICD.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Docimax.Interface_ICD.Model.UploadModel
{
    /// <summary>
    /// 通过图片文件上传的病案实体
    /// </summary>
    public class MedicalRecord_File : MedicalRecordBase
    {
        /// <summary>
        /// 详细的编目列表
        /// </summary>
        public List<Catalog> Catalogs { get; set; }
    }

    public class Catalog
    {
        /// <summary>
        /// 编目排序值，值越小越靠前
        /// </summary>
        public int CatalogOrder { get; set; }
        /// <summary>
        /// 编目名称
        /// </summary>
        public string CatalogName { get; set; }
        /// <summary>
        /// 编目中包含的内容
        /// </summary>
        public List<CataLogFile> CatalogFiles { get; set; }
        /// <summary>
        /// 该编目的子目录
        /// </summary>
        public List<Catalog> SubCatalog { get; set; }
    }
    public class CataLogFile
    {
        /// <summary>
        /// 文件的排序值，值越小越靠前
        /// </summary>
        public int FileOrder { get; set; }
        /// <summary>
        /// 客户端的文件的名称，不包含目录路径,必须包含扩展名。
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        ///  获取一个 System.IO.Stream 对象，该对象指向一个上载文件，以准备读取该文件的内容。
        /// </summary>
        public byte[] InputStream { get; set; }
        /// <summary>
        /// 图片文件保存后的URL
        /// </summary>
        public string FileURL { get; set; }
        /// <summary>
        /// 文件的 MIME 内容类型
        /// </summary>
        public string ConentType { get; set; }
    }
}
