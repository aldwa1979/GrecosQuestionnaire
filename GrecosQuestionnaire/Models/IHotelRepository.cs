﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public interface IHotelRepository
    {
        IEnumerable<HotelModel> GetAllHotels();
        int GetHotelId(string code, int season);
        int GetMainRoomlId(string hotelCode, string roomCode, int season);
        HotelModel GetHotel(string code, int season);
        void UploadNewHotels(HotelModel hotel);
        void UploadNewRooms(MainRoomModel hotel);
        void UploadNewShareds(SharedUnitModel hotel);
        MainRoomModel GetMainRoom(string mainRoomCode, string hotelCode, int season);
        SharedUnitModel GetSharedUnit(string sharedUnitCode, string hotelCode, string roomCode, int season);
        void UploadNewSeason(SeasonModel season);
    }
}
