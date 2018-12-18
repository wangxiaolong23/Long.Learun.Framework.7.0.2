using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CRMModule
{
    public class LR_CRMModuleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_CRMModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_CRMModule_default",
                "LR_CRMModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}