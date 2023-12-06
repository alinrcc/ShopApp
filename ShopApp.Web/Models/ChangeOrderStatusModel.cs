using ShopApp.Model;

namespace ShopApp.Web.Models
{
    public class ChangeOrderStatusModel
    {
        public int Id { get; set; }
        public EnumOrderState OrderStatus { get; set; }
    }
}
