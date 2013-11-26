using System.ComponentModel.DataAnnotations;

namespace ChestClient.Models
{
    public class LogOnModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}
