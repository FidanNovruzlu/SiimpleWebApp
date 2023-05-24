using System.ComponentModel.DataAnnotations;

namespace SiimpleWebApp.ViewModels.AccountVM
{
    public class RegisterVM
    {
        public string Name { get; set; } = null!;
        [EmailAddress,Required]
        public string Email { get; set; } = null!;
        public string Surname { get; set; } = null!;
        [MaxLength(20),Required]
        public string UserName { get; set; } = null!;
        [Required,DataType(DataType.Password),MinLength(8)]
        public string Password { get; set; } = null!;
        [Required, DataType(DataType.Password), MinLength(8), Compare(nameof(Password))]
        public string ConfrimPassword { get; set; } = null!;
    }
}
