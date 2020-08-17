using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context; //Utilizes InMemoryRepository to load Products from the DB
        IRepository<ProductCategory> productCategories;
        //ProductCategoryRepository productCategories;//Allows us to load productCategories from the DB

        //Creates a constructor for our controller.
        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)//allows the constructor to accept the two interfaces
        {
            context = productContext;
            productCategories = productCategoryContext;
            //context = new InMemoryRepository<Product>();
            //productCategories = new InMemoryRepository<ProductCategory>();
        }

        public ActionResult Index(string Category=null)
        {
            List<Product> products = context.Collection().ToList();//Creates a list of products
            List<ProductCategory> categories = productCategories.Collection().ToList();

            if(Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;

            return View(model);//Updates the view to return the created ProductListViewModel instead of previous
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}