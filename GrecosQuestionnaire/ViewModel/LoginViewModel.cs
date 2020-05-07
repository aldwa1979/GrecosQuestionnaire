﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name="Remember me")]
        public bool RememberMe { get; set; }
    }
}
