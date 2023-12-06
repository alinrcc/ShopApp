using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.DataAccess.Concrete
{
    public class CartRepository : GenericRepository<Cart, ApplicationDbContext>, ICartRepository
    {
        public override void Update(Cart entity)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }

        public Cart GetByUserId(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context
                            .Carts
                            .Include(i => i.CartItems)
                            .ThenInclude(i => i.Product)
                            .FirstOrDefault(i => i.UserId == userId);
            }
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context = new ApplicationDbContext())
            {
                var cmd = @"delete from CartItem where CartId={0} And ProductId={1}";
                context.Database.ExecuteSqlRaw(cmd, cartId, productId);
            }
        }

        public void ClearCart(string cartId)
        {
            using (var context = new ApplicationDbContext())
            {
                var cmd = @"delete from CartItem where CartId={0}";
                object value = context.Database.ExecuteSqlRaw(cmd, cartId);
            }
        }
    }
    }

