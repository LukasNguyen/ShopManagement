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
    public class ContactController : Controller
    {
        private IContactDetailService _contactDetailService;

        public ContactController(IContactDetailService contactDetailService)
        {
            _contactDetailService = contactDetailService;
        }
        // GET: Contact
        public ActionResult Index()
        {
            var model = _contactDetailService.GetDefaultContactDetail();
            var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return View(contactViewModel);
        }
    }
}