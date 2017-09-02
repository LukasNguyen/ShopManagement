using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using SaleShop.Data;
using SaleShop.Data.Infrastructure;
using SaleShop.Data.Repositories;
using SaleShop.Service;

[assembly: OwinStartup(typeof(SaleShop.Web.App_Start.Startup))]

namespace SaleShop.Web.App_Start
{
    //Nhớ cài đặt package Microsoft.Owin.Host.SystemWeb để chạy file Startup này
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigAutofac(app);   
        }

        private void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //Register Controller
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //Register WebApi
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest(); // khi tạo biến IUnitOfWork thì gán type UnitOfWork cho nó
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<SaleShopDbContext>().AsSelf().InstancePerRequest();

            //Repositories
            builder.RegisterAssemblyTypes(typeof(PostCategoryRepository).Assembly)
                .Where(n => n.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();

            //Services
            builder.RegisterAssemblyTypes(typeof(PostCategoryService).Assembly).Where(n => n.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            Autofac.IContainer container = builder.Build(); // gán tất cả những register trên vào cái thùng chứa container này
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); //thay thế cơ chế mặc định bằng cái ta đã regist ở trên

             GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
