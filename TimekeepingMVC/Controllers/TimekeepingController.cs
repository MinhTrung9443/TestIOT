using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TimekeepingMVC.Models;

namespace TimekeepingMVC.Controllers
{
    public class TimekeepingController : Controller
    {
        private static List<PhieuChamCong> _phieuChamCongList = new List<PhieuChamCong>
        {
            new PhieuChamCong { Id = 1, STT = 1, Ngay = DateTime.Now, Nguoi = "Nguyễn Văn A", TDVao = "08:00", TDRa = "17:00", TongGioLam = 9.0 },
            new PhieuChamCong { Id = 2, STT = 2, Ngay = DateTime.Now.AddDays(-1), Nguoi = "Trần Thị B", TDVao = "08:30", TDRa = "16:30", TongGioLam = 8.0 }
        };
        private int _nextId = 3;

        public IActionResult Index()
        {
            ViewBag.ActivePage = "Timekeeping";
            return View(_phieuChamCongList);
        }

        public IActionResult Create()
        {
            ViewBag.ActivePage = "Timekeeping";
            return View(new PhieuChamCong { Ngay = DateTime.Now, TDVao = "08:00", TDRa = "17:00", TongGioLam = 9.0 });
        }

        [HttpPost]
        public IActionResult Create(PhieuChamCong model)
        {
            if (ModelState.IsValid)
            {
                model.Id = _nextId++;
                model.STT = _phieuChamCongList.Count + 1;
                var timeIn = TimeSpan.Parse(model.TDVao);
                var timeOut = TimeSpan.Parse(model.TDRa);
                model.TongGioLam = (timeOut - timeIn).TotalHours;
                _phieuChamCongList.Add(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}