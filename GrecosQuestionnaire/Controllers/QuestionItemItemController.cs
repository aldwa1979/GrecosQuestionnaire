using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrecosQuestionnaire.Logic.Hotels;
using GrecosQuestionnaire.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GrecosQuestionnaire.Controllers
{
    public class QuestionItemItemController : Controller
    {
        private readonly IHotelRepository _hotelRepository;

        public QuestionItemItemController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public ActionResult Index(long QuestionItemId, int? page, string f = null)
        {
            var question = _hotelRepository.GetQuestionItems().Where(p => p.Id == QuestionItemId).FirstOrDefault();

            var items = _hotelRepository.GetQuestionItemItems().Where(x => x.QuestionItem.Id == QuestionItemId);

            ViewBag.Count = items.Count();
            ViewBag.QuestionItemId = QuestionItemId;
            ViewBag.Title = question.Title;

            var pageNumber = page ?? 1;
            var onePage = items.OrderBy(p => p.ItemOrder).ToPagedList(pageNumber, 10);

            return View(onePage);
        }

        public ActionResult Create(long QuestionItemId)
        {
            var model = new QuestionItemItemModel
            {
                QuestionItemId = (int)QuestionItemId
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(QuestionItemItemModel model)
        {
            if (ModelState.IsValid)
            {
                var questionItemItem = new QuestionItemItem()
                {
                    Items = model.Items,
                    QuestionItemType = model.QuestionItemType,
                    Title = model.Title,
                    ItemOrder = model.Order,
                    QuestionItem = _hotelRepository.GetQuestionItems().Where(p => p.Id == model.QuestionItemId).FirstOrDefault(),  
                    Parts = model.Parts,
                    SingleSpace = model.SingleSpace,
                    Required = model.Required
                };
                _hotelRepository.UploadQuestionItemItems(questionItemItem);

                TempData["Message-Success"] = "Poprawnie dodano element podpytania";
                return RedirectToAction("Index", new { questionItemId = model.QuestionItemId });
            }

            return View(model);
        }

        public ActionResult Edit(long id)
        {
            var item = _hotelRepository.GetQuestionItemItem((int)id);
            var model = new QuestionItemItemModel
            {
                QuestionItemId = item.QuestionItem.Id,
                Id = item.Id,
                Items = item.Items,
                Order = item.ItemOrder,
                Title = item.Title,
                QuestionItemType = item.QuestionItemType,
                Parts = item.Parts,
                SingleSpace = item.SingleSpace,
                Required = item.Required
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(QuestionItemItemModel model)
        {
            if (ModelState.IsValid)
            {
                var questionItemItem = _hotelRepository.GetQuestionItemItems().Where(p => p.Id == model.Id).FirstOrDefault();

                questionItemItem.Items = model.Items;
                questionItemItem.QuestionItemType = model.QuestionItemType;
                questionItemItem.Title = model.Title;
                questionItemItem.ItemOrder = model.Order;
                questionItemItem.QuestionItem = _hotelRepository.GetQuestionItems().Where(p => p.Id == model.QuestionItemId).FirstOrDefault();
                questionItemItem.Parts = model.Parts;
                questionItemItem.SingleSpace = model.SingleSpace;
                questionItemItem.Required = model.Required;

                _hotelRepository.UploadQuestionItemItems(questionItemItem);

                TempData["Message-Success"] = "Poprawnie zaktualizowano element pytania";
                return RedirectToAction("Index", new { questionItemId = model.QuestionItemId });
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var item = _hotelRepository.GetQuestionItemItem(id);
            long questionItemId = item.QuestionItem.Id;

            _hotelRepository.RemoveQuestionItemItems(item);

            TempData["Message-Success"] = "Poprawnie usunięto element pytania";
            return RedirectToAction("Index", new { questionItemId });
        }
    }
}