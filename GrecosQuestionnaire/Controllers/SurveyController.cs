using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrecosQuestionnaire.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrecosQuestionnaire.Controllers
{
    public class SurveyController : Controller
    {
        private readonly IHotelRepository _hotelRepository;

        public SurveyController(IHotelRepository _hotelRepository)
        {
            this._hotelRepository = _hotelRepository;
        }

        public IActionResult Index(int? page, int hotelId)
        {
            if (!page.HasValue)
            {
                page = 1;
            }
            if (page == 0)
            {
                page = 1;
            }

            var items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).OrderBy(x => x.ItemOrder);

            ViewBag.Hotel = hotelId;
            ViewBag.Dolphin = false;
            ViewData["page"] = page;

            return View(items);
        }

        [HttpPost]
        public IActionResult Index(IFormCollection formCollection, int hotel)
        {
            var response = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).FirstOrDefault();

            if (response == null)
            {
                ResponseModel responseModel = new ResponseModel();
                responseModel.ResponseDate = DateTime.Now;
                responseModel.HotelId = hotel;
                responseModel.UserName = User.Identity.Name;

                _hotelRepository.UploadResponses(responseModel);
            }
            else
            {
                return View();
            }



            return RedirectToAction("Index");
        }
    }
}