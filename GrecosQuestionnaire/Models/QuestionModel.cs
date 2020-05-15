using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class QuestionModel
    {
        public long Id { get; set; }

        [Display(Name = "Strona")]
        public virtual int Page { get; set; }

        [Display(Name = "Kolejność")]
        public virtual int Order { get; set; }

        [Display(Name = "Tytuł")]
        public virtual string Title { get; set; }

        [Display(Name = "Podtytuł")]
        public virtual string Subtitle { get; set; }

        [Display(Name = "Usunięty")]
        public virtual bool Removed { get; set; }

        [Display(Name = "Nagłówek")]
        public bool IsHeader { get; set; }

        [Display(Name = "Klasa")]
        public string Class { get; set; }
    }
}
