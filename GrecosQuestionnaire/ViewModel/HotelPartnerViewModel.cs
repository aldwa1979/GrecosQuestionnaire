using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.ViewModel
{
    public class HotelPartnerViewModel
    {
        public int Id { get; set; }
        public string HotelCode { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Destination { get; set; }
        public int Season { get; set; }
        public int? HotelPartnerId { get; set; }
        public string Partner { get; set; }
    }
}
