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
    public class QuestionItemController : Controller
    {
        private readonly IHotelRepository _hotelRepository;

        public QuestionItemController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public ActionResult Index(long questionId, int? page, string f = null)
        {
            var question = _hotelRepository.GetQuestions().Where(p => p.Id == questionId).FirstOrDefault();

            var items = _hotelRepository.GetQuestionItems().Where(x => x.Question.Id == questionId).ToList();

            ViewBag.Count = items.Count();
            ViewBag.QuestionId = questionId;
            ViewBag.Title = question.Title;

            var pageNumber = page ?? 1;
            var onePage = items.OrderBy(p=>p.ItemOrder).ToPagedList(pageNumber, 100);

            return View(onePage);
        }

        public ActionResult Create(long questionId)
        {
            var model = new QuestionItemModel
            {
                QuestionId = (int)questionId
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(QuestionItemModel model)
        {
            if (ModelState.IsValid)
            {
                var questionItem = new QuestionItem()
                {
                    Items = model.Items,
                    QuestionItemType = model.QuestionItemType,
                    Title = model.Title,
                    ItemOrder = model.Order,
                    Question = _hotelRepository.GetQuestions().Where(p => p.Id == model.QuestionId).FirstOrDefault(),    //(model.QuestionId),
                    Parts = model.Parts,
                    SingleSpace = model.SingleSpace,
                    Required = model.Required
                };
                _hotelRepository.UploadQuestionItems(questionItem);

                TempData["Message-Success"] = "Poprawnie dodano element pytania";
                return RedirectToAction("Index", new { questionId = model.QuestionId });
            }

            return View(model);
        }

        public ActionResult Edit(long id)
        {
            var item = _hotelRepository.GetQuestionItem((int)id);
            var model = new QuestionItemModel
            {
                QuestionId = item.Question.Id,
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
        public ActionResult Edit(QuestionItemModel model)
        {
            if (ModelState.IsValid)
            {
                var questionItem = _hotelRepository.GetQuestionItems().Where(p => p.Id == model.Id).FirstOrDefault();

                questionItem.Items = model.Items;
                questionItem.QuestionItemType = model.QuestionItemType;
                questionItem.Title = model.Title;
                questionItem.ItemOrder = model.Order;
                questionItem.Question = _hotelRepository.GetQuestions().Where(p => p.Id == model.QuestionId).FirstOrDefault();
                questionItem.Parts = model.Parts;
                questionItem.SingleSpace = model.SingleSpace;
                questionItem.Required = model.Required;

                _hotelRepository.UploadQuestionItems(questionItem);

                TempData["Message-Success"] = "Poprawnie zaktualizowano element pytania";
                return RedirectToAction("Index", new { questionId = model.QuestionId });
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var item = _hotelRepository.GetQuestionItem(id);
            long questionId = item.Question.Id;

            _hotelRepository.RemoveQuestionItems(item);

            TempData["Message-Success"] = "Poprawnie usunięto element pytania";
            return RedirectToAction("Index", new { questionId });
        }
    }
}