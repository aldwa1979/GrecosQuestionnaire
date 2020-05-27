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

        public IActionResult Index(int? page, int hotel)
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

            foreach (var key in HttpContext.Session.Keys)
            {
                ViewData[key.ToString()] = HttpContext.Session.GetString(key);
            }

            ViewBag.Hotel = hotel;
            ViewData["page"] = page;

            return View(items);
        }

        [HttpPost]
        public IActionResult Index(IFormCollection formCollection, bool back = false)
        {
            int page = int.Parse(formCollection["page"]);
            int hotel = int.Parse(formCollection["hotel"].ToString().Substring(10, 4));

            TempData["hotel"] = hotel;

            var response = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).FirstOrDefault();

            foreach (var formData in formCollection)
            {
                if (formData.Key == "hotel" || formData.Key == "page")
                    continue;
                else
                {
                    HttpContext.Session.SetString(formData.Key, formData.Value);
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

            else if (page == 2)
            {
                page = 3;
            }

            else if (page == 3)
            {
                page = 4;
            }

            else if (page == 4)
            {
                page = 5;
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

                    foreach (var key in HttpContext.Session.Keys)
                    {
                        string stringkey = key.ToString();
                        string replaced = stringkey.Replace("t", string.Empty);

                        var responseId = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).SingleOrDefault();
                        int itemId;

                        if (int.TryParse(replaced, out itemId))
                        {
                            var questionItem = _hotelRepository.GetQuestionItem(itemId);
                            ResponseItemModel responseItem = new ResponseItemModel();
                            responseItem.QuestionItem = questionItem;
                            responseItem.RawValue = HttpContext.Session.GetString(key);
                            responseItem.Value = key.ToString();
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
            return RedirectToAction("Index", new { page });
        }

        public IActionResult Back(int page, int hotel)
        {
            TempData["hotel"] = hotel;

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

        public IActionResult BackEdit(int page, int hotel)
        {
            TempData["hotel"] = hotel;

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
            return RedirectToAction("Edit", new { page });
        }

        public IActionResult Edit(int? page, int hotel)
        {
            if (!page.HasValue)
            {
                page = 1;
            }
            if (page == 0)
            {
                page = 1;
            }


            var response = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).SingleOrDefault();
            var responseitems = _hotelRepository.GetResponseItem().Where(r => r.Response.Id == response.Id).ToList();

            var items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).OrderBy(x => x.ItemOrder);

            foreach (var item in responseitems)
            {
                ViewData[item.Value] = item.RawValue;
            }

            ViewBag.Hotel = hotel;
            ViewData["page"] = page;

            return View(items);
        }
    }
}