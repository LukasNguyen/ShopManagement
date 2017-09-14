using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SaleShop.Service;
using SaleShop.Web.Infrastructure.Core;

namespace SaleShop.Web.Api
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        private IErrorService _errorService;
        public HomeController(IErrorService errorService) : base(errorService)
        {
            _errorService = errorService;
        }

        [Route("TestMethod")]
        [HttpGet]
        public string TestMethod()
        {
            return "Hello , Lukas member";
        }

    }
}
