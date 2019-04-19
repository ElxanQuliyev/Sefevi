using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sefevi.Models;
using System.Web.Helpers;
using System.IO;

namespace Sefevi.Areas.SefAreas.Controllers
{
    public class AddTopSliderController : Controller
    {
        private SefeviDB db = new SefeviDB();

        // GET: SefAreas/AddTopSlider
        public async Task<ActionResult> Index()
        {
            var topSliders = db.TopSliders.Include(t => t.LanguageTB);
            return View(await topSliders.ToListAsync());
        }

        // GET: SefAreas/AddTopSlider/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopSlider topSlider = await db.TopSliders.FindAsync(id);
            if (topSlider == null)
            {
                return HttpNotFound();
            }
            return View(topSlider);
        }

        // GET: SefAreas/AddTopSlider/Create
        public ActionResult Create()
        {
            ViewBag.LanguageId = new SelectList(db.LanguageTBs, "LanguageID", "CultureName");
            return View();
        }

        // POST: SefAreas/AddTopSlider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TopSiderId,Header,Description,SliderImage,LanguageId")] TopSlider topSlider,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo photoInfo = new FileInfo(Photo.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                    img.Save("~/Uploads/SliderPhoto/" + newPhoto);
                    topSlider.SliderImage = "/Uploads/SliderPhoto/" + newPhoto;
                }
                db.TopSliders.Add(topSlider);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LanguageId = new SelectList(db.LanguageTBs, "LanguageID", "CultureName", topSlider.LanguageId);
            return View(topSlider);
        }

        // GET: SefAreas/AddTopSlider/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopSlider topSlider = await db.TopSliders.FindAsync(id);
            if (topSlider == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageId = new SelectList(db.LanguageTBs, "LanguageID", "CultureName", topSlider.LanguageId);
            return View(topSlider);
        }

        // POST: SefAreas/AddTopSlider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TopSiderId,Header,Description,SliderImage,LanguageId")] TopSlider topSlider,int id,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                var sliderInfo = await db.TopSliders.SingleAsync(m => m.TopSiderId == id);
                if (Photo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(sliderInfo.SliderImage)))
                    {
                        System.IO.File.Delete(Server.MapPath(sliderInfo.SliderImage));
                    }
                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo photoInfo = new FileInfo(Photo.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                    img.Save("~/Uploads/SliderPhoto/" + newPhoto);
                    sliderInfo.SliderImage = "/Uploads/SliderPhoto/" + newPhoto;
                }
                sliderInfo.LanguageId = topSlider.LanguageId;
                sliderInfo.Header = topSlider.Header;
                sliderInfo.Description = topSlider.Description;


                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LanguageId = new SelectList(db.LanguageTBs, "LanguageID", "CultureName", topSlider.LanguageId);
            return View(topSlider);
        }

        // GET: SefAreas/AddTopSlider/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopSlider topSlider = await db.TopSliders.FindAsync(id);
            if (topSlider == null)
            {
                return HttpNotFound();
            }
            return View(topSlider);
        }

        // POST: SefAreas/AddTopSlider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TopSlider topSlider = await db.TopSliders.FindAsync(id);
            db.TopSliders.Remove(topSlider);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
