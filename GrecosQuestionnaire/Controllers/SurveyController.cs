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
            var updateHotel = _hotelRepository.GetHotelId(hotel);

            HotelModel hotelModel = updateHotel;
            hotelModel.Response = true;
            _hotelRepository.UploadHotels(hotelModel);
            

            _hotelRepository.UploadHotels(updateHotel);

            if (response == null)
            {
                ResponseModel responseModel = new ResponseModel();
                responseModel.ResponseDate = DateTime.Now;
                responseModel.HotelId = hotel;
                responseModel.UserName = User.Identity.Name;

                _hotelRepository.UploadResponses(responseModel);


                foreach (var formData in formCollection)
                {
                    string stringkey = formData.Key.ToString();
                    string replaced = stringkey.Replace("t", string.Empty);

                    var responseId = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).SingleOrDefault();
                    int itemId;

                    if (int.TryParse(replaced, out itemId))
                    {
                        var questionItem = _hotelRepository.GetQuestionItem(itemId);
                        ResponseItemModel responseItem = new ResponseItemModel();
                        responseItem.QuestionItem = questionItem;
                        responseItem.RawValue = formData.Value;
                        responseItem.Value = formData.Key;
                        responseItem.Response = responseId;

                        _hotelRepository.UploadResponseItems(responseItem);
                    }
                }
            }
            else
            {
                return View();
            }



            return RedirectToAction("Index");
        }
    }
}