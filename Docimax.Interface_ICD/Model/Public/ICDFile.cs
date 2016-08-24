
namespace Docimax.Interface_ICD.Model
{
    public class ICDFile
    {
        /// <summary>
        /// 文件上传时名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件目录
        /// </summary>
        public string FileURL { get; set; }
        /// <summary>
        /// 文件的ContentType,呈现文件时使用
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 附件类别
        /// </summary>
        public int AttachType { get; set; }
        /// <summary>
        /// 附件在该组的排序值
        /// </summary>
        public int FileIndex { get; set; }
    }
}
