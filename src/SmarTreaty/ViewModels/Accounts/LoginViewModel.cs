using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.ViewModels.Accounts
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}