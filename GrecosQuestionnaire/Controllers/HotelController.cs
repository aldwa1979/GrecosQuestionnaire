using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrecosQuestionnaire.Models;
using GrecosQuestionnaire.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GrecosQuestionnaire.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelImportRepository _hotelImportRepository;
        
        public HotelController(IHotelRepository hotelRepository, IHotelImportRepository hotelImportRepository)
        {
            _hotelRepository = hotelRepository;
            _hotelImportRepository = hotelImportRepository;
        }

        public IActionResult Index()
        {
            var model = _hotelRepository.GetAllHotels();

            AddViewBag();
            return View(model);
        }

        [HttpPost]
        public IActionResult Import()
        {
            //pobieram z magica bazę hoteli i pokoju dla każdego sezonu
            var hotelsInMagic = _hotelImportRepository.GetAllHotels();

            foreach (var hotel in hotelsInMagic)
            {

                //ustalam jaki sezon jest aktualnie w pętli
                var season = Int32.Parse(hotel.DestinationSeasonName.Substring(hotel.DestinationSeasonName.Length - 4, 4));

                //jeśli nie ma pokoju lub jest pokój ale nie ma sezonu to ma dodać rekord
                if (
                    (!(Equals(_hotelRepository.GetHotel(hotel.HotelCode, season)?.HotelCode, hotel.HotelCode))) ||
                    ((Equals(_hotelRepository.GetHotel(hotel.HotelCode, season)?.HotelCode, hotel.HotelCode)) && !(Equals(_hotelRepository.GetHotel(hotel.HotelCode, season)?.Season, season)))
                    )
                {
                    var hotelModel = new HotelModel
                    {
                        HotelCode = hotel.HotelCode,
                        Name = hotel.Name,
                        Country = hotel.Country,
                        Destination = hotel.Destination,
                        Season = season
                    };

                    List<SharedUnitModel> sharedUnit = new List<SharedUnitModel>
                    {
                        new SharedUnitModel(hotel.RoomCode, hotel.RoomDesc)
                    };

                    hotelModel.MainRoom.Add(
                        new MainRoomModel(hotel.RoomAllocCode, sharedUnit));

                    try
                    {
                        _hotelRepository.UploadNewHotels(hotelModel);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Błąd podczas zapisu elementu. " + e);
                    }
                }


                //jeśli jest pokój i jest sezon ale nie ma głównego pokoju to ma dodać tylko główny i podrzędny pokój z rekordu
                else if ( (Equals(_hotelRepository.GetHotel(hotel.HotelCode, season)?.HotelCode, hotel.HotelCode)) &&
                           Equals(_hotelRepository.GetHotel(hotel.HotelCode, season)?.Season, season) &&
                         !(Equals(_hotelRepository.GetMainRoom(hotel.RoomAllocCode, hotel.HotelCode, season)?.MainRoomCode, hotel.RoomAllocCode)))
                {
                    List<SharedUnitModel> sharedUnit = new List<SharedUnitModel>
                    {
                        new SharedUnitModel(hotel.RoomCode, hotel.RoomDesc)
                    };

                    var mainRoom = new MainRoomModel
                    {
                        HotelModelId = _hotelRepository.GetHotel(hotel.HotelCode, season).Id,
                        MainRoomCode = hotel.RoomAllocCode,
                        SharedUnit = sharedUnit
                    };

                    try
                    {
                        _hotelRepository.UploadNewRooms(mainRoom);
                    }

                    catch (Exception e)
                    {
                        throw new Exception("Błąd podczas zapisu elementu. " + e);
                    }
                }


                //jeśli jest hotel i sezon i główny pokój ale nie ma podrzędnego pokoju to ma tylko dodać z rekordu ten podrzędny pokój
                else if (  (Equals(_hotelRepository.GetHotel(hotel.HotelCode, season)?.HotelCode, hotel.HotelCode)) &&
                            Equals(_hotelRepository.GetHotel(hotel.HotelCode, season)?.Season, season) &&
                           (Equals(_hotelRepository.GetMainRoom(hotel.RoomAllocCode, hotel.HotelCode, season)?.MainRoomCode, hotel.RoomAllocCode)) &&
                          !(Equals(_hotelRepository.GetSharedUnit(hotel.RoomCode, hotel.HotelCode, hotel.RoomAllocCode, season)?.SharedRoomCode, hotel.RoomCode)) )
                {
                    var sharedUnit = new SharedUnitModel
                    {
                        MainRoomModelId = _hotelRepository.GetMainRoom(hotel.RoomAllocCode, hotel.HotelCode, season).Id,
                        SharedRoomName = hotel.RoomDesc,
                        SharedRoomCode = hotel.RoomCode,
                    };

                    try
                    {
                        _hotelRepository.UploadNewShareds(sharedUnit);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Błąd podczas zapisu elementu. " + e);
                    }
                }

                else   
                {
                    continue;
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Assign (int id)
        {
            var model = _hotelRepository.GetHotelId(id);

            AddViewBag();

            return View(model);
        }

        [HttpPost]
        public IActionResult Assign(HotelModel hotelModel)
        {
            if(ModelState.IsValid)
            {
                _hotelRepository.UploadNewHotels(hotelModel);
            }
            
            return RedirectToAction("Index");
        }


        private void AddViewBag()
        {
            var partners = _hotelRepository.GetPartners().Select(x => new SelectListItem()
            {
                Text = x.Name.ToString(),
                Value = x.Id.ToString()
            }).ToList();

            //partners.Insert(0,
            //    new SelectListItem() { Selected = true, Text = string.Empty, Value = (-1).ToString(CultureInfo.InvariantCulture) });

            ViewBag.Partners = partners;
            //ViewBag.Owners = partners;

            //var users = Entity.Query<User>().Where(x => x.Partner.IsSpecial).Select(x => new SelectListItem()
            //{
            //    Selected = false,
            //    Text = x.Name,
            //    Value = x.Id.ToString(CultureInfo.InvariantCulture)
            //}).ToList();

            //ViewBag.Users = users;
        }


    }
}