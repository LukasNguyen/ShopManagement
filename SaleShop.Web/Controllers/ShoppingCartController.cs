using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using Microsoft.AspNet.Identity;
using SaleShop.Common;
using SaleShop.Model.Models;
using SaleShop.Service;
using SaleShop.Web.App_Start;
using SaleShop.Web.Infrastructure.Extensions;
using SaleShop.Web.Models;

namespace SaleShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductService _productService;
        private ApplicationUserManager _userManager;
        private IOrderService _orderService;

        public ShoppingCartController(IProductService productService,ApplicationUserManager userManager,IOrderService orderService)
        {
            _productService = productService;
            _userManager = userManager;
            _orderService = orderService;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>(); 
            return View();
        }

        public ActionResult CheckOut()
        {
            if (Session[CommonConstants.SessionCart] == null)
                return Redirect("/gio-hang.html");
            return View();
        }

        public JsonResult GetUser()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _userManager.FindById(userId);
                return Json(new {data = user, status = true});
            }
            return Json(new { status = false });
        }
        public JsonResult CreateOrder(string orderViewModel)
        {
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);

            var orderNew = new Order();
            orderNew.UpdateOrder(order);

            if (Request.IsAuthenticated)
            {
                orderNew.CustomerID = User.Identity.GetUserId();
                orderNew.CreatedBy = User.Identity.GetUserName();
            }

            orderNew.CreatedDate = DateTime.Now;

            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            bool isEnough = true;
            foreach (var item in cart)
            {
                var detail = new OrderDetail();
                detail.ProductID = item.ProductID;
                detail.Quantity = item.Quantity;
                detail.Price = item.Product.Price;
                orderDetails.Add(detail);
                _productService.SellProduct(item.ProductID, item.Quantity);

                isEnough = _productService.SellProduct(item.ProductID, item.Quantity);
                if(!isEnough)
                    break;
            }

            if (isEnough)
            {
                orderNew.OrderDetails = orderDetails;

                _orderService.Create(orderNew);

                return Json(new { status = true });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    message = "Không đủ hàng."
                });
            }
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
            var product = _productService.GetById(productID);
            if (cart == null)
                cart = new List<ShoppingCartViewModel>();

            if (product.Quantity == 0)
            {
                return Json(new { status = false, message = "Sản phẩm này hiện đang hết hàng" });
            }
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
            try
            {
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
            catch
            {
                return Json(new { status = false });
            }
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