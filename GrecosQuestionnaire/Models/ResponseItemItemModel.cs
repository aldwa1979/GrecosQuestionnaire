using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class ResponseItemItemModel
    {
        public int Id { get; set; }

        public virtual QuestionItemItem QuestionItemItem { get; set; }

        public virtual ResponseItemModel ResponseItem { get; set; }

        public virtual string RawValue { get; set; }

        public virtual string Value { get; set; }
    }
}
