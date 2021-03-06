﻿using GrecosQuestionnaire.Data.Enum;
using System.Collections.Generic;

namespace GrecosQuestionnaire.Models
{
    public class QuestionItem
    {
        public int Id { get; set; }

        public virtual Question Question { get; set; }

        public virtual int ItemOrder { get; set; }

        public virtual string Title { get; set; }

        public virtual string Items { get; set; }

        public virtual QuestionItemType QuestionItemType { get; set; }

        public virtual ICollection<QuestionItemItem> ItemItems { get; set; }

        public virtual int Parts { get; set; }

        public virtual int SingleSpace { get; set; }

        public virtual string Class { get; set; }

        public virtual bool Required { get; set; }
    }
}
