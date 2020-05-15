using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class Question
    {
        public int Id { get; set; }

        public virtual int ItemPage { get; set; }

        public virtual int ItemOrder { get; set; }

        public virtual string Title { get; set; }

        public virtual string Subtitle { get; set; }

        public virtual bool IsHeader { get; set; }

        public virtual bool Removed { get; set; }

        public virtual ICollection<QuestionItem> Items { get; set; }

        public virtual string Class { get; set; }
    }
}
