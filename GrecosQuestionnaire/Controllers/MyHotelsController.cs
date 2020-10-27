using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GrecosQuestionnaire.Logic.Hotels;
using GrecosQuestionnaire.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrecosQuestionnaire.Controllers
{
    public class MyHotelsController : Controller
    {
        private readonly IHotelRepository _hotelRepository;

        public MyHotelsController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            var userIds = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var partners = _hotelRepository.GetUsersPartners().Where(p => p.UserID == userIds).Select(x=>x.PartnerModelID).ToList();
            var hotels = _hotelRepository.GetAllHotels();

            List<HotelModel> myHotels = new List<HotelModel>();

            foreach (var hotel in hotels)
            {
                if (partners.Contains(hotel.HotelPartnerId.GetValueOrDefault()))
                {
                    myHotels.Add(hotel);
                }
            }
            
            return View(myHotels);
        }
    }
}