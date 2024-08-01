using Library.Models;
using Library.ViewModel;
using Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Service
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookModel> AddBook(BookVm bookVm)
        {
            BookModel model = new()
            {
                Title = bookVm.Title,
                Ganre = bookVm.Ganre,
                Hight = bookVm.Hight,
                Width = bookVm.Width,
                SetId = bookVm.SetId
            };
            await _context.Books
                .AddAsync(model);
            await _context
                .SaveChangesAsync();
            return model;
        }

        public async Task<BookModel?> UpdateBook(BookVm bookVm)
        {
            var book = await _context.Books
                .FindAsync(bookVm.Id);
            if (book != null)
            {
                book.Title = bookVm.Title;
                book.Ganre = bookVm.Ganre;
                book.Hight = bookVm.Hight;
                book.Width = bookVm.Width;
                book.SetId = bookVm.SetId;
                _context.Books
                    .Update(book);
                await _context
                    .SaveChangesAsync();
            }
            return book;
        }

        public async Task<IEnumerable<BookModel>> GetAllBooks() =>
            await _context.Books
            .Include(b => b.Set)
            .ToListAsync();

        public async Task<BookModel?> GetBookById(long id) =>
            await _context.Books
            .Include(b => b.Set)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}
