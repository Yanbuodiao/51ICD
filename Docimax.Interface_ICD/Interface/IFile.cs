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
    }
}
