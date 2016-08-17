using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Docimax.Common
{
    public static class HTMLRadioCheckExtensions
    {
        #region 单选框和复选框的扩展
        /// <summary>
        /// 复选框,selValue为选中项
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="selectList"></param>
        /// <param name="selValue"></param>
        /// <returns></returns>
        public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, IEnumerable<string> selValue)
        {
            return CheckBoxAndRadioFor<object, string>(name, selectList, false, selValue);
        }
        public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string selValue)
        {
            return CheckBox(htmlHelper, name, selectList, new List<string> { selValue });

        }
        /// <summary>
        /// 复选框
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="selectList"></param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxFor(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
        {
            return CheckBox(htmlHelper, name, selectList, new List<string>());
        }
        /// <summary>
        /// 根据列表输出checkbox
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="selectList"></param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
        {
            return CheckBoxFor(htmlHelper, expression, selectList, null);
        }

        /// <summary>
        ///  根据列表输出checkbox,selValue为默认选中的项
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="selectList"></param>
        /// <param name="selValue"></param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string selValue)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            return CheckBoxAndRadioFor<TModel, TProperty>(name, selectList, false, new List<string> { selValue });
        }
        /// <summary>
        /// 输出单选框和复选框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <param name="selectList"></param>
        /// <param name="isRadio"></param>
        /// <param name="selValue"></param>
        /// <returns></returns>
        static MvcHtmlString CheckBoxAndRadioFor<TModel, TProperty>(string name, IEnumerable<SelectListItem> selectList, bool isRadio, IEnumerable<string> selValue)
        {
            StringBuilder str = new StringBuilder();
            int c = 0;
            string check, activeClass;
            string type = isRadio ? "Radio" : "checkbox";

            foreach (var item in selectList)
            {
                c++;
                if (selValue != null && selValue.Contains(item.Value))
                {
                    check = "checked='checked'";
                    activeClass = "style=color:red";
                }
                else
                {
                    check = string.Empty;
                    activeClass = string.Empty;
                }
                str.AppendFormat("<span><input type='{3}' value='{0}' name={1} id={1}{2} " + check + "/>", item.Value, name, c, type);
                str.AppendFormat("<label for='{0}{1}' {3}>{2}</lable></span>", name, c, item.Text, activeClass);

            }
            return MvcHtmlString.Create(str.ToString());
        }


        public static MvcHtmlString RadioButton(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, IEnumerable<string> selValue)
        {
            return CheckBoxAndRadioFor<object, string>(name, selectList, true, selValue);
        }
        /// <summary>
        /// 单选按钮组，seletList为选中项
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="selectList"></param>
        /// <param name="selValue"></param>
        /// <returns></returns>
        public static MvcHtmlString RadioButton(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string selValue)
        {
            return RadioButton(htmlHelper, name, selectList, new List<string> { selValue });
        }
        /// <summary>
        /// 单选按钮组
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="selectList"></param>
        /// <returns></returns>
        public static MvcHtmlString RadioButton(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
        {
            return RadioButton(htmlHelper, name, selectList, new List<string>());
        }
        /// <summary>
        ///  根据列表输出radiobutton
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="selectList"></param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
        {
            return RadioButtonFor(htmlHelper, expression, selectList, new List<string>());
        }
        public static MvcHtmlString RadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IEnumerable<string> selValue)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            return CheckBoxAndRadioFor<TModel, TProperty>(name, selectList, true, selValue);
        }
        /// <summary>
        /// 根据列表输出radiobutton,selValue为默认选中的项
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="selectList"></param>
        /// <param name="selValue"></param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string selValue)
        {
            return RadioButtonFor(htmlHelper, expression, selectList, new List<string> { selValue });
        }
        #endregion
    }
}
