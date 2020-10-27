using GrecosQuestionnaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Logic.ImportMagic
{
    public interface IHotelImportRepository
    {
        IEnumerable<HotelImportModel> GetAllHotels();
    }
}
