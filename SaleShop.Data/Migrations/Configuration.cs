using System.Collections.Generic;
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
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SaleShop.Data.SaleShopDbContext context)
        {
            //CreateProductCategoryExample(context);
            CreateSlideExample(context);
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
        private void CreateFooterExample(SaleShop.Data.SaleShopDbContext context)
        {
            if (context.Footers.Count(n=>n.Content == CommonConstants.DefaultFooterId) == 0)
            {
              
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
    }
}
