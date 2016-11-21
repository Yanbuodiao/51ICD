using System.Web;

namespace Docimax.Interface_ICD.Interface
{
    public interface IFile
    {
        /// <summary>
        /// 保存上传的文件，物理保存
        /// </summary>
        /// <param name="file">通过MVC上传的文件</param>
        /// <param name="virtualDirectory">要保存到的文件目录（不含文件名称）</param>
        /// <returns>文件保存后返回的地址</returns>
        string SaveFile(HttpPostedFileBase file, string virtualDirectory);
        /// <summary>
        /// 从指定的地址下载图片类文件
        /// </summary>
        /// <param name="fileURL"></param>
        /// <returns></returns>
        byte[] GetPicFile(string fileURL);
        /// <summary>
        /// 从指定的地址下载文件
        /// </summary>
        /// <param name="fileURL"></param>
        /// <returns></returns>
        byte[] GetFile(string fileURL);
        /// <summary>
        /// 保存病历文件
        /// </summary>
        /// <param name="fileBytes">病历文件内容</param>
        /// <param name="virtualFilePah">文件虚拟目录</param>
        /// <returns>保存后的文件路径</returns>
        string SaveMedicalFile(byte[] fileBytes, string virtualFilePah);
    }
}
