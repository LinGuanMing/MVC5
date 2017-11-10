using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test(string id)
        {
            return View((object)id);
        }
        public ActionResult Test2(string id)
        {
            return PartialView("Test", (object)id);
        }
        public ActionResult Succeed()
        {
            return View("JSAlertRedirect", (object)"新增成功");
        }
        public ActionResult File1()
        {
            return File(
                @"C:\Users\wakau\Dropbox\Projects\TraPac\Stars 2.0\Final\甘特圖.xlsx",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "客戶資料輸出.xlsx");
        }
        public ActionResult file2(int dl = 0)
        {
            switch (dl)
            {
                case 1:
                    return File(Server.MapPath("~/Content/1377946249-1584189015.jpg"), "image/jpeg", "兄弟加油.jpg");
                default:
                    return File(Server.MapPath("~/Content/1377946249-1584189015.jpg"), "image/jpeg");
            }
        }
    }
}