using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrecosQuestionnaire.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Internal;

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
            ViewData["page"] = page;

            return View(items);
        }

        [HttpPost]
        public IActionResult Index(IFormCollection formCollection)
        {
            int page = int.Parse(formCollection["page"]);
            int hotel = int.Parse(formCollection["hotel"].ToString().Substring(10,4));
            
            TempData["hotel"] = hotel;

            var response = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).FirstOrDefault();
            var kolekcja = new Dictionary<string, string>();

            foreach (var formData in formCollection)
            {
                if (formData.Key == "hotel" || formData.Key == "page")
                    continue;
                else
                {
                    TempData[formData.Key] = formData.Value;
                    kolekcja.Add(formData.Key, formData.Value);
                }
            }


                if (page == 1)
            {
                var updateHotel = _hotelRepository.GetHotelId(hotel);

                HotelModel hotelModel = updateHotel;
                hotelModel.Response = true;
                _hotelRepository.UploadHotels(hotelModel);

                page = 2;
            }

            else
            {
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

                    return View("Submit");
                }
                else
                {
                    return View("Index");
                }
            }
            return RedirectToAction("Index", new {page});
        }

        public ActionResult Back(int page)
        {
            ViewBag.Dolphin = false;
            if (page == 1)
            {
                page = 1;
            }
            else if (page == 2)
            {
                page = 1;
            }
            else if (page == 3)
            {
                page = 2;
            }
            else if (page == 4)
            {
                page = 3;
            }
            else if (page == 5)
            {
                page = 4;
            }
            else if (page == 6)
            {
                page = 5;
            }
            return RedirectToAction("Index", new { page });
        }
    }
}