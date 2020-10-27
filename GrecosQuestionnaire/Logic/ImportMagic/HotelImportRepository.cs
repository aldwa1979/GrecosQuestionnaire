using GrecosQuestionnaire.Data;
using GrecosQuestionnaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Logic.ImportMagic
{
    public class HotelImportRepository : IHotelImportRepository
    {
        private readonly HotelImportDBContext _context;

        public HotelImportRepository(HotelImportDBContext context)
        {
            _context = context;
        }

        //Wyszukuję z bazy magica wszystkie istniejące hotele i pokoje we wszystkich sezonach
        public IEnumerable<HotelImportModel> GetAllHotels()
        {
            var x = _context.RoomTypes.ToList();

            return _context.RoomTypes.Where(p => p.HotelCode != null && p.RoomAllocCode != null && p.RoomCode != null && p.DestinationSeasonName != null &&
                                                p.HotelCode != "" && p.RoomAllocCode != "" && p.RoomCode != "" && p.DestinationSeasonName != "").Distinct().ToList();
        }

    }
}
