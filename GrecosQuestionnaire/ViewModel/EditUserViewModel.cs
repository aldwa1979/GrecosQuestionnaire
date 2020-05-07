using GrecosQuestionnaire.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public List<PartnerModel> Partners { get; set; }
    }
}
