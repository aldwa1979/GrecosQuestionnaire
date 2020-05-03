using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public interface IHotelImportRepository
    {
        IEnumerable<HotelImportModel> GetAllHotels();
    }
}
