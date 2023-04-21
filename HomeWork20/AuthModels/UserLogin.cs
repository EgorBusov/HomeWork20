using System.ComponentModel.DataAnnotations;

namespace HomeWork20.AuthModels
{
    public class UserLogin
    {
        [Required,MaxLength(50)]
        public string UserName { get; set; }

        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// перенаправление после аутентификации
        /// </summary>
        public string? ReturnUrl { get; set; }
    }
}
