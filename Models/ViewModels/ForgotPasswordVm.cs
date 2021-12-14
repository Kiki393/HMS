using System.ComponentModel.DataAnnotations;

namespace HMS.Models.ViewModels
{
    public class ForgotPasswordVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}