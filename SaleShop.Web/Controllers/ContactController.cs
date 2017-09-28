using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BotDetect.Web.Mvc;
using SaleShop.Common;
using SaleShop.Model.Models;
using SaleShop.Service;
using SaleShop.Web.Infrastructure.Extensions;
using SaleShop.Web.Models;

namespace SaleShop.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactDetailService _contactDetailService;
        private IFeedbackService _feedbackService;

        public ContactController(IContactDetailService contactDetailService,IFeedbackService feedbackService)
        {
            _contactDetailService = contactDetailService;
            _feedbackService = feedbackService;
        }
        // GET: Contact
        public ActionResult Index()
        {
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel {ContactDetail = GetDetail()};

            return View(feedbackViewModel);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode","contactCaptcha","Mã xác nhận không đúng")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                Feedback feedback = new Feedback();
                feedback.UpdateFeedback(feedbackViewModel);
                _feedbackService.Create(feedback);
                _feedbackService.Save();
                ViewData["SuccessMessage"] = "Gửi phản hồi thành công";

                //Send mail
                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/contact_template.html"));
                content = content.Replace("{{Name}}", feedbackViewModel.Name);
                content = content.Replace("{{Email}}", feedbackViewModel.Email);
                content = content.Replace("{{Message}}", feedbackViewModel.Message);

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ website", content);
            }
            feedbackViewModel.ContactDetail = GetDetail();
            return View("Index",feedbackViewModel);
        }

        private ContactDetailViewModel GetDetail()
        {
            var model = _contactDetailService.GetDefaultContactDetail();
            var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return contactViewModel;
        }
    }
} 