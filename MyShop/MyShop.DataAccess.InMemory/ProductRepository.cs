using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>(); //Creates a list of products

        public ProductRepository() //Creates a productRepo
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        /*
         * Searches for a product to update by the product Id if none is found returns Product not found.
         */
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        /*
         * Searches for a product by the product Id if none is found returns Product not found.
         */
        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        /*
        * Searches for a product by the product Id if found deletes it, if not returns Product not found.
        */
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
