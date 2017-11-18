using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using SaleShop.Model.Models;
using SaleShop.Service;
using SaleShop.Web.Infrastructure.Core;
using SaleShop.Web.Models;

namespace SaleShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;

        //Get instance không qua controller trong DI
        //private IProductService _productServiceTest = ServiceFactory.Get<IProductService>();

        public ProductController(IProductService productService,IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }
        // GET: Product
        public ActionResult Detail(int id)
        {
            var productModel = _productService.GetById(id);
            var productViewModel = Mapper.Map<Product, ProductViewModel>(productModel);
            var relatedProduct = _productService.GetRelatedProducts(id, 6);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProduct);
            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>,IEnumerable<TagViewModel>>(_productService.GetListTagByProductID(id));
            List<string> lstImages = new JavaScriptSerializer().Deserialize<List<string>>(productViewModel.MoreImages);
            ViewBag.MoreImages = lstImages;
            return View(productViewModel);
        }

        public ActionResult Category(int id,int page = 1,string sort="")
        {
            int pageSize = int.Parse(Common.ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByCategoryPaging(id, page, pageSize,sort,out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);

            var category = _productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);


            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = productViewModel,
                MaxPage = int.Parse(Common.ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = (int)Math.Ceiling((double)totalRow/pageSize)
            };

            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var lstName = _productService.GetListProductByName(keyword);
            //var viewmodel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>> (model);
            return Json(new {data = lstName}, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(Common.ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);

            ViewBag.Keyword = keyword;

            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = productViewModel,
                MaxPage = int.Parse(Common.ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = (int)Math.Ceiling((double)totalRow / pageSize)
            };

            return View(paginationSet);
        }

        public ActionResult ListByTag(string tagid,int page = 1)
        {
            int pageSize = int.Parse(Common.ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByTagID(tagid, page, pageSize, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);

            ViewBag.Tag = Mapper.Map<Tag,TagViewModel>(_productService.GetTag(tagid));

            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = productViewModel,
                MaxPage = int.Parse(Common.ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = (int)Math.Ceiling((double)totalRow / pageSize)
            };

            return View(paginationSet);
        }
    }
}