using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopApp.DataAccess.Concrete
{
    public class CategoryRepository : GenericRepository<Category, ApplicationDbContext>, ICategoryRepository
    {
        public void DeleteFromCategory(int categoryId, int productId)
        {
            using (var context = new ApplicationDbContext())
            {
                var cmd = @"delete from ProductCategory where ProductId=@p0 And CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd, productId, categoryId);
            }
        }

        public Category GetByIdWithProducts(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Categories
                        .Where(i => i.Id == id)
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Product)
                        .FirstOrDefault();
            }
        }
    }
}
