using Library.Models;
using Library.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Library.ViewModel;
using System.Linq;

namespace Library.Controllers
{
    public class ShelfController : Controller
    {
        private readonly IShelfService _shelfService;
        private readonly ILibraryService _libraryService;

        public ShelfController(IShelfService shelfService, ILibraryService libraryService)
        {
            _shelfService = shelfService;
            _libraryService = libraryService;
        }

        public async Task<IActionResult> Index(long libraryId)
        {
            var shelves = await _shelfService.GetAllShelves();
            var filteredShelves = shelves.Where(s => s.LibraryId == libraryId).ToList();
            ViewBag.LibraryId = libraryId;
            return View(filteredShelves);
        }

        public IActionResult AddShelf(long libraryId)
        {
            ViewBag.LibraryId = libraryId;
            return View(new ShelfVm { LibraryId = libraryId });
        }

        public async Task<IActionResult> EditShelf(long id)
        {
            var shelf = await _shelfService.GetShelfById(id);
            if (shelf == null)
            {
                return NotFound();
            }

            var shelfVm = new ShelfVm
            {
                Id = shelf.Id,
                Hight = shelf.Hight,
                Width = shelf.Width,
                LibraryId = shelf.LibraryId
            };
            ViewBag.LibraryId = shelf.LibraryId;
            return View(shelfVm);
        }

        [HttpPost]
        public async Task<IActionResult> AddShelf(ShelfVm shelfVm)
        {
            if (ModelState.IsValid)
            {
                await _shelfService.AddShelf(shelfVm);
                return RedirectToAction(nameof(Index), new { libraryId = shelfVm.LibraryId });
            }
            return View(shelfVm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShelf(ShelfVm shelfVm)
        {
            if (ModelState.IsValid)
            {
                await _shelfService.UpdateShelf(shelfVm);
                return RedirectToAction(nameof(Index), new { libraryId = shelfVm.LibraryId });
            }
            return View(shelfVm);
        }

        public async Task<IActionResult> AddSet(long shelfId)
        {
            var shelf = await _shelfService.GetShelfById(shelfId);
            if (shelf == null)
            {
                return NotFound();
            }

            ViewBag.ShelfId = shelfId;
            return View(new SetVm { ShelfId = shelfId });
        }
    }
}
