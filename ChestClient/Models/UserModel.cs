using System.ComponentModel.DataAnnotations;

namespace ChestClient.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
    }
}