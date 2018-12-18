using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_WorkFlowModule
{
    public class LR_WorkFlowModuleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_WorkFlowModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_WorkFlowModule_default",
                "LR_WorkFlowModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}