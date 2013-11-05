using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace ChestClient2.Models
{
    public class UserModels
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

    }
}