using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using SaleShop.Common;
using SaleShop.Model.Models;
using SaleShop.Service;
using SaleShop.Web.Models;

namespace SaleShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductService _productService;

        public ShoppingCartController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>(); 
            return View();
        }

        public JsonResult GetAll()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();

            var cart = (List<ShoppingCartViewModel>) Session[CommonConstants.SessionCart];
            return Json(new {data = cart,status = true}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Add(int productID)
        {
            var cart = (List<ShoppingCartViewModel>) Session[CommonConstants.SessionCart];
            if (cart == null)
                cart = new List<ShoppingCartViewModel>();

            if (cart.Any(n => n.ProductID == productID))
            {
                foreach (var item in cart)
                {
                    if (item.ProductID == productID)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductID = productID;
                var product = _productService.GetById(productID);
                newItem.Product = Mapper.Map<Product, ProductViewModel>(product);
                newItem.Quantity = 1;
                cart.Add(newItem);
            }
            Session[CommonConstants.SessionCart] = cart;
            return Json(new {status = true});
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);

            foreach (var item in cartSession)
            {
                foreach (var jitem in cartViewModel)
                {
                    if (item.ProductID == jitem.ProductID)
                    {
                        item.Quantity = jitem.Quantity;
                    }
                }
            }
            Session[CommonConstants.SessionCart] = cartSession;
            return Json(new {status = true});
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return Json(new {status = true});
        }
        [HttpPost]
        public JsonResult DeleteItem(int productID)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cartSession != null)
            {
                cartSession.RemoveAll(n => n.ProductID == productID);
                Session[CommonConstants.SessionCart] = cartSession;
                return Json(new { status = true });
            }
            return Json(new { status = false });
        }
    }
}