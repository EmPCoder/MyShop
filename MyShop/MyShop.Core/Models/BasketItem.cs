using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class BasketItem : BaseEntity
    {
        public string BasketId { get; set; }//Stre Basket Id
        public string ProductId { get; set; }//Stre Product Id
        public int Quantity { get; set; }//Stre Basket Qty.
    }
}
