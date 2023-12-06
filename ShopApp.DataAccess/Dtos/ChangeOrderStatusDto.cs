using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Dtos
{
    public class ChangeOrderStatusDto
    {
        public int Id { get; set; }
        public EnumOrderState OrderStatus { get; set; }
    }
}
