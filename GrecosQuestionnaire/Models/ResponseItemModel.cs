using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class ResponseItemModel
    {
        public int Id { get; set; }

        public virtual QuestionItem QuestionItem { get; set; }

        public virtual ResponseModel Response { get; set; }

        public virtual string RawValue { get; set; }

        public virtual string Value { get; set; }
    }
}
