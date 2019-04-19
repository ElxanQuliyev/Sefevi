using Sefevi.Models;
using Sefevi.ViewModels.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sefevi.Controllers
{
    public class HomeController : Controller
    {
        SefeviDB db = new SefeviDB();

        public ActionResult Index()
        {
            var defaultModel = new DefaultViewModel
            {
                topSilder=db.TopSliders.Where(x=>x.LanguageTB.CultureCode==MainLanguage.lb).ToList(),
                aboutUS=db.AboutUsTBs.Where(x=>x.LanguageTB.CultureCode==MainLanguage.lb).FirstOrDefault()
            };
            return View(defaultModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}