using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.ViewModel
{
    public class RoleViewModel
    {
        private string _roleName;

        [Required]
        public string RoleName
        {
            get
            {
                return _roleName;
            }
            set
            {
                _roleName = value;
            }
        }

        public RoleViewModel(string roleName)
        {
            _roleName = roleName;
        }
    }
}
