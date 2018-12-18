using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Learun.Application.Web
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：防伪验证
    /// </summary>
    public class HandlerValidateAntiForgeryTokenAttribute:AuthorizeAttribute
    {
        /// <summary>
        /// 拦截器
        /// </summary>
        /// <param name="filterContext">http上下文</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (request.HttpMethod == WebRequestMethods.Http.Post)
            {
                if (request.IsAjaxRequest())
                {
                    var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];
                    var cookieValue = antiForgeryCookie != null
                     ? antiForgeryCookie.Value
                     : null;
                    //从cookies 和 Headers 中 验证防伪标记
                    //这里可以加try-catch
                    AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
                }
                else
                {
                    new ValidateAntiForgeryTokenAttribute().OnAuthorization(filterContext);
                }
            }
        }
    }
}