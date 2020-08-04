using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context; //Utilizes InMemoryRepository to load Products from the DB
        IRepository<ProductCategory> productCategories;
        //ProductCategoryRepository productCategories;//Allows us to load productCategories from the DB

        //Creates a constructor for our controller.
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)//allows the constructor to accept the two interfaces
        {
            context = productContext;
            productCategories = productCategoryContext;
            //context = new InMemoryRepository<Product>();
            //productCategories = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        //Method to create the product
        public ActionResult Create()           
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)//Checks to make sure validation is correct.
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);//Assigns the image a product Id 
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);//Saves images to the specified path
                }

                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");//If everything worked right will send users back to index.
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);//Tries to find the product using the find method and by Id
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();//Getting either an empty product or a product from the DB
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)//Edits Product
        {
            Product productToEdit = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }

                productToEdit.Category = product.Category;//Defines what field to edit within the product in this case Category
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)//Deletes product
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id) //Makes the user confirm this is the product they want to delete
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();//Commits the changes the user selected
                return RedirectToAction("Index");
            }
        }
    }
}