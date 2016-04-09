using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using HuiYin.Models;

namespace HuiYin.Controllers
{
    public class WenKuController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Viewer(string viewFileName)
        {
            ViewBag.ViewFileName = viewFileName;
            return View();
        }

        private List<long> GetWhereList(long fId)
        {
            var newdb = new AppDbContext();
            string sql =
                "select f1.Id,f2.Id,f3.Id,f4.Id,f5.Id from WenKuFenLeis f1 left join WenKuFenLeis f2 on f1.id = f2.ParentId left join WenKuFenLeis f3 on f2.id = f3.ParentId left join WenKuFenLeis f4 on f3.id = f4.ParentId left join WenKuFenLeis f5 on f4.id = f5.ParentId ";

            if (fId > 0)
            {
                sql += " where f1.Id='" + fId + "' ";
            }
            var list = new List<long>();
            using (var dbcon = newdb.Database.Connection as SqlConnection)
            {
                dbcon.Open();
                var cmd = new SqlCommand(sql, dbcon);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var f1 = objtolong(reader[0]);
                        var f2 = objtolong(reader[1]);
                        var f3 = objtolong(reader[2]);
                        var f4 = objtolong(reader[3]);
                        var f5 = objtolong(reader[4]);
                        if (f1 != 0 && !list.Contains(f1))
                        {
                            list.Add(f1);
                        }
                        if (f2 != 0 && !list.Contains(f2))
                        {
                            list.Add(f2);
                        }
                        if (f3 != 0 && !list.Contains(f3))
                        {
                            list.Add(f3);
                        }
                        if (f4 != 0 && !list.Contains(f4))
                        {
                            list.Add(f4);
                        }
                        if (f5 != 0 && !list.Contains(f5))
                        {
                            list.Add(f5);
                        }
                    }
                }

            }
            return list;
        }

       // [Authorize]
        public ActionResult AddToShop(long wenkuId)
        {
            if (BLContext.LhUser == null)
            {
                return Json("未登录", JsonRequestBehavior.AllowGet);
            }
            //var wenku = db.WenKus.Find(wenkuId);
            var uf = db.UploadFiles.FirstOrDefault(x => x.WenKuId == wenkuId);
            var cart =OrderController.GetShoppingCart(uf.FileName);
            cart.LhUserId = BLContext.LhUserId;
            cart.UploadFileId = uf.Id;

            db.ShoppingCarts.Add(cart);
            db.SaveChanges();

//            var obj = new
//            {
//                jsonrpc = "2.0",
//                id = cart.Id,
//                fileid = uf.Id,
//                //cartid = cart.Id,
//                //fenshu = cart.Count,
//                //iscaida = cart.IsCaiDa,
//                //printsize = cart.PrintSize.ToString(),
//                //isdanmian = cart.IsDanMian,
//                //baozhuang = cart.BaoZhuang.ToString(),
//                //totalyeshu = cart.TotalPage,
//                jine = cart.Money.ToString(CultureInfo.InvariantCulture),
//                
//            };
            return Json("添加购物车成功", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Default(string key="", long fId=0)
        {
           
//            ViewBag.WenKuFenLeis = db.WenKuFenLeis.ToList();
            ViewBag.WenKuFenLeis1 = db.WenKuFenLeis.Where(x=>x.FenLeiType==FenLeiType.一级).ToList();
            ViewBag.WenKuFenLeis2 = db.WenKuFenLeis.Where(x=>x.FenLeiType==FenLeiType.二级).ToList();

            if (fId == 0)
            {
                var WenKus = db.WenKus.ToList();
                if (!string.IsNullOrEmpty(key))
                {
                    WenKus = WenKus.Where(x => x.Name.Contains(key)).ToList();
                }
                ViewBag.WenKus = WenKus;
            }
            else
            {
                var list = GetWhereList(fId);
                var fenleis = db.WenKuFenLeis.Where(x => list.Contains(x.Id)).ToList().Select(x=>x.Id);
                ViewBag.WenKus = db.WenKus.Where(x =>fenleis.Contains(x.WenKuFenLeiId)).ToList();

            }
            //                var listall=from t1 in db.WenKuFenLeis
            //                            join t2 in db.WenKuFenLeis on t1.Id equals t2.ParentId into r1
            //                            from t12 in r1.DefaultIfEmpty()
            //                            join t3 in db.WenKuFenLeis on 

            return View();
        }

        private long objtolong(object obj)
        {
            if (obj == null||obj is DBNull)
            {
                return 0;
            }
            return Convert.ToInt64(obj);
        }

        [Authorize]
        // GET: WenKu
        public ActionResult Index(RequestModel request)
        {
            if (BLContext.LhUser == null || BLContext.LhUser.UserType != UserType.Admin)
            {
                return Redirect("/LhUser/Login?ReturlUrl=/WenKu/Index");
            }
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
            ViewBag.WenKuFenLeiId1 = new SelectList(db.WenKuFenLeis.Where(x => x.FenLeiType == FenLeiType.一级).ToList(), "Id", "Name");
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

            var uf = new UploadFile();
            uf.FileName = wenKu.Name;
            uf.FilePathName = wenKu.FileName;
            uf.WenKuId = wenKu.Id;

            db.UploadFiles.Add(uf);
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
            var uf = db.UploadFiles.FirstOrDefault(x => x.WenKuId == id);
            if (uf != null)
            {
                db.UploadFiles.Remove(uf);
                db.SaveChanges();
            }
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
