using Learun.Application.Base.AuthorizeModule;
using Learun.Util;
using Learun.Util.Operat;
using System.Web.Mvc;

namespace Learun.Application.Web
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：登录认证（会话验证组件）
    /// </summary>
    public class HandlerLoginAttribute : AuthorizeAttribute
    {
        private DataAuthorizeIBLL dataAuthorizeIBLL = new DataAuthorizeBLL();
        private FilterMode _customMode;
        /// <summary>默认构造</summary>
        /// <param name="Mode">认证模式</param>
        public HandlerLoginAttribute(FilterMode Mode)
        {
            _customMode = Mode;
        }
        /// <summary>
        /// 响应前执行登录验证,查看当前用户是否有效 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // 登录拦截是否忽略
            if (_customMode == FilterMode.Ignore)
            {
                return;
            }
            // 验证登录状态
            int res = OperatorHelper.Instance.IsOnLine().stateCode;
            if (res != 1)// 登录过期或者未登录
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
                return;
            }
            // IP过滤
            if (!this.FilterIP())
            {
                filterContext.Result = new RedirectResult("~/Login/Index?error=ip");
                return;
            }
            // 时段过滤
            if (!this.FilterTime())
            {
                filterContext.Result = new RedirectResult("~/Login/Index?error=time");
                return;
            }
            // 判断当前接口是否需要加载数据权限
            if (!this.DataAuthorize(filterContext))
            {
                filterContext.Result = new ContentResult { Content = new ResParameter { code = ResponseCode.fail, info = "没有该数据权限" }.ToJson() };
                return;
            }
        }
        /// <summary>
        /// IP过滤
        /// </summary>
        /// <returns></returns>
        private bool FilterIP()
        {
            bool isFilterIP = Config.GetValue("FilterIP").ToBool();
            if (isFilterIP == true)
            {
                return new FilterIPBLL().FilterIP();
            }
            return true;
        }
        /// <summary>
        /// 时段过滤
        /// </summary>
        /// <returns></returns>
        private bool FilterTime()
        {
            bool isFilterIP = Config.GetValue("FilterTime").ToBool();
            if (isFilterIP == true)
            {
                return new FilterTimeBLL().FilterTime();
            }
            return true;
        }
        /// <summary>
        /// 执行权限认证
        /// </summary>
        /// <param name="filterContext">当前连接</param>
        /// <returns></returns>
        private bool DataAuthorize(AuthorizationContext filterContext)
        {
            var areaName = filterContext.RouteData.DataTokens["area"] + "/";            //获取当前区域
            var controllerName = filterContext.RouteData.Values["controller"] + "/";    //获取控制器
            var action = filterContext.RouteData.Values["Action"];                      //获取当前Action
            string currentUrl = "/" + areaName + controllerName + action;               //拼接构造完整url


            WebHelper.AddHttpItems("currentUrl", currentUrl);
            return dataAuthorizeIBLL.SetWhereSql(currentUrl);
        }
    }
}