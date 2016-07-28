﻿using System;
using System.Collections.Generic;

namespace Docimax.Interface_ICD.Model
{
    public class PagedList<S, T>
    {
        /// <summary>
        /// 除基本查询条件（时间段和基本模糊查询）外的查询条件实体类
        /// </summary>
        public S SearchModel { get; set; }
        /// <summary>
        /// 结果实体列表
        /// </summary>
        public List<T> Content { get; set; }
        /// <summary>
        /// 查询条件的开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 查询条件的结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 基本查询输入（用于模糊查询或者精确查询）
        /// </summary>
        public string TextFilter { get; set; }

        private int currentPage;
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurrentPage
        {
            get
            {
                if (currentPage == 0)
                {
                    currentPage = 1;
                }
                return currentPage;
            }
            set
            {
                currentPage = value;
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
    }

}
