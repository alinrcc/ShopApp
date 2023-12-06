using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.DataAccess.Abstract
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetByUserId(string userId);
        void DeleteFromCart(int cartId, int productId);
        void ClearCart(string cartId);
    }
}
