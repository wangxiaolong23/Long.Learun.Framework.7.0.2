using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_WebChatModule
{
    public class LR_WebChatModuleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_WebChatModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_WebChatModule_default",
                "LR_WebChatModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}