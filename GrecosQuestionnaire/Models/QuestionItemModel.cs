using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class QuestionItemModel
    {
        public int Id { get; set; }

        public virtual QuestionModel Question { get; set; }

        public virtual int ItemOrder { get; set; }

        public virtual string Title { get; set; }

        public virtual string Items { get; set; }

        public virtual QuestionItemType QuestionItemType { get; set; }

        public virtual int Parts { get; set; }

        public virtual int SingleSpace { get; set; }

        public virtual bool Required { get; set; }
    }
}
