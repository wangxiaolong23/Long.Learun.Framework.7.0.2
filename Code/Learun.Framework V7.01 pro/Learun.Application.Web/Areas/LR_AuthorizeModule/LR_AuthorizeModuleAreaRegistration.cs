using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_AuthorizeModule
{
    public class LR_AuthorizeModuleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_AuthorizeModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_AuthorizeModule_default",
                "LR_AuthorizeModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}