using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SaleShop.Model.Models;
using SaleShop.Service;
using SaleShop.Web.Models;

namespace SaleShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private ICommonService _commonService;

        public HomeController(IProductCategoryService productCategoryService,ICommonService commonService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel); 
            return PartialView(footerViewModel); // PartialView("Footer"); chỉ ra đúng tên view khi action và view không trùng tên
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView(); // PartialView("Header"); chỉ ra đúng tên view khi action và view không trùng tên
        }
        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();

            var listProductCategoryViewModel = Mapper
                .Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);

            return PartialView(listProductCategoryViewModel);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}