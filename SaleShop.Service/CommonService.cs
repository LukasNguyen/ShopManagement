using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleShop.Common;
using SaleShop.Data.Infrastructure;
using SaleShop.Data.Repositories;
using SaleShop.Model.Models;

namespace SaleShop.Service
{
    public interface ICommonService
    {
        Footer GetFooter();
    }
    public class CommonService : ICommonService
    {
        private IUnitOfWork _unitOfWork;
        private IFooterRepository _footerRepository;

        public CommonService(IFooterRepository footerRepository, IUnitOfWork unitOfWork)
        {
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
        }
        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(n => n.ID == CommonConstants.DefaultFooterId);
        }
    }
}
