using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _23DH114304_MyStore.Models;
using _23DH114304_MyStore.Models.ViewModel;
using PagedList;


namespace _23DH114304_MyStore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // GET: Admin/Home
        public ActionResult Index( string searchTerm, int? page)
        {
            var model = new HomeProductVM();
            var products = db.Products.AsQueryable();


            if (!string.IsNullOrEmpty(searchTerm) )
            {
                model.SearchTerm = searchTerm;
                products = products.Where( p => p.ProductName.Contains(searchTerm) ||
                                        p.ProductDescription.Contains(searchTerm) ||
                                        p.Category.CategoryName.Contains(searchTerm));
            }

            int pageNumber = page ?? 1;
            int pageSize = 6;

            model.FeaturedProducts = products.OrderByDescending( p => p.OrderDetails.Count()).Take(10).ToList();
            model.NewProducts = products.OrderBy(p => p.OrderDetails.Count()).Take(20).ToPagedList(pageNumber, pageSize);

            return View();
        }
    }
}