using GrecosQuestionnaire.Data.Enum;

namespace GrecosQuestionnaire.Models
{
    public class QuestionItemModel
    {
        public long Id { get; set; }

        public virtual long QuestionId { get; set; }

        public virtual int Order { get; set; }

        public virtual string Title { get; set; }

        public virtual string Items { get; set; }

        public virtual QuestionItemType QuestionItemType { get; set; }

        public virtual int Parts { get; set; }

        public virtual int SingleSpace { get; set; }

        public string Class { get; set; }

        public virtual bool Required { get; set; }
    }
}
