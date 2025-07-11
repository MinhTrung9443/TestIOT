using Microsoft.AspNetCore.Mvc;
using TimekeepingMVC.Services;
using TimekeepingMVC.Models;

namespace TimekeepingMVC.Controllers
{
    public class TimekeepingController : Controller
    {
        private readonly ITimekeepingService _timekeepingService;

        public TimekeepingController(ITimekeepingService timekeepingService)
        {
            _timekeepingService = timekeepingService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ActivePage = "Timekeeping";
            var records = await _timekeepingService.GetAllAsync();
            return View(records);
        }

        public IActionResult Create()
        {
            ViewBag.ActivePage = "Timekeeping";
            return View(new PhieuChamCong { Ngay = DateTime.Now, TDVao = "08:00", TDRa = "17:00" });
        }

        [HttpPost]
        public async Task<IActionResult> Create(PhieuChamCong model)
        {
            if (ModelState.IsValid)
            {
                await _timekeepingService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.ActivePage = "Timekeeping";
            var record = await _timekeepingService.GetByIdAsync(id);
            if (record == null) return NotFound();
            return View(record);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, PhieuChamCong model)
        {
            if (ModelState.IsValid)
            {
                await _timekeepingService.UpdateAsync(id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _timekeepingService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}