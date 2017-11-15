﻿using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SaleShop.Common;
using SaleShop.Model.Models;

namespace SaleShop.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SaleShop.Data.SaleShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SaleShop.Data.SaleShopDbContext context)
        {
            //CreateProductCategoryExample(context);
            //CreateSlideExample(context);
            //CreatePageExample(context);
            //CreateContactDetailExample(context);
            CreateConfigTitle(context);
        }
        private void CreateUserExample(SaleShop.Data.SaleShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SaleShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new SaleShopDbContext()));

            //Tạo mới user
            var user = new ApplicationUser()
            {
                UserName = "youandpro",
                Email = "dat.nguyenthaithanh@hotmail.com",
                BirthDay = DateTime.Now,
                FullName = "Lukas Nguyen    "
            };

            manager.Create(user, "123456789");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole() { Name = "Admin" });
                roleManager.Create(new IdentityRole() { Name = "User" });

                var adminUser = manager.FindByEmail("dat.nguyenthaithanh@hotmail.com");

                manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            }
        }
        private void CreateProductCategoryExample(SaleShop.Data.SaleShopDbContext context)
        {
            if (!context.ProductCategories.Any())
            {
                context.ProductCategories.AddRange(new List<ProductCategory>()
                {
                    new ProductCategory(){Name = "Điện Lạnh",Alias = "dien-lanh",Status = true},
                    new ProductCategory(){Name = "Viễn thông",Alias = "vien-thong",Status = true},
                    new ProductCategory(){Name = "Đồ gia dụng",Alias = "do-gia-dung",Status = true},
                new ProductCategory(){Name = "Mỹ phẩm",Alias = "my-pham",Status = true}
                });
                context.SaveChanges();
            }
        }
        private void CreateSlideExample(SaleShop.Data.SaleShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide()
                    {
                        Name = "Slide 1",
                        DisplayOrder = 1 ,
                        Status = true,
                        Url = "#",
                        Image = "/Assets/client/images/bag.jpg",
                        Content = @"<h2>FLAT 50% 0FF</h2>
                        <label>FOR ALL PURCHASE <b>VALUE</b></label>
                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                        <span class=""on-get"">GET NOW</span>"
                    },
                    new Slide()
                    {
                        Name = "Slide 2",
                        DisplayOrder = 2 ,
                        Status = true,
                        Url = "#",
                        Image = "/Assets/client/images/bag1.jpg",
                        Content = @"<h2>FLAT 50% 0FF</h2>
                        <label>FOR ALL PURCHASE <b>VALUE</b></label>
                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                        <span class=""on-get"">GET NOW</span>"
                    }
                }; 
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }
        private void CreatePageExample(SaleShop.Data.SaleShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                var page = new Page()
                {
                    Name = "Giới thiệu",
                    Alias = "gioi-thieu",
                    Content = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?",
                    Status = true
                };
                context.Pages.Add(page);
                context.SaveChanges();
            }
        }
        private void CreateContactDetailExample(SaleShop.Data.SaleShopDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                var contactDetail = new SaleShop.Model.Models.ContactDetail()
                {
                    Name = "Lukas Shop",
                    Address = "Thành phố Hồ Chí Minh",
                    Email = "dat.nguyenthaithanh@hotmail.com",
                    Lat = 10.760501,
                    Lng = 106.663296,
                    Phone = "0949209493",
                    Website = "lukasnguyen.com",
                    Other = "",
                    Status = true

                };
                context.ContactDetails.Add(contactDetail);
                context.SaveChanges();
            }
        }

        private void CreateConfigTitle(SaleShopDbContext context)
        {
            if (!context.SystemConfigs.Any(x => x.Code == "HomeTitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    ValueString = "Trang chủ LukasShop",

                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    ValueString = "Trang chủ LukasShop",

                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    ValueString = "Trang chủ LukasShop",

                });
            }
            context.SaveChanges();
        }
    }
}
