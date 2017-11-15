using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleShop.Common.ViewModels;
using SaleShop.Data.Infrastructure;
using SaleShop.Data.Repositories;
using SaleShop.Model.Models;

namespace SaleShop.Service
{
    public interface IOrderService
    {
        bool Create(Order order);
    }
    public class OrderService:IOrderService
    {
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Create(Order order)
        {
            try
            {
                _orderRepository.Add(order);

                foreach (var orderDetail in order.OrderDetails)
                {
                    orderDetail.OrderID = order.ID;
                    _orderDetailRepository.Add(orderDetail);
                }

                _unitOfWork.Commit();

                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
