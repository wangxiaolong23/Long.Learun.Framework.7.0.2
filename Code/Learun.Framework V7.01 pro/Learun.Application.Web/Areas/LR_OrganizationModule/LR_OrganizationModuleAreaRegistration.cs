using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_OrganizationModule
{
    public class LR_OrganizationModuleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_OrganizationModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_OrganizationModule_default",
                "LR_OrganizationModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}