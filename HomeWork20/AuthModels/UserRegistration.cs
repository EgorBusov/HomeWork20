using System.ComponentModel.DataAnnotations;

namespace HomeWork20.AuthModels
{
    public class UserRegistration
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
