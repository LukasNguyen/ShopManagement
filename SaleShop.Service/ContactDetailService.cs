using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleShop.Data.Infrastructure;
using SaleShop.Data.Repositories;
using SaleShop.Model.Models;

namespace SaleShop.Service
{
    public interface IContactDetailService
    {
        ContactDetail GetDefaultContactDetail();
    }
    public class ContactDetailService:IContactDetailService
    {
        private IContactDetailRepository _contactDetailRepository;
        private IUnitOfWork _unitOfWork;

        public ContactDetailService(IContactDetailRepository contactDetailRepository,IUnitOfWork unitOfWork)
        {
            _contactDetailRepository = contactDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public ContactDetail GetDefaultContactDetail()
        {
            return _contactDetailRepository.GetSingleByCondition(n => n.Status);
        }
    }
}
