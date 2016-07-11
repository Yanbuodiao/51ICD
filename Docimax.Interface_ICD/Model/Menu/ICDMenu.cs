using System.Collections.Generic;

namespace Docimax.Interface_ICD.Model
{
    /// <summary>
    /// 编码平台菜单实体类
    /// </summary>
    public class ICDMenu
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuID { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 父菜单ID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 同级下的排序值
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 分区名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// Action名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<ICDMenu> SonMenuList { get; set; }
    }
}
