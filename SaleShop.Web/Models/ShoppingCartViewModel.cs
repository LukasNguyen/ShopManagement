using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleShop.Web.Models
{
    [Serializable]
    public class ShoppingCartViewModel
    {
        public int ProductID { get; set; }
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
    }
}