using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CodeDemo
{
    public class LR_CodeDemoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_CodeDemo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_CodeDemo_default",
                "LR_CodeDemo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}