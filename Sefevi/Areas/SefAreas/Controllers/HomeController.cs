using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sefevi.Areas.SefAreas.Controllers
{
    public class HomeController : Controller
    {
        // GET: SefAreas/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}