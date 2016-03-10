using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HuiYin.Models;

namespace HuiYin.Controllers
{
    public class WenKuController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: WenKu
        public ActionResult Index(RequestModel request)
        {
            var wenKus = db.WenKus.Include(w => w.WenKuFenLei);


            return View(wenKus.ToList());
        }

        // GET: WenKu/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WenKu wenKu = db.WenKus.Find(id);
            if (wenKu == null)
            {
                return HttpNotFound();
            }
            return View(wenKu);
        }

        // GET: WenKu/Create
        public ActionResult Create()
        {
            ViewBag.WenKuFenLeiId1 = new SelectList(db.WenKuFenLeis.Where(x=>x.FenLeiType==FenLeiType.一级).ToList(), "Id", "Name");
            ViewBag.WenKuFenLeiId2 = new SelectList(new List<SelectListItem>(), "Value", "Text");
            ViewBag.WenKuFenLeiId3 = new SelectList(new List<SelectListItem>(), "Value", "Text");
            ViewBag.WenKuFenLeiId4 = new SelectList(new List<SelectListItem>(), "Value", "Text");
            ViewBag.WenKuFenLeiId5 = new SelectList(new List<SelectListItem>(), "Value", "Text");

            return View();
        }



        // POST: WenKu/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WenKu wenKu)
        {
                db.WenKus.Add(wenKu);
                db.SaveChanges();
                return RedirectToAction("Index");
           
        }

        // GET: WenKu/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WenKu wenKu = db.WenKus.Find(id);
            if (wenKu == null)
            {
                return HttpNotFound();
            }
            ViewBag.WenKuFenLeiId = new SelectList(db.WenKuFenLeis, "Id", "Name", wenKu.WenKuFenLeiId);
            return View(wenKu);
        }

        // POST: WenKu/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WenKu wenKu)
        {
              db.Entry(wenKu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        // GET: WenKu/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WenKu wenKu = db.WenKus.Find(id);
            if (wenKu == null)
            {
                return HttpNotFound();
            }
            return View(wenKu);
        }

        // POST: WenKu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            WenKu wenKu = db.WenKus.Find(id);
            db.WenKus.Remove(wenKu);
            db.SaveChanges();
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
