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
    public class AboutUsController : Controller
    {
        private SefeviDB db = new SefeviDB();

        // GET: SefAreas/AboutUs
        public async Task<ActionResult> Index()
        {
            var aboutUsTBs = db.AboutUsTBs.Include(a => a.LanguageTB);
            return View(await aboutUsTBs.ToListAsync());
        }

        // GET: SefAreas/AboutUs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUsTB aboutUsTB = await db.AboutUsTBs.FindAsync(id);
            if (aboutUsTB == null)
            {
                return HttpNotFound();
            }
            return View(aboutUsTB);
        }

        // GET: SefAreas/AboutUs/Create
        public ActionResult Create()
        {
            ViewBag.LanguageId = new SelectList(db.LanguageTBs, "LanguageID", "CultureName");
            return View();
        }

        // POST: SefAreas/AboutUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AboutID,Header,Description,AboutImage,LanguageId,Icon")] AboutUsTB aboutUsTB,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo photoInfo = new FileInfo(Photo.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                    img.Save("~/Uploads/AboutPhoto/" + newPhoto);
                    aboutUsTB.AboutImage = "/Uploads/AboutPhoto/" + newPhoto;
                }
                db.AboutUsTBs.Add(aboutUsTB);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LanguageId = new SelectList(db.LanguageTBs, "LanguageID", "CultureName", aboutUsTB.LanguageId);
            return View(aboutUsTB);
        }

        // GET: SefAreas/AboutUs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUsTB aboutUsTB = await db.AboutUsTBs.FindAsync(id);
            if (aboutUsTB == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageId = new SelectList(db.LanguageTBs, "LanguageID", "CultureName", aboutUsTB.LanguageId);
            return View(aboutUsTB);
        }

        // POST: SefAreas/AboutUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AboutID,Header,Description,AboutImage,LanguageId,Icon")] AboutUsTB aboutUsTB,HttpPostedFileBase Photo,int id)
        {
            if (ModelState.IsValid)
            {
                var aboutInfo = await db.AboutUsTBs.SingleAsync(m => m.AboutID == id);
                if (Photo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(aboutInfo.AboutImage)))
                    {
                        System.IO.File.Delete(Server.MapPath(aboutInfo.AboutImage));
                    }
                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo photoInfo = new FileInfo(Photo.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                    img.Save("~/Uploads/AboutPhoto/" + newPhoto);
                    aboutInfo.AboutImage = "/Uploads/AboutPhoto/" + newPhoto;
                }
                aboutInfo.LanguageId = aboutUsTB.LanguageId;
                aboutInfo.Header = aboutUsTB.Header;
                aboutInfo.Description = aboutUsTB.Description;
                aboutInfo.AboutImage = aboutUsTB.AboutImage;
                aboutInfo.Icon = aboutUsTB.Icon;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LanguageId = new SelectList(db.LanguageTBs, "LanguageID", "CultureName", aboutUsTB.LanguageId);
            return View(aboutUsTB);
        }

        // GET: SefAreas/AboutUs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUsTB aboutUsTB = await db.AboutUsTBs.FindAsync(id);
            if (aboutUsTB == null)
            {
                return HttpNotFound();
            }
            return View(aboutUsTB);
        }

        // POST: SefAreas/AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AboutUsTB aboutUsTB = await db.AboutUsTBs.FindAsync(id);
            db.AboutUsTBs.Remove(aboutUsTB);
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
