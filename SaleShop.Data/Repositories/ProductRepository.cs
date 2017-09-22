using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleShop.Data.Infrastructure;
using SaleShop.Model.Models;

namespace SaleShop.Data.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {
        IEnumerable<Product> GetListProductByTagID(string tagid,int page ,int pagesize,out int totalRow);
    }
    public class ProductRepository:RepositoryBase<Product>,IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }

        public IEnumerable<Product> GetListProductByTagID(string tagid, int page, int pagesize, out int totalRow)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.ID equals pt.ProductID
                        where pt.TagID == tagid
                        select p;
            totalRow = query.Count();

            return query.OrderByDescending(n=>n.CreatedDate).Skip((page-1)*pagesize).Take(pagesize);
        }
    }
}
