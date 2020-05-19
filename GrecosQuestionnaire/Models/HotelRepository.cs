using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelDBContext _context;

        public HotelRepository(HotelDBContext context)
        {
            _context = context;
        }

        //Pobieram listę wszystkich hoteli
        public IEnumerable<HotelModel> GetAllHotels()
        {
            return _context.Hotels.ToList();
        }

        //Szukam wszystkich partnerów
        public List<PartnerModel> GetPartners()
        {
            return _context.Partners.ToList();
        }

        //Szukam wszystkich powiązań partnerów i użytkownikó
        public List<UserPartnerModel> GetUsersPartners()
        {
            return _context.UsersPartners.ToList();
        }

        //Szukam id hotelu w bazie hoteli po id
        public HotelModel GetHotelId(int id)
        {
            return _context.Hotels.Where(p => p.Id == id).FirstOrDefault();
        }

        //Szukam id hotelu w bazie hoteli po kodzie i sezonie
        public int GetHotelId(string hotelCode, int season)
        {
            return _context.Hotels.Where(p => p.HotelCode == hotelCode && p.Season == season).FirstOrDefault().Id;
        }

        //Szukam hotel w bazie hoteli
        public HotelModel GetHotel(string hotelCode, int season)
        {
            return _context.Hotels.Where(p => p.HotelCode == hotelCode && p.Season==season).FirstOrDefault();
        }

        //Szukam id głównego pokoju w bazie hoteli
        public int GetMainRoomlId(string hotelCode, string roomCode, int season)
        {
            var hotel = GetHotelId(hotelCode, season);
            return _context.MainRooms.Where(p => p.MainRoomCode == roomCode && p.HotelModelId == hotel).FirstOrDefault().Id;
        }

        //Szukam głównego ppkoju w bazie hoteli
        public MainRoomModel GetMainRoom(string mainRoomCode, string hotelCode, int season)
        {
            var hotel = GetHotelId(hotelCode, season);
            return _context.MainRooms.Where(p => p.MainRoomCode == mainRoomCode && p.HotelModelId == hotel).FirstOrDefault();
        }

        //Szukam pokoju podrzędnego w bazie hoteli
        public SharedUnitModel GetSharedUnit(string sharedUnitCode, string hotelCode, string roomCode, int season)
        {
            var room = GetMainRoomlId(hotelCode, roomCode, season);
            return _context.SharedUnits.Where(p => p.SharedRoomCode == sharedUnitCode && p.MainRoomModelId == room).FirstOrDefault();
        }

        //Pobieram listę wszystkich pytań wraz z podpytaniami
        public List<Question> GetQuestions()
        {
            var questions_items = _context.Questions.Select(p => p.Items).ToList();
            var model = _context.Questions.ToList();

            return model;
        }

        //Pobieram listę wszystkich podpytań
        public List<QuestionItem> GetQuestionItems()
        {
            return _context.QuestionItems.ToList();
        }

        //Szukam podpytanie po id
        public QuestionItem GetQuestionItem(int id)
        {
            var model = _context.QuestionItems.Where(p => p.Id == id).SingleOrDefault();
            model.Question = _context.QuestionItems.Where(p => p.Id == id).Select(a => a.Question).FirstOrDefault();
            return model;
        }

        //Pobieram listę wszystkich głównych odpowiedzi
        public List<ResponseModel> GetResponses()
        {
            return _context.Responses.ToList();
        }

        //Pobieram listę wszystkich items odpowiedzi
        public List<ResponseItemModel> GetResponseItem()
        {
            return _context.ResponseItems.ToList();
        }

        //Zapisuje do bazy nowe hotele wraz z pokojami
        public void UploadNewHotels(HotelModel hotel)
        {
            _context.Update(hotel);
            _context.SaveChanges();
        }

        //Zapisuję do bazy nowe pokoje bo hotel już mam
        public void UploadNewRooms(MainRoomModel room)
        {
            _context.Update(room);
            _context.SaveChanges();
        }

        //Zapisuję do bazy tylko podrzędne pokoje bo hotel i główny pokój już mam
        public void UploadNewShareds(SharedUnitModel shared)
        {
            _context.Update(shared);
            _context.SaveChanges();
        }

        //Zapisuję do bazy nowego partnera
        public void AddNewPartner(PartnerModel partner)
        {
            _context.Update(partner);
            _context.SaveChanges();
        }

        //Zapisuję do bazy nowy sezon
        public void UploadNewSeason(SeasonModel season)
        {
            _context.Update(season);
            _context.SaveChanges();
        }

        //Zapisuję do bazy powiązanie między użytkowiniem a partnerem
        public void UploadMatchUserPartner(UserPartnerModel userPartner)
        {
            _context.Update(userPartner);
            _context.SaveChanges();
        }

        //Usuwam z bazy powiązanie między użytkowiniem a partnerem
        public void RemoveMatchUserPartner(int id)
        {
            var partner = _context.UsersPartners.Where(p => p.Id == id).FirstOrDefault();
            _context.UsersPartners.Remove(partner);
            _context.SaveChanges();
        }

        //Zapisuję do bazy powiązanie między użytkowiniem a partnerem
        public void UploadQuestions(Question questions)
        {
            _context.Update(questions);
            _context.SaveChanges();
        }

        //Zapisuję do bazy powiązanie między użytkowiniem a partnerem
        public void RemoveQuestion(Question questions)
        {
            _context.Remove(questions);
            _context.SaveChanges();
        }

        //Zapisuję do bazy powiązanie między użytkowiniem a partnerem
        public void UploadQuestionItems(QuestionItem questionsItems)
        {
            _context.Update(questionsItems);
            _context.SaveChanges();
        }

        //Zapisuję do bazy powiązanie między użytkowiniem a partnerem
        public void RemoveQuestionItems(QuestionItem questionsItems)
        {
            _context.Remove(questionsItems);
            _context.SaveChanges();
        }

        //Zapisuję do bazy powiązanie między użytkowiniem a partnerem
        public void UploadResponses(ResponseModel response)
        {
            _context.Update(response);
            _context.SaveChanges();
        }

        //Zapisuję do bazy powiązanie między użytkowiniem a partnerem
        public void UploadResponseItems(ResponseItemModel responseItem)
        {
            _context.Update(responseItem);
            _context.SaveChanges();
        }
    }
}
