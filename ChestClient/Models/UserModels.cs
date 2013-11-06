using System.ComponentModel.DataAnnotations;

namespace ChestClient.Models
{
    public class UserModels
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

    }
}