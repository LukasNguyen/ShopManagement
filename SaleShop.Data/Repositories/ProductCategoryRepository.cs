using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleShop.Data.Infrastructure;
using SaleShop.Model.Models;

namespace SaleShop.Data.Repositories
{
    public interface IProductCategoryRepository: IRepository<ProductCategory>
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);
    }
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>,IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbFactory dbFactory):base(dbFactory)
        {
            
        }

        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return this.DbContext.ProductCategories.Where(n => n.Alias == alias);
        }
    }
}
