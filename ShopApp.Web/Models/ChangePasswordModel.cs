



using System.ComponentModel.DataAnnotations;

namespace ShopApp.Web.Models
{
    public class ChangePasswordModel
    {
        public string UserId { get; set; }

        [Required]
        public string PastPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        public string NewPasswordAgain { get; set; }
    }
}
