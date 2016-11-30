
using System;

namespace Docimax.Common
{
    public class PageHelper
    {

        #region 分页脚本
        /// <summary>
        /// 分页脚本 
        /// </summary>
        /// <param name="TotalNum">数据总条数</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="pageIndex">当前页</param> 
        /// <returns></returns>
        public static string InitialPageScript(int TotalNum, int PageSize, int pageIndex)
        {
            string urljs = "\"javascript:search('{0}')\"";
            int iLint;
            int TotalPage;
            string FootList = "";
            //if (TotalNum == 0) { TotalPage = 0; return ""; }

            //FootList += "<div class=\"page-txt\">";
            if (TotalNum % PageSize == 0)
            {
                TotalPage = TotalNum / PageSize;
            }
            else
            {
                TotalPage = 1 + Convert.ToInt32(TotalNum / PageSize);
            }
            //FootList += "共" + TotalNum + "条";
            FootList +=  "<ul  class='pagination'>";

            int iPint = 5;
            if (TotalPage < 5)
            {
                iPint = TotalPage;
            }

            if (TotalPage <= 5)
            {
                if (pageIndex == 1)
                {
                    FootList += "<li><A href=" + string.Format(urljs, 1) + ">上一页</A></li>";
                }
                else
                {
                    FootList += "<li><A href=" + string.Format(urljs, pageIndex - 1) + ">上一页</A></li>";
                }

                for (iLint = 1; iLint <= iPint; iLint++)
                {
                    if (iLint == pageIndex)
                    {
                        FootList += "<li class=\"active\"><A>" + iLint + "</A></li>";
                    }
                    else
                    {
                        FootList += "<li><A href=" + string.Format(urljs, iLint) + ">" + iLint + "</A></li>";
                    }
                }

                if (pageIndex != TotalPage)
                {
                    FootList += "<li><A href=" + string.Format(urljs, pageIndex + 1) + ">下一页</A></li>";
                }
                else
                {
                    FootList += "<li><A href=" + string.Format(urljs, TotalPage) + ">下一页</A></li>";
                }

                int NextNum = pageIndex + 1;
                if (pageIndex == TotalPage)
                {
                    NextNum = pageIndex;
                }

            }
            else
            {
                FootList += "<li><A href=" + string.Format(urljs, 1) + ">首页</A></li>";
                if (pageIndex <= 3)
                {
                    if (pageIndex == 1)
                    {
                        FootList += "<li><A href=" + string.Format(urljs, 1) + ">上一页</A></li>";
                    }
                    else
                    {
                        FootList += "<li><A href=" + string.Format(urljs, pageIndex - 1) + ">上一页</A></li>";
                    }

                    for (iLint = 1; iLint <= 5; iLint++)
                    {
                        if (iLint == pageIndex)
                        {
                            FootList += "<li class=\"active\"><A>" + iLint + "</A></li>";
                        }
                        else
                        {
                            FootList += "<li><A href=" + string.Format(urljs, iLint) + ">" + iLint + "</A></li>";
                        }
                    }
                    FootList += "<li><A href=" + string.Format(urljs, pageIndex + 1) + ">下一页</A></li>";
                }
                else if (pageIndex + 3 < TotalPage)
                {
                    FootList += "<li><A href=" + string.Format(urljs, pageIndex - 1) + ">上一页</A></li>";
                    for (iLint = pageIndex - 3; iLint < pageIndex + 3; iLint++)
                    {
                        if (iLint == pageIndex)
                        {
                            FootList += "<li class=\"active\"><A>" + iLint + "</A></li>";
                        }
                        else
                        {
                            FootList += "<li><A href=" + string.Format(urljs, iLint) + ">" + iLint + "</A></li>";
                        }
                    }
                    FootList += "<li><A href=" + string.Format(urljs, pageIndex + 1) + ">下一页</A></li>";
                }
                else
                {
                    FootList += "<li><A href=" + string.Format(urljs, pageIndex - 1) + ">上一页</A></li>";
                    for (iLint = pageIndex - 4; iLint <= TotalPage; iLint++)
                    {
                        if (iLint == pageIndex)
                        {
                            FootList += "<li class=\"active\"><A>" + iLint + "</A></li>";
                        }
                        else
                        {
                            FootList += "<li><A href=" + string.Format(urljs, iLint) + ">" + iLint + "</A></li>";
                        }
                    }
                }
                FootList += "<li><A href=" + string.Format(urljs, TotalPage) + ">末页</A></li>";
            }
            FootList += "</ul>";

            return FootList;
        }
        #endregion
    }
}