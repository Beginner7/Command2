using System;
using System.ComponentModel.DataAnnotations;

namespace ChestClient.Models
{
    public class GameModel
    {
        [Display(Name = "ID игры")]
        public int GameID { get; set; }
        
        [Display(Name = "Имя 1 игрока")]
        public UserModel PlayerOneName{ get; set; }

        [Display(Name = "Имя 2 игрока")]
        public UserModel PlayerTwoName { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Время начала игры")]
        public DateTime TimeNow { get; set; }
    }
}