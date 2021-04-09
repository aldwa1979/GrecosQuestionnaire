using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrecosQuestionnaire.Logic.Hotels;
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
            var hotelCodeNameBase = _hotelRepository.GetHotelId(hotel);
            var hotelCodeName = hotelCodeNameBase.HotelCode + " " + hotelCodeNameBase.Name;

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

                PassToView(page, hotel, hotelCodeName);

                SubClassMethod(page, items);

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

                PassToView(page, hotel, hotelCodeName);

                SubClassMethod(page, items);

                return View(items);
            }

            else
            {
                var items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).OrderBy(x => x.ItemOrder);

                foreach (var key in HttpContext.Session.Keys)
                {
                    ViewData[key.ToString()] = HttpContext.Session.GetString(key);
                }

                PassToView(page, hotel, hotelCodeName);

                SubClassMethod(page, items);

                return View(items);
            }
        }

        private void SubClassMethod(int? page, IOrderedEnumerable<Question> items)
        {
            //wyszukuję pytania z grupy ComboList aby na automacie w widoku pokazywać wszystkie wybrane part1
            var comboListQuestions = _hotelRepository.GetQuestionItems().Where(z => z.QuestionItemType.ToString() == "ComboList").Select(z => z.Id).ToArray();
            var comboListQuestionsSubClass = _hotelRepository.GetQuestionItems().
                Where(z => z.QuestionItemType.ToString() == "ComboList").
                Select(z => new { z.Id, V = z.Items.Split($"\r\n") }).ToArray();

            //wyszukuję pytania z grupy ComboList aby na automacie w widoku pokazywać wszystkie wybrane part2
            int number = 0;
            int comboQuestionPerPageToView = 0;

            foreach (var item in comboListQuestions)
            {
                foreach (var item2 in items)
                {
                    if (item2.Items != null && item2.ItemPage == page && item2.Items.Where(x => x.Id == item).Any())
                    {
                        if (comboQuestionPerPageToView == 0)
                        {
                            comboQuestionPerPageToView = (Int32.TryParse(HttpContext.Session.GetString(item.ToString()), out number)) ? Int32.Parse(HttpContext.Session.GetString(item.ToString())) : number;
                        }
                    }
                }
            }

            //wyszukuję pytania z grupy ComboList dla SubClass aby na automacie w widoku pokazywać wszystkie wybrane 
            List<string> listOfSubClass = new List<string>();

            foreach (var item in comboListQuestionsSubClass)
            {
                foreach (var item2 in items)
                {
                    if (item2.Items != null && item2.ItemPage == page && item2.Items.Where(x => x.Id == item.Id).Any())
                    {
                        var idSubClass = HttpContext.Session.GetString(item.Id.ToString());

                        foreach (var subClass in item.V)
                        {
                            if (subClass.Split('$')[0] == idSubClass && subClass.Split('$')[1].Contains("showSubClass"))
                            {
                                listOfSubClass.Add(subClass.Split('$')[1]);
                            }
                        }
                    }
                }
            }

            ViewBag.NumberSubClass = listOfSubClass;
            ViewBag.Number = comboQuestionPerPageToView;
        }

        [HttpPost]
        public IActionResult Index(IFormCollection formCollection, string action, bool back = false)
        {
            int page = int.Parse(formCollection["page"]);

            int hotel = formCollection["hotel"].ToString().Length;
            string hotelCodeName = null;

            if (hotel == 4)
            {
                hotel = int.Parse(formCollection["hotel"]);
                var hotelCodeNameBase = _hotelRepository.GetHotelId(hotel);
                hotelCodeName = hotelCodeNameBase.HotelCode + " " + hotelCodeNameBase.Name;

            }
            else
            {
                hotel = int.Parse(formCollection["hotel"].ToString().Substring(10, 4));
            }

            var response = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).FirstOrDefault();

            //int walidacja = 100;
            int number = 0;
            int numberOfCheck = 100;
            var listOfComboQuestionItems = _hotelRepository.GetQuestionItems().Where(x => x.ItemOrder == 1 && x.QuestionItemType == Data.Enum.QuestionItemType.ComboList).Select(p => p.Id).ToArray();
            List<Question> questions = new List<Question>();
            Dictionary<string, string> roomList = new Dictionary<string, string>();
            List<string> list = new List<string>();
            List<int> lista = new List<int>();

            //Walidacja dla pokoi
            if (page == 7)
            {

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
            }

            if (page == 9)
            {

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
            }


            foreach (var formData in formCollection)
            {
                if (formData.Key == "hotel" || formData.Key == "page")
                    continue;
                else
                {
                    HttpContext.Session.SetString(formData.Key, formData.Value);

                    bool success1 = Int32.TryParse(formData.Key, out number);
                    bool success2 = Int32.TryParse(formData.Value, out number);

                    foreach (var item in listOfComboQuestionItems)
                    {
                        if (success1 && success2 && int.Parse(formData.Key) == item)
                        {
                            numberOfCheck = int.Parse(formData.Value);
                        }
                    }
                }
            }

            int walidacja1 = numberOfCheck;
            walidacja1++;

            if (action == "save" || action == "next")
            {
                //walidacja ankiety
                bool errorExist = false;

                if (page == 7 || page == 9)
                {
                    foreach (var source in _hotelRepository.GetQuestionItems().Where(x => x.Required && x.Question.ItemPage == page && !x.Question.Removed && lista.Contains(x.Question.Id)))
                    {
                        if (HttpContext.Session.GetString(source.Id.ToString()) == null || string.IsNullOrEmpty(HttpContext.Session.GetString(source.Id.ToString())))
                        {
                            if (HttpContext.Session.GetString(source.Id.ToString()) + "t" == null || string.IsNullOrEmpty(HttpContext.Session.GetString(source.Id.ToString() + "t".ToString())))
                            {
                                    errorExist = true;
                                    ModelState.AddModelError(source.Id.ToString(), source.Question.Title + " - " + source.Title + " is required");
                            }
                        }
                    }
                }

                else
                {
                    foreach (var source in _hotelRepository.GetQuestionItems().Where(x => x.Required && x.Question.ItemPage == page && !x.Question.Removed))
                    {
                        if (HttpContext.Session.GetString(source.Id.ToString()) == null || string.IsNullOrEmpty(HttpContext.Session.GetString(source.Id.ToString())))
                        {
                            if (HttpContext.Session.GetString(source.Id.ToString()) + "t" == null || string.IsNullOrEmpty(HttpContext.Session.GetString(source.Id.ToString() + "t".ToString())))
                            {
                                if (source.Question.ItemOrder <= walidacja1)
                                {
                                    errorExist = true;
                                    ModelState.AddModelError(source.Id.ToString(), source.Question.Title + " - " + source.Title + " is required");
                                }
                            }
                        }
                    }
                }

                if (errorExist)
                {
                    IOrderedEnumerable<Question> items = null;
  
                    if (page == 7 || page == 9)
                    {
                        items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed && lista.Contains(x.Id)).OrderBy(x => x.ItemOrder);

                        foreach (var key in (HttpContext.Session.Keys))
                        {
                            ViewData[key.ToString()] = HttpContext.Session.GetString(key);
                        }

                        PassToView(page, hotel, hotelCodeName);
                    }

                    else
                    {
                        items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed && x.ItemOrder <= walidacja1).OrderBy(x => x.ItemOrder);

                        foreach (var key in (HttpContext.Session.Keys))
                        {
                            ViewData[key.ToString()] = HttpContext.Session.GetString(key);
                        }

                        PassToView(page, hotel, hotelCodeName);
                        SubClassMethod(page, items);
                    }

                    return View(items);
                }
                //koniec walidacji
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

        public IActionResult Edit(int? page, int hotel)
        {
            var hotelCodeNameBase = _hotelRepository.GetHotelId(hotel);
            var hotelCodeName = hotelCodeNameBase.HotelCode + " " + hotelCodeNameBase.Name;

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

                PassToView(page, hotel, hotelCodeName);

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

                //wyszukuję pytania z grupy ComboList aby na automacie w widoku pokazywać wszystkie wybrane part1
                var comboListQuestions = _hotelRepository.GetQuestionItems().Where(z => z.QuestionItemType.ToString() == "ComboList").Select(z => z.Id).ToArray();

                int comboQuestionPerPageToViewFromSession = 0;
                int comboQuestionPerPageToViewFromDB = 0;
                int number = 0;

                foreach (var item in responseitems)
                {
                    foreach (var item2 in comboListQuestions)
                    {
                        foreach (var item3 in items)
                        {
                            if (item3.Items != null && item3.ItemPage == page && item3.Items.Where(x => x.Id == item.QuestionItem.Id).Any() && item.QuestionItem.Id == item2)
                            {
                                comboQuestionPerPageToViewFromDB = Int32.Parse(item.RawValue);
                            }
                        }
                    }
                    ViewData[item.Value] = item.RawValue;
                }

                foreach (var key in HttpContext.Session.Keys)
                {
                    ViewData[key.ToString()] = HttpContext.Session.GetString(key);
                }

                PassToView(page, hotel, hotelCodeName);

                //wyszukuję pytania z grupy ComboList aby na automacie w widoku pokazywać wszystkie wybrane part2
                foreach (var item in comboListQuestions)
                {
                    foreach (var item2 in items)
                    {
                        if (item2.Items != null && item2.ItemPage == page && item2.Items.Where(x => x.Id == item).Any())
                        {
                            comboQuestionPerPageToViewFromSession = (Int32.TryParse(HttpContext.Session.GetString(item.ToString()), out number)) ? Int32.Parse(HttpContext.Session.GetString(item.ToString())) : number;
                        }
                    }
                }

                if (comboQuestionPerPageToViewFromSession == 0)
                {
                    ViewBag.Number = comboQuestionPerPageToViewFromDB;
                }
                else
                {
                    ViewBag.Number = comboQuestionPerPageToViewFromSession;
                }

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

                PassToView(page, hotel, hotelCodeName);

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

                PassToView(page, hotel, hotelCodeName);

                return View(items);

            }

            return View();
        }

        private void PassToView(int? page, int hotel, string hotelCodeName)
        {
            ViewBag.Hotel = hotel;
            ViewBag.HotelCodeName = hotelCodeName;
            ViewData["page"] = page;
        }

        [HttpPost]
        public IActionResult Edit(IFormCollection formCollection, string action, bool back = false)
        {
            int page = int.Parse(formCollection["page"]);
            int hotel = formCollection["hotel"].ToString().Length;
            string hotelCodeName = null;

            if (hotel == 4)
            {
                hotel = int.Parse(formCollection["hotel"]);
                var hotelCodeNameBase = _hotelRepository.GetHotelId(hotel);
                hotelCodeName = hotelCodeNameBase.HotelCode + " " + hotelCodeNameBase.Name;
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


            //walidacja ankiety
            bool errorExist = false;

            foreach (var source in _hotelRepository.GetQuestionItems().Where(x => x.Required && x.Question.ItemPage == page && !x.Question.Removed))
            {
                if (HttpContext.Session.GetString(source.Id.ToString()) == null || string.IsNullOrEmpty(HttpContext.Session.GetString(source.Id.ToString())))
                {
                    if (HttpContext.Session.GetString(source.Id.ToString()) + "t" == null || string.IsNullOrEmpty(HttpContext.Session.GetString(source.Id.ToString() + "t".ToString())))
                    {
                        errorExist = true;
                        //ModelState.AddModelError(source.Id.ToString(), "*Field is required");
                        ModelState.AddModelError(source.Id.ToString(), source.Question.Title + " - " + source.Title + " is required");
                    }

                }

            }
            if (errorExist)
            {
                var items = _hotelRepository.GetQuestions().Where(x => x.ItemPage == page && !x.Removed).OrderBy(x => x.ItemOrder);

                foreach (var key in (HttpContext.Session.Keys))
                {
                    ViewData[key.ToString()] = HttpContext.Session.GetString(key);
                }

                PassToView(page, hotel, hotelCodeName);

                return View(items);
            }
            //koniec walidacji

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
                    string replacedItem = stringkey.Replace("q", string.Empty);

                    var responseId = _hotelRepository.GetResponses().Where(p => p.HotelId == hotel).SingleOrDefault();

                    int itemId;
                    int itemItemId;

                    if (stringkey.Contains('q') && stringkey.Length < 7 && int.TryParse(replacedItem, out itemItemId))
                    {
                        int replacedItemInt = int.Parse(replacedItem);

                        var questionItemItem = _hotelRepository.GetQuestionItemItem(itemItemId).QuestionItem.Id;
                        //var questionItem = _hotelRepository.GetQuestionItemItems().Where(p => p.Id == replacedItemInt).FirstOrDefault().QuestionItem.Id;
                        var responseItem = _hotelRepository.GetResponseItem().Where(p => p.Response.HotelId == hotel && p.QuestionItem.Id == questionItemItem).FirstOrDefault();

                        //var questionItem = _hotelRepository.GetQuestionItemItems().Where(p => p.Id == replacedItemInt).FirstOrDefault();
                        //var responseItem = _hotelRepository.GetResponseItem().Where(p => p.Response.HotelId == hotel && p.QuestionItem.Id == questionItem.QuestionItem.Id).SingleOrDefault();

                        ResponseItemItemModel responseItemItem = new ResponseItemItemModel();
                        responseItemItem.QuestionItemItem = questionItemItem;
                        responseItemItem.RawValue = HttpContext.Session.GetString(key);
                        responseItemItem.Value = key.ToString();
                        responseItemItem.ResponseItem = responseItem;

                        _hotelRepository.UploadResponseItemItems(responseItemItem);
                    }

                    else if (int.TryParse(replaced, out itemId))
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