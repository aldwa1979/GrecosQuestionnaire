using GrecosQuestionnaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Logic.Hotels
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
        void UploadHotels(HotelModel hotel);
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
        List<QuestionItemItem> GetQuestionItemItems();

        void UploadQuestionItemItems(QuestionItemItem questionsItemItems);
        void RemoveQuestionItemItems(QuestionItemItem questionsItemItems);
        void UploadQuestionItems(QuestionItem questionsItems);
        void RemoveQuestionItems(QuestionItem questionsItems);
        void UploadQuestions(Question questions);
        void RemoveQuestion(Question questions);

        QuestionItem GetQuestionItem(int id);
        QuestionItemItem GetQuestionItemItem(int id);

        List<ResponseModel> GetResponses();
        List<ResponseItemModel> GetResponseItem();
        List<ResponseItemItemModel> GetResponseItemItem();
        void UploadResponses(ResponseModel response);
        void UploadResponseItems(ResponseItemModel responseItem);
        void UploadResponseItemItems(ResponseItemItemModel responseItemItem);
    }
}
