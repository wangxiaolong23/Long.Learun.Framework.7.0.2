using Learun.Application.Base.AuthorizeModule;
using System.Web.Mvc;

namespace Learun.Application.Web
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：控制器执行后执行
    /// </summary>
    public class DepartAuthFillters : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// 执行完action后跳转后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
        /// <summary>
        /// 执行完action后跳转前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DataAuthorizeBLL dataAuthorizeBLL = new DataAuthorizeBLL();
            dataAuthorizeBLL.SetWhereSql();
        }
    }
}