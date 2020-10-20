using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrecosQuestionnaire.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Internal;
using X.PagedList;

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

            if (page == 7)
            {
                List<Question> questions = new List<Question>();
                Dictionary<string, string> roomList = new Dictionary<string, string>();
                List<string> list = new List<string>();

                foreach (var key in HttpContext.Session.Keys)
                {
                    roomList.Add(key.ToString(), HttpContext.Session.GetString(key));
                }

                var x = roomList.Where(p => p.Key == "2419").Select(s => new { Value = s.Value.Split(',') });

                foreach (var item in x)
                {
                    for (int i = 0; i < item.Value.Length; i++)
                    {
                        list.Add(item.Value.GetValue(i).ToString().Split("\r\n").GetValue(0).ToString());
                    }
                }

                //Mapowanie nazw pokoi na ID pytania z bazy SQL
                List<int> lista = new List<int>();

                foreach (var item in list)
                {
                    if (item.Contains("standard room"))
                    {
                        lista.Add(1024);
                    }
                    else if (item.Contains("superior"))
                    {
                        lista.Add(1025);
                    }
                    else if (item.Contains("deluxe"))
                    {
                        lista.Add(1026);
                    }
                    else if (item.Contains("dbl bungalow"))
                    {
                        lista.Add(1027);
                    }
                    else if (item.Contains("family"))
                    {
                        lista.Add(1028);
                    }
                    else if (item.Contains("superior family"))
                    {
                        lista.Add(1029);
                    }
                    else if (item.Contains("studio"))
                    {
                        lista.Add(1030);
                    }
                    else if (item.Contains("apartament"))
                    {
                        lista.Add(1031);
                    }
                    else if (item.Contains("suite"))
                    {
                        lista.Add(1032);
                    }
                    else if (item.Contains("junior suite"))
                    {
                        lista.Add(1033);
                    }
                    else if (item.Contains("family suite"))
                    {
                        lista.Add(1034);
                    }
                    else if (item.Contains("executive suite"))
                    {
                        lista.Add(1035);
                    }
                    else if (item.Contains("maisonette"))
                    {
                        lista.Add(1036);
                    }
                    else if (item.Contains("economic"))
                    {
                        lista.Add(1037);
                    }
                }

                //pobieram listę pytań
                var items2 = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).OrderBy(x => x.ItemOrder);
                var items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).Where(s => lista.Contains(s.Id)).OrderBy(x => x.ItemOrder);

                foreach (var key in HttpContext.Session.Keys)
                {
                    ViewData[key.ToString()] = HttpContext.Session.GetString(key);
                }

                ViewBag.Hotel = hotel;
                ViewData["page"] = page;

                return View(items);
            }

            if (page == 9)
            {
                List<Question> questions = new List<Question>();
                Dictionary<string, string> roomList = new Dictionary<string, string>();
                List<string> list = new List<string>();

                foreach (var key in HttpContext.Session.Keys)
                {
                    roomList.Add(key.ToString(), HttpContext.Session.GetString(key));
                }

                var x = roomList.Where(p => p.Key == "3391").Select(s => new { Value = s.Value.Split(',') });

                foreach (var item in x)
                {
                    for (int i = 0; i < item.Value.Length; i++)
                    {
                        list.Add(item.Value.GetValue(i).ToString().Split("\r\n").GetValue(0).ToString());
                    }
                }

                //Mapowanie nazw wyżywnienia na ID pytania z bazy SQL
                List<int> lista = new List<int>();

                foreach (var item in list)
                {
                    if (item.Contains("BB"))
                    {
                        lista.Add(1039);
                    }
                    else if (item.Contains("HB"))
                    {
                        lista.Add(1040);
                    }
                    else if (item.Contains("FB"))
                    {
                        lista.Add(1041);
                    }
                    else if (item.Contains("FB PLUS"))
                    {
                        lista.Add(1042);
                    }
                    else if (item.Contains("LIGHT ALL INCL"))
                    {
                        lista.Add(1043);
                    }
                    else if (item.Contains("ALL INCL"))
                    {
                        lista.Add(1044);
                    }
                    else if (item.Contains("ALL INCL CLASSIC"))
                    {
                        lista.Add(1045);
                    }
                    else if (item.Contains("ALL INCL PLUS"))
                    {
                        lista.Add(1046);
                    }
                    else if (item.Contains("ALL INCL PREMIUM"))
                    {
                        lista.Add(1047);
                    }
                }

                //pobieram listę pytań
                var items2 = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).OrderBy(x => x.ItemOrder);
                var items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).Where(s => lista.Contains(s.Id)).OrderBy(x => x.ItemOrder);

                foreach (var key in HttpContext.Session.Keys)
                {
                    ViewData[key.ToString()] = HttpContext.Session.GetString(key);
                }

                ViewBag.Hotel = hotel;
                ViewData["page"] = page;

                return View(items);
            }

            else
            {
                var items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).OrderBy(x => x.ItemOrder);

                foreach (var key in HttpContext.Session.Keys)
                {
                    ViewData[key.ToString()] = HttpContext.Session.GetString(key);
                }

                ViewBag.Hotel = hotel;
                ViewData["page"] = page;

                return View(items);
            }
        }

        [HttpPost]
        public IActionResult Index(IFormCollection formCollection, string action, bool back = false)
        {
            int page = int.Parse(formCollection["page"]);

            int hotel = formCollection["hotel"].ToString().Length;
            if (hotel == 4)
            {
                hotel = int.Parse(formCollection["hotel"]);
            }
            else
            {
                hotel = int.Parse(formCollection["hotel"].ToString().Substring(10, 4));
            }

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

            if (action == "next")
            {
                if (page == 1)
                {
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
                else if (page == 5)
                {
                    page = 6;
                }
                else if (page == 6)
                {
                    page = 7;
                }
                else if (page == 7)
                {
                    page = 8;
                }
                else if (page == 8)
                {
                    page = 9;
                }
                else if (page == 9)
                {
                    page = 10;
                }
                else if (page == 10)
                {
                    page = 11;
                }
                else
                {
                    return UpdateDatabase(hotel, response);
                }
            }
            else if (action == "save")
            {
                if (page == 1)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 2)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 3)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 4)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 5)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 6)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 7)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 8)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 9)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 10)
                {
                    return UpdateDatabase(hotel, response);
                }
                else
                {
                    return UpdateDatabase(hotel, response);
                }
            }
            else if (action == "back")
            {
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
                else if (page == 7)
                {
                    page = 6;
                }
                else if (page == 8)
                {
                    page = 7;
                }
                else if (page == 9)
                {
                    page = 8;
                }
                else if (page == 10)
                {
                    page = 9;
                }
                else if (page == 11)
                {
                    page = 10;
                }
            }

          
            return RedirectToAction("Index", new { page, hotel });
        }

        //public IActionResult Back(int page, int hotel)
        //{
        //    if (page == 1)
        //    {
        //        page = 1;
        //    }
        //    else if (page == 2)
        //    {
        //        page = 1;
        //    }
        //    else if (page == 3)
        //    {
        //        page = 2;
        //    }
        //    else if (page == 4)
        //    {
        //        page = 3;
        //    }
        //    else if (page == 5)
        //    {
        //        page = 4;
        //    }
        //    else if (page == 6)
        //    {
        //        page = 5;
        //    }
        //    else if (page == 7)
        //    {
        //        page = 6;
        //    }
        //    else if (page == 8)
        //    {
        //        page = 7;
        //    }
        //    else if (page == 9)
        //    {
        //        page = 8;
        //    }
        //    else if (page == 10)
        //    {
        //        page = 9;
        //    }
        //    else if (page == 11)
        //    {
        //        page = 10;
        //    }
        //    return RedirectToAction("Index", new { page, hotel });
        //}

        //public IActionResult BackEdit(int page, int hotel)
        //{
        //    if (page == 1)
        //    {
        //        page = 1;
        //    }
        //    else if (page == 2)
        //    {
        //        page = 1;
        //    }
        //    else if (page == 3)
        //    {
        //        page = 2;
        //    }
        //    else if (page == 4)
        //    {
        //        page = 3;
        //    }
        //    else if (page == 5)
        //    {
        //        page = 4;
        //    }
        //    else if (page == 6)
        //    {
        //        page = 5;
        //    }
        //    else if (page == 7)
        //    {
        //        page = 6;
        //    }
        //    else if (page == 8)
        //    {
        //        page = 7;
        //    }
        //    else if (page == 9)
        //    {
        //        page = 8;
        //    }
        //    else if (page == 10)
        //    {
        //        page = 9;
        //    }
        //    else if (page == 11)
        //    {
        //        page = 10;
        //    }
        //    return RedirectToAction("Edit", new { page, hotel });
        //}

        public IActionResult Edit(int? page, int hotel)
        {
            if (!page.HasValue || page == 0)
            {
                page = 1;

                //pobieram ID odpowiedzi powiązanej z hotelem
                var response = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).SingleOrDefault();

                //pobieram listę odpowiedzi powiązanych z odpowiedzią
                var responseitems = _hotelRepository.GetResponseItem().Where(r => r.Response.Id == response.Id).ToList();

                //pobieram listę pytań
                var items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).OrderBy(x => x.ItemOrder);

                foreach (var item in responseitems)
                {
                    ViewData[item.Value] = item.RawValue;
                }

                ViewBag.Hotel = hotel;
                ViewData["page"] = page;

                return View(items);
            }

            else if (page == 1 || page == 2 || page == 3 || page == 4 || page == 5 || page == 6 || page == 8 || page == 10 || page == 11)
            {
                //pobieram ID odpowiedzi powiązanej z hotelem
                var response = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).SingleOrDefault();

                //pobieram listę odpowiedzi powiązanych z odpowiedzią
                var responseitems = _hotelRepository.GetResponseItem().Where(r => r.Response.Id == response.Id).ToList();

                //pobieram listę pytań
                var items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).OrderBy(x => x.ItemOrder);

                foreach (var item in responseitems)
                {
                    if (item.QuestionItem.Id==26)
                    {
                        var dana = item.RawValue;
                        ViewBag.Number = Int32.Parse(dana);
                    }
                    ViewData[item.Value] = item.RawValue;
                }

                foreach (var key in HttpContext.Session.Keys)
                {
                    ViewData[key.ToString()] = HttpContext.Session.GetString(key);
                }

                ViewBag.Hotel = hotel;
                ViewData["page"] = page;

                return View(items);
            }

            else if (page == 7)
            {
                List<Question> questions = new List<Question>();
                Dictionary<string, string> roomList = new Dictionary<string, string>();
                List<string> list = new List<string>();

                foreach (var key in HttpContext.Session.Keys)
                {
                    roomList.Add(key.ToString(), HttpContext.Session.GetString(key));
                }

                var x = roomList.Where(p => p.Key == "2419").Select(s => new { Value = s.Value.Split(',') });

                foreach (var item in x)
                {
                    for (int i = 0; i < item.Value.Length; i++)
                    {
                        list.Add(item.Value.GetValue(i).ToString().Split("\r\n").GetValue(0).ToString());
                    }
                }

                //Mapowanie nazw pokoi na ID pytania z bazy SQL
                List<int> lista = new List<int>();

                foreach (var item in list)
                {
                    if (item.Contains("standard room"))
                    {
                        lista.Add(1024);
                    }
                    else if (item.Contains("superior"))
                    {
                        lista.Add(1025);
                    }
                    else if (item.Contains("deluxe"))
                    {
                        lista.Add(1026);
                    }
                    else if (item.Contains("dbl bungalow"))
                    {
                        lista.Add(1027);
                    }
                    else if (item.Contains("family"))
                    {
                        lista.Add(1028);
                    }
                    else if (item.Contains("superior family"))
                    {
                        lista.Add(1029);
                    }
                    else if (item.Contains("studio"))
                    {
                        lista.Add(1030);
                    }
                    else if (item.Contains("apartament"))
                    {
                        lista.Add(1031);
                    }
                    else if (item.Contains("suite"))
                    {
                        lista.Add(1032);
                    }
                    else if (item.Contains("junior suite"))
                    {
                        lista.Add(1033);
                    }
                    else if (item.Contains("family suite"))
                    {
                        lista.Add(1034);
                    }
                    else if (item.Contains("executive suite"))
                    {
                        lista.Add(1035);
                    }
                    else if (item.Contains("maisonette"))
                    {
                        lista.Add(1036);
                    }
                    else if (item.Contains("economic"))
                    {
                        lista.Add(1037);
                    }
                }

                //pobieram ID odpowiedzi powiązanej z hotelem
                var response = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).SingleOrDefault();

                //pobieram listę odpowiedzi powiązanych z odpowiedzią
                var responseitems = _hotelRepository.GetResponseItem().Where(r => r.Response.Id == response.Id).ToList();

                //pobieram listę pytań
                var items2 = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).OrderBy(x => x.ItemOrder);
                var items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).Where(s => lista.Contains(s.Id)).OrderBy(x => x.ItemOrder);

                foreach (var item in responseitems)
                {
                    ViewData[item.Value] = item.RawValue;
                }

                foreach (var key in HttpContext.Session.Keys)
                {
                    ViewData[key.ToString()] = HttpContext.Session.GetString(key);
                }

                ViewBag.Hotel = hotel;
                ViewData["page"] = page;

                return View(items);

            }

            else if (page == 9)
            {
                List<Question> questions = new List<Question>();
                Dictionary<string, string> roomList = new Dictionary<string, string>();
                List<string> list = new List<string>();

                foreach (var key in HttpContext.Session.Keys)
                {
                    roomList.Add(key.ToString(), HttpContext.Session.GetString(key));
                }

                var x = roomList.Where(p => p.Key == "3391").Select(s => new { Value = s.Value.Split(',') });

                foreach (var item in x)
                {
                    for (int i = 0; i < item.Value.Length; i++)
                    {
                        list.Add(item.Value.GetValue(i).ToString().Split("\r\n").GetValue(0).ToString());
                    }
                }

                //Mapowanie nazw wyżywienia na ID pytania z bazy SQL
                List<int> lista = new List<int>();

                foreach (var item in list)
                {
                    if (item.Contains("BB"))
                    {
                        lista.Add(1039);
                    }
                    else if (item.Contains("HB"))
                    {
                        lista.Add(1040);
                    }
                    else if (item.Contains("FB"))
                    {
                        lista.Add(1041);
                    }
                    else if (item.Contains("FB PLUS"))
                    {
                        lista.Add(1042);
                    }
                    else if (item.Contains("LIGHT ALL INCL"))
                    {
                        lista.Add(1043);
                    }
                    else if (item.Contains("ALL INCL"))
                    {
                        lista.Add(1044);
                    }
                    else if (item.Contains("ALL INCL CLASSIC"))
                    {
                        lista.Add(1045);
                    }
                    else if (item.Contains("ALL INCL PLUS"))
                    {
                        lista.Add(1046);
                    }
                    else if (item.Contains("ALL INCL PREMIUM"))
                    {
                        lista.Add(1047);
                    }
                }

                //pobieram ID odpowiedzi powiązanej z hotelem
                var response = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).SingleOrDefault();

                //pobieram listę odpowiedzi powiązanych z odpowiedzią
                var responseitems = _hotelRepository.GetResponseItem().Where(r => r.Response.Id == response.Id).ToList();

                //pobieram listę pytań
                var items2 = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).OrderBy(x => x.ItemOrder);
                var items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).Where(s => lista.Contains(s.Id)).OrderBy(x => x.ItemOrder);

                foreach (var item in responseitems)
                {
                    ViewData[item.Value] = item.RawValue;
                }

                foreach (var key in HttpContext.Session.Keys)
                {
                    ViewData[key.ToString()] = HttpContext.Session.GetString(key);
                }

                ViewBag.Hotel = hotel;
                ViewData["page"] = page;

                return View(items);

            }

            return View();
        }

        [HttpPost]
        public IActionResult Edit(IFormCollection formCollection, string action, bool back = false)
        {
            int page = int.Parse(formCollection["page"]);
            int hotel = formCollection["hotel"].ToString().Length;
            if (hotel == 4)
            {
                hotel = int.Parse(formCollection["hotel"]);
            }
            else
            {
                hotel = int.Parse(formCollection["hotel"].ToString().Substring(10, 4));
            }

            //TempData["hotel"] = hotel;
            var response = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).SingleOrDefault();

            foreach (var formData in formCollection)
            {
                if (formData.Key == "hotel" || formData.Key == "page")
                    continue;
                else
                {
                    HttpContext.Session.SetString(formData.Key, formData.Value);
                }
            }

            if (action == "next")
            {
                if (page == 1)
                {
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
                else if (page == 5)
                {
                    page = 6;
                }
                else if (page == 6)
                {
                    page = 7;
                }
                else if (page == 7)
                {
                    page = 8;
                }
                else if (page == 8)
                {
                    page = 9;
                }
                else if (page == 9)
                {
                    page = 10;
                }
                else if (page == 10)
                {
                    page = 11;
                }
                else
                {
                    return UpdateDatabase(hotel, response);
                }
            }
            else if (action == "save")
            {
                if (page == 1)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 2)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 3)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 4)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 5)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 6)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 7)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 8)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 9)
                {
                    return UpdateDatabase(hotel, response);
                }

                else if (page == 10)
                {
                    return UpdateDatabase(hotel, response);
                }
                else
                {
                    return UpdateDatabase(hotel, response);
                }
            }
            else if (action == "back")
            {
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
                else if (page == 7)
                {
                    page = 6;
                }
                else if (page == 8)
                {
                    page = 7;
                }
                else if (page == 9)
                {
                    page = 8;
                }
                else if (page == 10)
                {
                    page = 9;
                }
                else if (page == 11)
                {
                    page = 10;
                }
            }
            
            return RedirectToAction("Edit", new { page, hotel });
        }

        private IActionResult UpdateDatabase(int hotel, ResponseModel response)
        {
            if (response == null)
            {
                var updateHotel = _hotelRepository.GetHotelId(hotel);
                HotelModel hotelModel = updateHotel;
                hotelModel.Response = true;
                _hotelRepository.UploadHotels(hotelModel);

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
            else if (response != null)
            {
                foreach (var key in HttpContext.Session.Keys)
                {
                    string stringkey = key.ToString();
                    string replaced = stringkey.Replace("t", string.Empty);

                    var responseId = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).Select(x => x.Id).SingleOrDefault();
                    int itemId;

                    if (int.TryParse(replaced, out itemId))
                    {
                        var questionItem = _hotelRepository.GetQuestionItem(itemId);
                        var questionItemId = questionItem.Id;

                        ResponseItemModel responseItem = new ResponseItemModel();

                        var responseItemId = _hotelRepository.GetResponseItem().Where(p => p.Response.Id == responseId && p.QuestionItem.Id == questionItemId && p.Value == key.ToString()).Select(id => id.Id).SingleOrDefault();

                        responseItem.Id = responseItemId;
                        responseItem.QuestionItem = questionItem;
                        responseItem.RawValue = HttpContext.Session.GetString(key);
                        responseItem.Value = key.ToString();
                        responseItem.Response = response;

                        _hotelRepository.UploadResponseItems(responseItem);
                    }
                }
                return View("Submit");
            }
            else
                return View("Index");
        }
    }
}