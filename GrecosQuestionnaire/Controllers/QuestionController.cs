using System.Linq;
using GrecosQuestionnaire.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GrecosQuestionnaire.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IHotelRepository _hotelRepository;

        public QuestionController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public IActionResult Index(int? page, string f = null)
        {
            var items = _hotelRepository.GetQuestions().OrderBy(x => x.ItemPage).ThenBy(x => x.ItemOrder).AsQueryable();
            //items = ApplyFilters(items, f);

            ViewBag.Count = items.Count();

            var pageNumber = page ?? 1;
            var onePage = items.ToPagedList(pageNumber, 10);

            return View(onePage);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(QuestionModel model)
        {
            if (ModelState.IsValid)
            {
                var partner = new Question()
                {
                    ItemPage = model.Page,
                    ItemOrder = model.Order,
                    Title = model.Title,
                    Subtitle = model.Subtitle,
                    Removed = model.Removed,
                    IsHeader = model.IsHeader,
                    Class = model.Class
                };

                _hotelRepository.UploadQuestions(partner);

                TempData["Message-Success"] = "Poprawnie dodano pytanie";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(long id)
        {
            var item = _hotelRepository.GetQuestions().Where(p => p.Id == id).FirstOrDefault();
            var model = new QuestionModel()
            {
                Id = item.Id,
                Order = item.ItemOrder,
                Title = item.Title,
                Removed = item.Removed,
                Page = item.ItemPage,
                Subtitle = item.Subtitle,
                IsHeader = item.IsHeader,
                Class = item.Class
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(QuestionModel model)
        {
            if (ModelState.IsValid)
            {
                var question = _hotelRepository.GetQuestions().Where(p => p.Id == model.Id).FirstOrDefault();
                question.ItemPage = model.Page;
                question.ItemOrder = model.Order;
                question.Title = model.Title;
                question.Subtitle = model.Subtitle;
                question.Removed = model.Removed;
                question.IsHeader = model.IsHeader;
                question.Class = model.Class;

                _hotelRepository.UploadQuestions(question);

                TempData["Message-Success"] = "Poprawnie dodano pytanie";
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}