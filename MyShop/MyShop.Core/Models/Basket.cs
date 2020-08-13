using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Basket : BaseEntity
    {
        public virtual ICollection<BasketItem> BasketItems { get; set; }//Virtual ICollection lets framework know to load all basket items as well from DB "Lazy Loading"

        public Basket()
        {
            this.BasketItems = new List<BasketItem>();
        }
    }
}
