using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_ReportModule
{
    public class LR_ReportModuleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_ReportModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_ReportModule_default",
                "LR_ReportModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}