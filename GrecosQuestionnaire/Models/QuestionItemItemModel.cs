﻿using GrecosQuestionnaire.Data.Enum;

namespace GrecosQuestionnaire.Models
{
    public class QuestionItemItemModel
    {
        public long Id { get; set; }

        public virtual long QuestionItemId { get; set; }

        public virtual int Order { get; set; }

        public virtual string Title { get; set; }

        public virtual string Items { get; set; }

        public virtual QuestionItemType QuestionItemType { get; set; }

        public virtual int Parts { get; set; }

        public virtual int SingleSpace { get; set; }

        public virtual string SubClass { get; set; }

        public virtual bool Required { get; set; }
    }
}

