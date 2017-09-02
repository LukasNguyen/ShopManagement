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
    public interface IErrorService
    {
        Error CreateError(Error error);
        void Save(); 

    }
    public class ErrorService:IErrorService
    {
        private IErrorRepository _errorRepository;
        private IUnitOfWork _unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            _errorRepository = errorRepository;
            _unitOfWork = unitOfWork;
        }

        public Error CreateError(Error error)
        {
            return _errorRepository.Add(error);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
