using System;
using PagedList;
using System.Collections.Generic;
using Docimax.Common;

namespace Docimax.Interface_ICD.Model
{
    public class ICDTimePagedList<S, T> : ICDPagedList<S, T>
    {
        public IPagedList<T> PageList { get; set; }

        /// <summary>
        /// 查询条件的开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 查询条件的结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

    }
    public class ICDPagedList<S, T> : PageModel
    {
        /// <summary>
        /// 除基本查询条件（时间段和基本模糊查询）外的查询条件实体类
        /// </summary>
        public S SearchModel { get; set; }
        /// <summary>
        /// 结果实体列表
        /// </summary>
        public List<T> Content { get; set; }
    }

    public class PageModel
    {
        /// <summary>
        /// 基本查询输入（用于模糊查询或者精确查询）
        /// </summary>
        public string TextFilter { get; set; }

        private int pageIndex;
        /// <summary>
        /// 当前页数
        /// </summary>
        public int Page
        {
            get
            {
                if (pageIndex == 0)
                {
                    pageIndex = 1;
                }
                return pageIndex;
            }
            set
            {
                pageIndex = value;
            }
        }

        private int pageSize;
        /// <summary>
        /// 每页显示的记录条数
        /// </summary>
        public int PageSize
        {
            get
            {
                if (pageSize == 0)
                {
                    pageSize = 10;
                }
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }

        /// <summary>
        /// 符合条件的记录总条数
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// 符合记录的总页数
        /// </summary>
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalRecords / PageSize); }
        }

        public string PageScript
        {
            get { return PageHelper.InitialPageScript(TotalRecords, PageSize, Page); }
        }
    }
}
