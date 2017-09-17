using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ProductViewModel> LastestProducts { get; set; }
        public IEnumerable<SlideViewModel> Slides { get; set; }
        public IEnumerable<ProductViewModel> TopSaleProducts { get; set; }
    }
}