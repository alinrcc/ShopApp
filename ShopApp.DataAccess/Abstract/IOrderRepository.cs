using ShopApp.DataAccess.Dtos;
using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.DataAccess.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetOrders(string userId);
        List<Order> GetAllOrders();
        void ChangeOrderStatus(ChangeOrderStatusDto dto);

        ChangeOrderStatusDto GetOrderStatus(int id);
    }
}
