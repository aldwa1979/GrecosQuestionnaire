using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class HotelModel
    {
        public int Id { get; set; }
        public string HotelCode { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Destination { get; set; }
        public List<MainRoomModel> MainRoom { get; set; } = new List<MainRoomModel>();
        public int Season { get; set; }
    }
}
