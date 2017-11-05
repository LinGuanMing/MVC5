using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Models
{
    public class ProductsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Product.Take(10).ToList());
        }

        public ActionResult Search(string keyword)
        {
            var data = db.Product.Where(p => p.ProductName.Contains(keyword)).Take(10).ToList();
            return View("Index", data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CreateNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNew(CreateNewVM data)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(new Product()
                {
                    ProductId = 0,
                    ProductName = data.ProductName,
                    Price = data.Price,
                    Active = true,
                    Stock = 1
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Top10()
        {
            var db = new FabricsEntities();
            var result = db.Product.Take(10);
            //var data = from x in result
            //           orderby x.ProductId
            //           select new CreateNewVM()
            //           {
            //               ProductID = x.ProductId,
            //               ProductName = x.ProductName,
            //               Price = x.Price,
            //               OrderLineCount = x.OrderLine.Count()
            //           };

            var data = db.Database.SqlQuery<CreateNewVM>("SELECT TOP 10 *,OrderLineCount=(SELECT COUNT(*) FROM dbo.OrderLine o WHERE o.ProductId=p.ProductId) FROM dbo.Product p");

            return View(data);
        }

        public ActionResult PriceUp()
        {
            var db = new FabricsEntities();
            foreach (var item in db.Product)
            {
                item.Price += 1;
            }
            db.SaveChanges();
            return RedirectToAction("Top10");
        }

        public ActionResult Price_one(int id)
        {
            var db = new FabricsEntities();
            //Product data = db.Product.Find(id);
            //data.Price += 1;
            //db.SaveChanges();
            var all = db.Database.ExecuteSqlCommand("UPDATE dbo.Product SET price=price+1 WHERE dbo.Product.ProductId=@p0 ", id);
            return RedirectToAction("Top10");
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Top10");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
