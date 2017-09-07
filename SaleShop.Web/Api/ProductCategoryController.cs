using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using SaleShop.Model.Models;
using SaleShop.Service;
using SaleShop.Web.Infrastructure.Core;
using SaleShop.Web.Models;

namespace SaleShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;
        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            _productCategoryService = productCategoryService;
        }
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request,int page,int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {

                int totalRow = 0;

                var model = _productCategoryService.GetAll();

                totalRow = model.Count();

                var query = model.OrderByDescending(n => n.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<List<ProductCategoryViewModel>>(query);

                PaginationSet<ProductCategoryViewModel> paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow/pageSize)
                };
                      
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                 
                return response;
            });
        }
    }
}
