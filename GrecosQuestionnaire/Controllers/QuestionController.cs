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
    }
}