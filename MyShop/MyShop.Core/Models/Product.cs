using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product
    {
        public string Id { get; set; }

        //[StringLength(20)]//Defines the possible length of the display name
        [DisplayName("Product Name")]//Defines the name to be displayed when a product is added
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, 1000)]//Sets the range that someone can charge for a product
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        //Creates a product and assigns and new GUID to it
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
