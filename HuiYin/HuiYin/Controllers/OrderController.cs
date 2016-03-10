using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HuiYin.Models;
using System.Data.Entity;
namespace HuiYin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly decimal A4黑白单面 = (decimal)0.07;
        private readonly decimal A3黑白单面 = (decimal)0.3;
        private readonly decimal A2黑白单面 = (decimal)2;
        private readonly decimal A1黑白单面 = (decimal)3.5;


        private readonly decimal A4黑白双面 = (decimal)0.12;

        private readonly decimal A4彩打单面 = (decimal)0.5;


        private readonly int 胶装 = 6;
        private readonly int 打孔装 = 4;

        private AppDbContext db = new AppDbContext();


        public ActionResult SetPrintStatus(long orderid,int orderstatus)
        {
//            全部=0,
//        未打印 = 10,
//        已打印 = 20
            var order = db.Orders.Find(orderid);
            db.Orders.Attach(order);
            order.OrderStatus=OrderStatus.已打印;
            db.SaveChanges();
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllOrder(string XingMing = "", string Start = "", string End = "", int Status = 0, int PageIndex = 1)
        {
            int pagesize = 20;

            if (BLContext.LhUser.UserType != UserType.Admin)
            {
                return RedirectToAction("AdminLogin","LhUser");
            }
            var list = db.Orders.Include(x=>x.LhUser).Include(x => x.OrderDetails).ToList();
            //客户
            if (!string.IsNullOrEmpty(XingMing))
            {
                    list = list.Where(x => x.LhUser.XingMing.Contains(XingMing)).ToList();
            }
            //开始时间
            if (!string.IsNullOrEmpty(Start))
            {
                DateTime ds;
                if (DateTime.TryParse(Start, out ds))
                {
                    list = list.Where(x => x.CreateTime >= ds).ToList();
                }
            }
            //结束时间
            if (!string.IsNullOrEmpty(End))
            {
                DateTime de;
                if (DateTime.TryParse(End, out de))
                {
                    list = list.Where(x => x.CreateTime <= de).ToList();
                }
            }

            //状态
            if (Status != 0)
            {
                if (Status == 10)
                {
                    list = list.Where(x => x.OrderStatus == OrderStatus.未打印).ToList();
                }
                else if (Status == 20)
                {
                    list = list.Where(x => x.OrderStatus == OrderStatus.已打印).ToList();
                }
            }


            if (PageIndex < 1)
            {
                PageIndex = 1;
            }

            //总共记录
            ViewBag.TotalCount = list.Count();

            int pages;
            if (list.Count() % pagesize == 0)
            {
                pages = list.Count() / pagesize;
            }
            else
            {
                pages = list.Count() / pagesize + 1;
            }
            if (PageIndex > pages)
            {
                PageIndex = pages;
            }
            var skipcount = pagesize * (PageIndex - 1);

            list = list.OrderByDescending(x => x.CreateTime).Skip(skipcount).Take(pagesize).ToList();

            //总页数
            ViewBag.TotalPages = pages;
            ViewBag.PageIndex = PageIndex;
            ViewBag.XingMing = XingMing;
            ViewBag.Start = Start;
            ViewBag.End = End;
            ViewBag.Status = Status;

            ViewBag.StatusName = "全部";
            if (Status == 10)
            {
                ViewBag.StatusName = "未打印";
            }
            else
            {
                ViewBag.StatusName = "已打印";
            }
            return View(list);
        }

        public ActionResult CreateOrderByCart()
        {
            var list = db.ShoppingCarts.Where(x => x.LhUserId == BLContext.LhUserId);
            var order=new Order();
            order.LhUserId = BLContext.LhUserId;
            order.OrderStatus=OrderStatus.未打印;
            foreach (ShoppingCart cart in list)
            {
                var detail=new OrderDetail();
                detail.LhUserId = cart.LhUserId;
                detail.UploadFileId = cart.UploadFileId;
                detail.Name = cart.Name;
                detail.Count = cart.Count;
                detail.IsDanMian = cart.IsDanMian;
                detail.IsCaiDa = cart.IsCaiDa;
                detail.PrintSize = cart.PrintSize;
                detail.BaoZhuang = cart.BaoZhuang;
                detail.TotalPage = cart.TotalPage;
                detail.Money = cart.Money;
                order.OrderDetails.Add(detail);
            }
            order.Money = order.OrderDetails.Sum(x => x.Money);
            db.Orders.Add(order);
            db.ShoppingCarts.RemoveRange(list);
            db.SaveChanges();

            return RedirectToAction("MyOrder");
        }

        public ActionResult MyOrder()
        {
            var orders = db.Orders.Include(x => x.OrderDetails).Where(x => x.LhUserId == BLContext.LhUserId).OrderByDescending(x => x.CreateTime).ToList();
            return View(orders);
        }

        public ActionResult ClearCart()
        {
            var list = db.ShoppingCarts.Where(x => x.LhUserId == BLContext.LhUserId);
            db.ShoppingCarts.RemoveRange(list);

            db.SaveChanges();
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DelCart(long cartid)
        {
            var cart = db.ShoppingCarts.Find(cartid);
            db.ShoppingCarts.Remove(cart);
            db.SaveChanges();
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        private ShoppingCart GetCartWithMoeny(ShoppingCart cart)
        {
            if (cart.IsCaiDa)
            {
                //彩印只有A4 单打
                cart.PrintSize = PrintSize.A4;
                cart.IsDanMian = true;
                cart.Money = cart.Count * cart.TotalPage * A4彩打单面;
            }
            else
            {
                //如果是单页
                if (cart.IsDanMian)
                {
                    //A4
                    if (cart.PrintSize == PrintSize.A4)
                    {
                        cart.Money = Math.Round(cart.Count * cart.TotalPage * A4黑白单面, 2);
                    }
                    else if (cart.PrintSize == PrintSize.A3)
                    {
                        cart.Money = Math.Round(cart.Count * cart.TotalPage * A3黑白单面, 2);
                    }
                    else if (cart.PrintSize == PrintSize.A2)
                    {
                        cart.Money = Math.Round(cart.Count * cart.TotalPage * A2黑白单面, 2);
                    }
                    else if (cart.PrintSize == PrintSize.A1)
                    {
                        cart.Money = Math.Round(cart.Count * cart.TotalPage * A1黑白单面, 2);
                    }
                }
                else
                {
                    //如果双面 只有A4黑白有双面
                    cart.PrintSize = PrintSize.A4;
                    cart.Money = Math.Round(cart.Count * A4黑白双面 * GetYe(cart.TotalPage), 2);
                }
            }
            if (cart.BaoZhuang == BaoZhuang.打孔装)
            {
                cart.Money += cart.Count * 打孔装;
            }
            else if (cart.BaoZhuang == BaoZhuang.胶装)
            {
                cart.Money += cart.Count * 胶装;
            }
            return cart;
        }

        public ActionResult ChangeCartByDanShuang(string danshuang, long cartid)
        {
            var cart = db.ShoppingCarts.Find(cartid);
            db.ShoppingCarts.Attach(cart);

            if (danshuang == "dan")
            {
                cart.IsDanMian = true;
            }
            else
            {
                cart.IsDanMian = false;
            }

            cart = GetCartWithMoeny(cart);
            db.SaveChanges();
            return Json(cart, JsonRequestBehavior.AllowGet);
        }
        public ActionResult HeiBaiChange(string heibai, long cartid)
        {
            var cart = db.ShoppingCarts.Find(cartid);
            db.ShoppingCarts.Attach(cart);

            if (heibai == "heibai")
            {
                cart.IsCaiDa = false;
            }
            else
            {
                cart.IsCaiDa = true;
            }

            cart = GetCartWithMoeny(cart);
            db.SaveChanges();
            return Json(cart, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PrintSizeChange(string psize, long cartid)
        {
            var cart = db.ShoppingCarts.Find(cartid);
            db.ShoppingCarts.Attach(cart);
            if (psize == "A4")
            {
                cart.PrintSize = PrintSize.A4;
            }
            else if (psize == "A3")
            {
                cart.PrintSize = PrintSize.A3;
            }
            else if (psize == "A2")
            {
                cart.PrintSize = PrintSize.A2;
            }
            else if (psize == "A1")
            {
                cart.PrintSize = PrintSize.A1;
            }

            cart = GetCartWithMoeny(cart);
            db.SaveChanges();
            return Json(cart, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CountChange(string count, long cartid)
        {
            var cart = db.ShoppingCarts.Find(cartid);
            db.ShoppingCarts.Attach(cart);

            cart.Count = Convert.ToInt32(count);

            cart = GetCartWithMoeny(cart);
            db.SaveChanges();
            return Json(cart, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BaoZhuangChange(string baozhuang, long cartid)
        {
            var cart = db.ShoppingCarts.Find(cartid);
            db.ShoppingCarts.Attach(cart);
            if (baozhuang == "wu")
            {
                cart.BaoZhuang = BaoZhuang.无;
            }
            else if (baozhuang == "jiaozhuang")
            {
                cart.BaoZhuang = BaoZhuang.胶装;
            }
            else if (baozhuang == "dakongzhuang")
            {
                cart.BaoZhuang = BaoZhuang.打孔装;
            }

            cart = GetCartWithMoeny(cart);
            db.SaveChanges();
            return Json(cart, JsonRequestBehavior.AllowGet);
        }
        private int GetYe(int pagecount)
        {
            if (pagecount % 2 == 0)
            {
                return pagecount / 2;
            }
            else
            {
                return pagecount / 2 + 1;
            }
        }

        public OrderController()
        {
            var applicationPath = VirtualPathUtility.ToAbsolute("~") == "/" ? "" : VirtualPathUtility.ToAbsolute("~");
            _urlPath = applicationPath + "/Upload";
        }

        // GET: Order
        //        public ActionResult Index()
        //        {
        //            return View();
        //        }
    

        public ActionResult AddOrder()
        {
            if (string.IsNullOrEmpty(BLContext.LhUser.XingMing) || string.IsNullOrEmpty(BLContext.LhUser.Address))
            {
                return RedirectToAction("PersonInfo", "LhUser", new { formindex ="true"});
            }
            var userid = BLContext.LhUserId;
            var carts = db.ShoppingCarts.Where(x => x.LhUserId == userid&&x.Status==Status.未下单);
            var list = carts.ToList<ShoppingCart>();
            ViewBag.Carts = list;
            return View();
        }


        static string _urlPath = string.Empty;
        public ActionResult UpLoadProcess(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {
            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload");
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string ex = Path.GetExtension(file.FileName);
            var filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            var filename = Path.Combine(localPath, filePathName);
            file.SaveAs(filename);
            var uf = new UploadFile();
            uf.FileName = file.FileName;
            uf.FilePathName = filePathName;
            uf.LhUserId = BLContext.LhUserId;
            db.UploadFiles.Add(uf);
            db.SaveChanges();

            var cart = GetShoppingCart(filename, file);
            cart.LhUserId = BLContext.LhUserId;
            cart.UploadFileId = uf.Id;
         
            db.ShoppingCarts.Add(cart);
            db.SaveChanges();

            var obj = new
            {
                jsonrpc = "2.0",
                id = id,
                fileid = uf.Id,
                cartid = cart.Id,
                fenshu = cart.Count,
                iscaida = cart.IsCaiDa,
                printsize = cart.PrintSize.ToString(),
                isdanmian = cart.IsDanMian,
                baozhuang = cart.BaoZhuang.ToString(),
                totalyeshu = cart.TotalPage,
                jine = cart.Money.ToString(CultureInfo.InvariantCulture),
                filePath = _urlPath + "/" + filePathName
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        private ShoppingCart GetShoppingCart(string filename, HttpPostedFileBase file)
        {
            var cart = new ShoppingCart();
            string ex = Path.GetExtension(filename);
            if (ex.Contains("doc"))
            {
                var util = new AppUtil();
                var total = util.GetWordPageCount(filename);
                cart.TotalPage = total;
            }
            else if (ex.Contains("pdf"))
            {
                var util = new AppUtil();
                var total = util.GetPDFPageCountByDll(filename);
                cart.TotalPage = total;
            }

            //设置默认值
            cart.Name = file.FileName;
            cart.Count = 1;
            cart.IsDanMian = true;
            cart.IsCaiDa = false;
            cart.PrintSize = PrintSize.A4;
            cart.BaoZhuang = BaoZhuang.无;
            cart.Status = Status.未下单;
            cart.Money = Math.Round(((decimal)(A4黑白单面 * cart.TotalPage)), 2);
            return cart;
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