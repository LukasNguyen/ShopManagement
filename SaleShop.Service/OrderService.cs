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
        Order Create(Order order);
        void UpdateStatus(int ID);
        void Save();
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

        public Order Create(Order order)
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

                return order;
            }
            catch(Exception ex)
            {
                return null;
                throw;
            }
        }

        public void UpdateStatus(int ID)
        {
            var order = _orderRepository.GetSingleById(ID);
            order.Status = true;
            _orderRepository.Update(order);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

    }
}
