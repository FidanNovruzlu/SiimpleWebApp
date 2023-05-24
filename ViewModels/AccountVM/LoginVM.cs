using System.ComponentModel.DataAnnotations;

namespace SiimpleWebApp.ViewModels.AccountVM
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required,DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
