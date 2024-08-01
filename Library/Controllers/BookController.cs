using Library.Models;
using Library.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Library.ViewModel;
using System.Linq;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ISetService _setService;
        private readonly IShelfService _shelfService;

        public BookController(IBookService bookService, ISetService setService, IShelfService shelfService)
        {
            _bookService = bookService;
            _setService = setService;
            _shelfService = shelfService;
        }

        public async Task<IActionResult> Index(long setId)
        {
            var books = await _bookService.GetAllBooks();
            var filteredBooks = books.Where(b => b.SetId == setId).ToList();
            ViewBag.SetId = setId;
            return View(filteredBooks);
        }

        public IActionResult AddBook(long setId)
        {
            ViewBag.SetId = setId;
            return View(new BookVm { SetId = setId });
        }

        public async Task<IActionResult> EditBook(long id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookVm = new BookVm
            {
                Id = book.Id,
                Ganre = book.Ganre,
                Title = book.Title,
                Hight = book.Hight,
                Width = book.Width,
                SetId = book.SetId
            };
            ViewBag.SetId = book.SetId;
            return View(bookVm);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookVm bookVm)
        {
            if (ModelState.IsValid)
            {
                var book = new BookModel
                {
                    Title = bookVm.Title,
                    Ganre = bookVm.Ganre,
                    Hight = bookVm.Hight,
                    Width = bookVm.Width,
                    SetId = bookVm.SetId
                };

                var result = await _shelfService.AddBookToShelf(bookVm.SetId, book); 
                if (result != "Book added successfully.")
                {
                    ModelState.AddModelError(string.Empty, result);
                    return View(bookVm);
                }

                return RedirectToAction(nameof(Index), new { setId = bookVm.SetId });
            }
            return View(bookVm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(BookVm bookVm)
        {
            if (ModelState.IsValid)
            {
                var book = new BookModel
                {
                    Id = bookVm.Id,
                    Title = bookVm.Title,
                    Ganre = bookVm.Ganre,
                    Hight = bookVm.Hight,
                    Width = bookVm.Width,
                    SetId = bookVm.SetId
                };

                var result = await _shelfService.AddBookToShelf(bookVm.SetId, book); 
                if (result != "Book added successfully.")
                {
                    ModelState.AddModelError(string.Empty, result);
                    return View(bookVm);
                }

                await _bookService.UpdateBook(bookVm);
                return RedirectToAction(nameof(Index), new { setId = bookVm.SetId });
            }
            return View(bookVm);
        }
    }
}
