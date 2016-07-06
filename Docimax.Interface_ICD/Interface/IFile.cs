using System.Web;

namespace Docimax.Interface_ICD.Interface
{
    public interface IFile
    {
        /// <summary>
        /// 保存用户的附件文件
        /// </summary>
        /// <param name="file">用户上传的文件</param>
        /// <returns>保存后地址</returns>
        string SaveUserAttachFile(HttpPostedFileBase file);
        /// <summary>
        /// 保存用户开通服务时的附件文件
        /// </summary>
        /// <param name="file">用户上传的文件</param>
        /// <returns>保存后地址</returns>
        string SaveUserServiceAttachFile(HttpPostedFileBase file);
    }
}
