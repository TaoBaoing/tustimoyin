using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HuiYin.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Upload()
        {
            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload\\WenKu");
            if (Request.Files.Count <= 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }
            var file = Request.Files[0];

            string ex = Path.GetExtension(file.FileName);
            var filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            var filename = Path.Combine(localPath, filePathName);
            file.SaveAs(filename);

            return Json(new {Name = file.FileName, FileName = filePathName }, JsonRequestBehavior.AllowGet);
        }
    }
}