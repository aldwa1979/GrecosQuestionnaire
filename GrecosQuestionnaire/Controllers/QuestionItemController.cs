using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            var items = _hotelRepository.GetQuestionItems().Where(x => x.Question.Id == questionId);
            //items = ApplyFilters(items, f);

            ViewBag.Count = items.Count();
            ViewBag.QuestionId = questionId;
            ViewBag.Title = question.Title;

            var pageNumber = page ?? 1;
            var onePage = items.ToPagedList(pageNumber, 10);

            return View(onePage);
        }

        public ActionResult Create(long questionId)
        {
            var model = new QuestionItemModel
            {
                Id = (int)questionId
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(QuestionItemModel model)
        {
            if (ModelState.IsValid)
            {
                var questionItem = new QuestionItemModel()
                {
                    Items = model.Items,
                    QuestionItemType = model.QuestionItemType,
                    Title = model.Title,
                    ItemOrder = model.ItemOrder,
                    Question = Entity.Get<Question>(model.QuestionId),
                    Parts = model.Parts,
                    SingleSpace = model.SingleSpace,
                    Required = model.Required
                };
                Entity.Save(questionItem);

                Commit();

                TempData["Message-Success"] = "Poprawnie dodano element pytania";
                return RedirectToAction("Index", new { questionId = model.QuestionId });
            }

            return View(model);
        }

        public ActionResult Edit(long id)
        {
            var item = Entity.Get<QuestionItem>(id);
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
                var questionItem = Entity.Get<QuestionItem>(model.Id);

                questionItem.Items = model.Items;
                questionItem.QuestionItemType = model.QuestionItemType;
                questionItem.Title = model.Title;
                questionItem.ItemOrder = model.Order;
                questionItem.Question = Entity.Get<Question>(model.QuestionId);
                questionItem.Parts = model.Parts;
                questionItem.SingleSpace = model.SingleSpace;
                questionItem.Required = model.Required;

                Entity.Update(questionItem);

                Commit();

                TempData["Message-Success"] = "Poprawnie zaktualizowano element pytania";
                return RedirectToAction("Index", new { questionId = model.QuestionId });
            }

            return View(model);
        }

        public ActionResult Delete(long id)
        {
            var item = Entity.Get<QuestionItem>(id);
            long questionId = item.Question.Id;
            Entity.Delete(item);
            Commit();

            TempData["Message-Success"] = "Poprawnie usunięto element pytania";
            return RedirectToAction("Index", new { questionId });
        }
    }
}