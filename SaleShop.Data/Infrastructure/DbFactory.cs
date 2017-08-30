using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleShop.Data.Infrastructure
{
    //DbFactory để khởi tạo dbContext
    public class DbFactory : Disposable, IDbFactory
    {
        private SaleShopDbContext dbContext;

        public SaleShopDbContext Init()
        {
            return dbContext ?? (dbContext = new SaleShopDbContext()); // nếu nó null thì khởi tạo cho nó
        }

        protected override void DisposeCore()
        {
            if(dbContext!=null)
                dbContext.Dispose();;
        }
    }
}
