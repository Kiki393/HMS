namespace HMS.Models.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class LoginVm
    {
        [Required]
        public string Email { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [DisplayName("Remember Me?")]
        public bool RememberMe { get; init; }
    }
}