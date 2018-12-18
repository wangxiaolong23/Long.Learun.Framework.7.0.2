using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_SystemModule
{
    public class LR_SystemModuleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_SystemModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_SystemModule_default",
                "LR_SystemModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}