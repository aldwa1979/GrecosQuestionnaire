using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.ViewModel
{
    public class UserViewModel
    {
        private string _email;

        public UserViewModel()
        {

        }

        public UserViewModel(string email)
        {
            _email = email;
        }

        [Required]
        [Display(Name = "Adres e-mail")]
        
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }

        }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage="Hasło nie pasuje")]
        public string ConfirmPassword { get; set; }
    }
}
