using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleShop.Web.App_Start;
using Microsoft.AspNet.Identity.Owin;
using SaleShop.Model.Models;
using SaleShop.Web.Models;
using System.Threading.Tasks;
using BotDetect.Web.Mvc;
using SaleShop.Common;

namespace SaleShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            //Thêm .Current
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            //Thêm .Current
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Account
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel,string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;
            return View(loginViewModel);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var clientUser = await _userManager.FindByEmailAsync(model.Email);

                if (clientUser != null)
                {
                    ModelState.AddModelError("email", "Email đã tồn tại trong hệ thống");
                    return View(model);
                }

                var userByUserName = await _userManager.FindByNameAsync(model.UserName);

                if (userByUserName != null)
                {
                    ModelState.AddModelError("name", "Tài khoản đã tồn tại trong hệ thống");
                    return View(model);
                }

                //Tạo mới user
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address
                };

                await _userManager.CreateAsync(user, model.Password);

                await _userManager.AddToRoleAsync(user.Id,"User");


                //Send mail
                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/register_template.html"));
                content = content.Replace("{{UserName}}", model.UserName);
                content = content.Replace("{{Password}}", model.Password);
                content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink")+"dang-nhap.html");

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "Đăng ký thành công tài khoản ở LUKAS Shop", content);


                ViewData["SuccessMessage"] = "Đăng ký tài khoản thành công";
            }
            return View();
        }
    }
}