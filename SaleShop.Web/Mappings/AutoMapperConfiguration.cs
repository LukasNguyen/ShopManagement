using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SaleShop.Model.Models;
using SaleShop.Web.Models;

namespace SaleShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Post, PostViewModel>();
                    cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                    cfg.CreateMap<PostTag, PostTagViewModel>();
                    cfg.CreateMap<Tag, TagViewModel>();
                    cfg.CreateMap<Product, ProductViewModel>();
                    cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                    cfg.CreateMap<ProductTag, ProductTagViewModel>();
                    cfg.CreateMap<Footer, FooterViewModel>();
                }
            );
        }
    }
}