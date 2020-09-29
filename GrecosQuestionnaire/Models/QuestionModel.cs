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

        [Display(Name = "Page")]
        public virtual int Page { get; set; }

        [Display(Name = "Order")]
        public virtual int Order { get; set; }

        [Display(Name = "Title")]
        public virtual string Title { get; set; }

        [Display(Name = "Subtitle")]
        public virtual string Subtitle { get; set; }

        [Display(Name = "Removed")]
        public virtual bool Removed { get; set; }

        [Display(Name = "Heading")]
        public bool IsHeader { get; set; }

        [Display(Name = "Class")]
        public string Class { get; set; }
    }
}
