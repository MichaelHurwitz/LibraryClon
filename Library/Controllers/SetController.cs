using Library.Models;
using Library.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Library.ViewModel;
using System.Linq;

namespace Library.Controllers
{/// <summary>
/// /
/// </summary>
    public class SetController : Controller
    {
        private readonly ISetService _setService;
        private readonly IShelfService _shelfService;

        public SetController(ISetService setService, IShelfService shelfService)
        {
            _setService = setService;
            _shelfService = shelfService;
        }

        public async Task<IActionResult> Index(long shelfId)
        {
            var sets = await _setService.GetAllSets();
            var filteredSets = sets.Where(s => s.ShelfId == shelfId).ToList();
            ViewBag.ShelfId = shelfId;
            return View(filteredSets);
        }

        public IActionResult AddSet(long shelfId)
        {
            ViewBag.ShelfId = shelfId;
            return View(new SetVm { ShelfId = shelfId });
        }

        public async Task<IActionResult> EditSet(long id)
        {
            var set = await _setService.GetSetById(id);
            if (set == null)
            {
                return NotFound();
            }

            var setVm = new SetVm
            {
                Id = set.Id,
                SetName = set.SetName,
                ShelfId = set.ShelfId
            };
            ViewBag.ShelfId = set.ShelfId;
            return View(setVm);
        }

        [HttpPost]
        public async Task<IActionResult> AddSet(SetVm setVm)
        {
            if (ModelState.IsValid)
            {
                await _setService.AddSet(setVm);
                return RedirectToAction("Index", new { shelfId = setVm.ShelfId });
            }
            return View(setVm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSet(SetVm setVm)
        {
            if (ModelState.IsValid)
            {
                await _setService.UpdateSet(setVm);
                return RedirectToAction("Index", new { shelfId = setVm.ShelfId });
            }
            return View(setVm);
        }

        public async Task<IActionResult> AddBook(long setId)
        {
            var set = await _setService.GetSetById(setId);
            if (set == null)
            {
                return NotFound();
            }

            ViewBag.SetId = setId;
            return View(new BookVm { SetId = setId });
        }
    }
}
