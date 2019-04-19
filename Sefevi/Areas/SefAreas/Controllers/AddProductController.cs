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
    public class AddProductController : Controller
    {
        private SefeviDB db = new SefeviDB();

        // GET: SefAreas/AddProduct
        public async Task<ActionResult> Index()
        {
            var products = db.ProductsTBs.Include(p => p.LanguageTB);
            return View(await products.ToListAsync());
        }

        // GET: SefAreas/AddProduct/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsTB product = await db.ProductsTBs.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: SefAreas/AddProduct/Create
        public ActionResult Create()
        {
            ViewBag.LanguageId = new SelectList(db.LanguageTBs, "LanguageID", "CultureName");
            return View();
        }

        // POST: SefAreas/AddProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,Name,Decription,Protein,Price,Ton,LanguageId,ProductImage")] ProductsTB product, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo photoInfo = new FileInfo(Photo.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                    img.Save("~/Uploads/ProductImage/" + newPhoto);
                    product.ProductImage = "/Uploads/ProductImage/" + newPhoto;
                }
                db.ProductsTBs.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LanguageId = new SelectList(db.LanguageTBs, "LanguageID", "CultureName", product.LanguageId);
            return View(product);
        }

        // GET: SefAreas/AddProduct/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsTB product = await db.ProductsTBs.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.LanguageId = new SelectList(db.LanguageTBs, "LanguageID", "CultureName", product.LanguageId);
            return View(product);
        }

        // POST: SefAreas/AddProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public async Task<ActionResult> Edit([Bind(Include = "ProductID,Name,Decription,Protein,Price,Ton,LanguageId,ProductImage")] ProductsTB product,int id, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                var productInfo = await db.ProductsTBs.SingleAsync(m => m.ProductID == id);
                if (Photo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(productInfo.ProductImage)))
                    {
                        System.IO.File.Delete(Server.MapPath(productInfo.ProductImage));
                    }
                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo photoInfo = new FileInfo(Photo.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                    img.Save("~/Uploads/ProductImage/" + newPhoto);
                    productInfo.ProductImage = "/Uploads/ProductImage/" + newPhoto;
                }
                productInfo.LanguageId = product.LanguageId;
                productInfo.Name = product.Name;
                productInfo.Price = product.Price;
                productInfo.Protein = product.Protein;
                productInfo.Ton = product.Ton;
                productInfo.Decription = product.Decription;


                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LanguageId = new SelectList(db.LanguageTBs, "LanguageID", "CultureName", product.LanguageId);
            return View(product);
        }

        // GET: SefAreas/AddProduct/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsTB product = await db.ProductsTBs.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: SefAreas/AddProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductsTB product = await db.ProductsTBs.FindAsync(id);
            db.ProductsTBs.Remove(product);
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
