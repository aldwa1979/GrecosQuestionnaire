using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public interface IHotelRepository
    {
        IEnumerable<HotelModel> GetAllHotels();
        List<PartnerModel> GetPartners();
        HotelModel GetHotelId(int id);
        List<UserPartnerModel> GetUsersPartners();
        int GetHotelId(string code, int season);
        int GetMainRoomlId(string hotelCode, string roomCode, int season);
        HotelModel GetHotel(string code, int season);
        void UploadNewHotels(HotelModel hotel);
        void UploadNewRooms(MainRoomModel hotel);
        void UploadNewShareds(SharedUnitModel hotel);
        MainRoomModel GetMainRoom(string mainRoomCode, string hotelCode, int season);
        SharedUnitModel GetSharedUnit(string sharedUnitCode, string hotelCode, string roomCode, int season);
        void AddNewPartner(PartnerModel partner);
        void UploadNewSeason(SeasonModel season);
        void UploadMatchUserPartner(UserPartnerModel userPartner);
        void RemoveMatchUserPartner(int id);
        List<Question> GetQuestions();
        List<QuestionItem> GetQuestionItems();
        void UploadQuestionItems(QuestionItem questionsItems);
        void RemoveQuestionItems(QuestionItem questionsItems);
        void UploadQuestions(Question questions);
        void RemoveQuestion(Question questions);
        QuestionItem GetQuestionItem(int id);
    }
}
