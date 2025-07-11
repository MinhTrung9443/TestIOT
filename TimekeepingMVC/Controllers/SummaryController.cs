using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimekeepingMVC.Services;

namespace TimekeepingMVC.Controllers
{
    public class SummaryController : Controller
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ActivePage = "Summary";
            var data = await _summaryService.GetSummaryAsync();
            return View(data);
        }
    }
}
