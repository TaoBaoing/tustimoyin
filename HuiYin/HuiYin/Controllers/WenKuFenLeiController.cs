using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using HuiYin.Models;

namespace HuiYin.Controllers
{
    public class WenKuFenLeiController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult GetChilds(long id,FenLeiType type)
        {
            List<WenKuFenLei> list = db.WenKuFenLeis.Where(x => x.ParentId == id && x.FenLeiType == type).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddChild(long id)
        {
            var fenlei = db.WenKuFenLeis.Find(id);
            return View(fenlei);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddChild(long id, FenLeiType type, string Name)
        {
            var feilei = new WenKuFenLei();
            feilei.Name = Name;
            feilei.ParentId = id;
            feilei.FenLeiType = type + 1;

            db.WenKuFenLeis.Add(feilei);
            db.SaveChanges();
            if (type == FenLeiType.一级)
            {
                return RedirectToAction("Index1");
            }
            else if (type == FenLeiType.二级)
            {
                return RedirectToAction("Index2");
            }
            else if (type == FenLeiType.三级)
            {
                return RedirectToAction("Index3");
            }
            else if (type == FenLeiType.四级)
            {
                return RedirectToAction("Index4");
            }
            else
            {
                return RedirectToAction("Index5");
            }
        }

        public ActionResult Index3()
        {
            var list = db.WenKuFenLeis.Where(x => x.FenLeiType == FenLeiType.三级).ToList();
            return View(list);
        }
        public ActionResult Index4()
        {
            var list = db.WenKuFenLeis.Where(x => x.FenLeiType == FenLeiType.四级).ToList();
            return View(list);
        }
        public ActionResult Index5()
        {
            var list = db.WenKuFenLeis.Where(x => x.FenLeiType == FenLeiType.五级).ToList();
            return View(list);
        }

        public ActionResult Index2()
        {
            var list = db.WenKuFenLeis.Include(x => x.ParentWenKuFenLei).Where(x => x.FenLeiType == FenLeiType.二级).ToList();
            return View(list);
        }

        public ActionResult Index1()
        {
            var list = db.WenKuFenLeis.Where(x => x.FenLeiType == FenLeiType.一级).ToList();
            return View(list);
        }


        public ActionResult Create1()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(WenKuFenLei wenKuFenLei)
        {
            wenKuFenLei.FenLeiType = FenLeiType.一级;
            db.WenKuFenLeis.Add(wenKuFenLei);
            db.SaveChanges();
            return RedirectToAction("Index1");
        }





        // GET: WenKuFenLei
        public ActionResult Index()
        {
            return View(db.WenKuFenLeis.ToList());
        }

        // GET: WenKuFenLei/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WenKuFenLei wenKuFenLei = db.WenKuFenLeis.Find(id);
            if (wenKuFenLei == null)
            {
                return HttpNotFound();
            }
            return View(wenKuFenLei);
        }

        // GET: WenKuFenLei/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WenKuFenLei/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WenKuFenLei wenKuFenLei)
        {
            if (ModelState.IsValid)
            {
                db.WenKuFenLeis.Add(wenKuFenLei);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wenKuFenLei);
        }

        // GET: WenKuFenLei/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WenKuFenLei wenKuFenLei = db.WenKuFenLeis.Find(id);
            if (wenKuFenLei == null)
            {
                return HttpNotFound();
            }
            return View(wenKuFenLei);
        }

        // POST: WenKuFenLei/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WenKuFenLei wenKuFenLei)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wenKuFenLei).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wenKuFenLei);
        }

        // GET: WenKuFenLei/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WenKuFenLei wenKuFenLei = db.WenKuFenLeis.Find(id);
            if (wenKuFenLei == null)
            {
                return HttpNotFound();
            }
            return View(wenKuFenLei);
        }

        // POST: WenKuFenLei/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            WenKuFenLei wenKuFenLei = db.WenKuFenLeis.Find(id);
            db.WenKuFenLeis.Remove(wenKuFenLei);
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
