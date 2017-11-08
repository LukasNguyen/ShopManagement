using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleShop.Web.Models
{
    public class OrderDetailViewModel
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}