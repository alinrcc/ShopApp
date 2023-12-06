using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Dtos;
using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderRepository _orderRepository;
        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void ChangeOrderStatus(ChangeOrderStatusDto dto)
        {
            _orderRepository.ChangeOrderStatus(dto);
        }

        public void Create(Order entity)
        {
            _orderRepository.Create(entity);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public List<Order> GetOrders(string userId)
        {
            return _orderRepository.GetOrders(userId);
        }

        public ChangeOrderStatusDto GetOrderStatus(int id)
        {
            return _orderRepository.GetOrderStatus(id);
        }
    }
}
