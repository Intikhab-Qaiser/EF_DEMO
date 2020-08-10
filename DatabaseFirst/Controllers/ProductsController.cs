using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseFirst;
using DatabaseFirst.Models;

namespace DatabaseFirst.Controllers
{
    public class ProductsController : Controller
    {
        private AdventureWorksContext db = new AdventureWorksContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductCategory).Include(p => p.ProductModel).
                            Select(p => 
                                    new ProductsList
                                    {
                                        ProductId = p.ProductID,
                                        Name = p.Name,
                                        Number = p.ProductNumber,
                                        Category = p.ProductCategory.Name,
                                        Model = p.ProductModel.Name,
                                        StandardCost = p.StandardCost,
                                        ListPrice = p.ListPrice
                                    });
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name");
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductEdit editProduct)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = editProduct.Name,
                    ProductNumber = editProduct.Number,
                    SellStartDate = editProduct.SellStart,
                    StandardCost = editProduct.StandardCost,
                    ListPrice = editProduct.ListPrice,
                    ProductCategoryID = editProduct.ProductCategoryID,
                    ProductModelID = editProduct.ProductModelID,
                    ModifiedDate = DateTime.Now,
                    rowguid = Guid.NewGuid()
                };
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", editProduct.ProductCategoryID);
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name", editProduct.ProductModelID);
            return View(editProduct);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var productEdit = new ProductEdit
            {
                ProductId = product.ProductID,
                Name = product.Name,
                Number = product.ProductNumber,
                ProductModelID = product.ProductModelID.Value,
                Category = product.ProductCategory.Name,
                ProductCategoryID = product.ProductCategoryID.Value,
                Model = product.ProductModel.Name,
                SellStart = product.SellStartDate,
                StandardCost = product.StandardCost,
                ListPrice = product.ListPrice
            };

            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", productEdit.ProductCategoryID);
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name", productEdit.ProductModelID);
            return View(productEdit);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductEdit editProduct)
        {
            if (ModelState.IsValid)
            {
                Product product = db.Products.Find(editProduct.ProductId);

                product.Name = editProduct.Name;
                product.ProductNumber = editProduct.Number;
                product.SellStartDate = editProduct.SellStart;
                product.StandardCost = editProduct.StandardCost;
                product.ListPrice = editProduct.ListPrice;
                product.ProductModelID = editProduct.ProductModelID;
                product.ProductCategoryID = editProduct.ProductCategoryID;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", editProduct.ProductCategoryID);
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name", editProduct.ProductModelID);
            return View(editProduct);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
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
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
