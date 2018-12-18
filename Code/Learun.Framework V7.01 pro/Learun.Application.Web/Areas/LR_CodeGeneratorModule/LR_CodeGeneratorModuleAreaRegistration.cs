using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CodeGeneratorModule
{
    public class LR_CodeGeneratorModuleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_CodeGeneratorModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_CodeGeneratorModule_default",
                "LR_CodeGeneratorModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}