
using System.Web.Mvc;

namespace Sefevi.Areas.SefAreas
{
    public class SefAreasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SefAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SefAreas_default",
                "SefAreas/{controller}/{action}/{id}",
                new { action = "Index", controller = "Home",id = UrlParameter.Optional },
                new string[] { "Sefevi.Areas.SefAreas.Controllers" }

            );
        }
    }
}