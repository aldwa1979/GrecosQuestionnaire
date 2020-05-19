using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class ResponseModel
    {
        public int Id { get; set; }

        public virtual DateTime ResponseDate { get; set; }

        public int HotelId { get; set; }

        public string UserName { get; set; }
    }
}
