using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleShop.Data.Infrastructure
{
    //Giao tiếp để khởi tạo các đối tượng
    public interface IDbFactory:IDisposable
    {
        SaleShopDbContext Init();
    }
}
