using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OnlineMedicine.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
