using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class HotelImportModel
    {
        public string HotelCode { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Destination { get; set; }
        public string RoomDesc { get; set; }
        public string RoomAllocCode { get; set; }
        public string RoomCode { get; set; }
        public int FixedPlaces { get; set; }
        public int MaxAdults { get; set; }
        public int MaxAdultsWithChildren { get; set; }
        public int MaxAdultsAndChildrenIncludesInfants { get; set; }
        public int MaxInfants { get; set; }
        public int MaxChildrenAndInfantsWithoutBed { get; set; }
        public string DestinationSeasonName { get; set; }
    }
}
