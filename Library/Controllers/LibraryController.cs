using Library.Models;
using Library.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Library.ViewModel;

namespace Library.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        private readonly IShelfService _shelfService;

        public LibraryController(ILibraryService libraryService, IShelfService shelfService)
        {
            _libraryService = libraryService;
            _shelfService = shelfService;
        }

        public async Task<IActionResult> Index() =>
            View(await _libraryService.GetAllLibraries());

        public IActionResult AddLibrary()
        {
            return View();
        }

        public async Task<IActionResult> EditLibrary(long id)
        {
            var library = await _libraryService.GetLibraryById(id);
            if (library == null)
            {
                return NotFound();
            }

            var libraryVm = new LibraryVm
            {
                Id = library.Id,
                Ganre = library.Ganre
            };
            return View(libraryVm);
        }

        [HttpPost]
        public async Task<IActionResult> AddLibrary(LibraryVm libraryVm)
        {
            if (ModelState.IsValid)
            {
                await _libraryService.AddLibrary(libraryVm);
                return RedirectToAction(nameof(Index));
            }
            return View(libraryVm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLibrary(LibraryVm libraryVm)
        {
            if (ModelState.IsValid)
            {
                await _libraryService.UpdateLibrary(libraryVm);
                return RedirectToAction(nameof(Index));
            }
            return View(libraryVm);
        }

        public async Task<IActionResult> AddShelf(long libraryId)
        {
            var library = await _libraryService.GetLibraryById(libraryId);
            if (library == null)
            {
                return NotFound();
            }

            ViewBag.LibraryId = libraryId;
            return View(new ShelfVm { LibraryId = libraryId });
        }
    }
}
