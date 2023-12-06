using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Dtos;
using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.DataAccess.Concrete
{
    public class OrderRepository : GenericRepository<Order, ApplicationDbContext>, IOrderRepository
    {
        public void ChangeOrderStatus(ChangeOrderStatusDto dto)
        {
            using (var context = new ApplicationDbContext())
            {
                var orders = context.Orders.Find(dto.Id);
                orders.OrderState = dto.OrderStatus;
                context.SaveChanges();
            }
        }

        public List<Order> GetAllOrders()
        {
            using (var context = new ApplicationDbContext())
            {
                var orders = context.Orders
                                .Include(i => i.OrderItems)
                                .ThenInclude(i => i.Product)
                                .AsQueryable();
                return orders.ToList();
            }
        }

        public List<Order> GetOrders(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var orders = context.Orders
                                .Include(i => i.OrderItems)
                                .ThenInclude(i => i.Product)
                                .AsQueryable();

                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(i => i.UserId == userId);
                }

                return orders.ToList();
            }
        }

        public ChangeOrderStatusDto GetOrderStatus(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var value = (from order in context.Orders
                             where order.Id == id
                             select new ChangeOrderStatusDto()
                             {
                                 Id = order.Id,
                                 OrderStatus = order.OrderState
                             }).FirstOrDefault();
                return value;
            }
        }
    }
}

