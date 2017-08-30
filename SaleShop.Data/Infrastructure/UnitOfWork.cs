using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleShop.Data.Infrastructure
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private SaleShopDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public SaleShopDbContext DbContext
        {
            get { return dbContext ?? (dbContext = new SaleShopDbContext()); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
