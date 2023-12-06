using ShopApp.DataAccess.Dtos;
using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Abstract
{
    public interface IOrderService
    {
        void Create(Order entity);
        List<Order> GetOrders(string userId);
        List<Order> GetAllOrders();
        void ChangeOrderStatus(ChangeOrderStatusDto dto);
        ChangeOrderStatusDto GetOrderStatus(int id);
    }
}
