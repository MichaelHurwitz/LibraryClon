using Library.Models;
using Library.ViewModel;
using Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Service
{
    public class ShelfService : IShelfService
    {
        private readonly ApplicationDbContext _context;

        public ShelfService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ShelfModel> AddShelf(ShelfVm shelfVm)
        {
            ShelfModel model = new()
            {
                Hight = shelfVm.Hight,
                Width = shelfVm.Width,
                LibraryId = shelfVm.LibraryId
            };
            await _context.Shelves
                .AddAsync(model);
            await _context
                .SaveChangesAsync();
            return model;
        }

        public async Task<ShelfModel?> UpdateShelf(ShelfVm shelfVm)
        {
            var shelf = await _context.Shelves
                .FindAsync(shelfVm.Id);
            if (shelf != null)
            {
                shelf.Hight = shelfVm.Hight;
                shelf.Width = shelfVm.Width;
                shelf.LibraryId = shelfVm.LibraryId;
                _context.Shelves.Update(shelf);
                await _context.SaveChangesAsync();
            }
            return shelf;
        }

        public async Task<IEnumerable<ShelfModel>> GetAllShelves() =>
            await _context.Shelves
            .Include(s => s.Library)
            .Include(s => s.Sets)
            .ThenInclude(s => s.Books)
            .ToListAsync();

        public async Task<ShelfModel?> GetShelfById(long id) =>
            await _context.Shelves
            .Include(s => s.Library)
            .Include(s => s.Sets)
            .ThenInclude(s => s.Books)
            .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<string> AddBookToShelf(long setId, BookModel book)
        {
            var set = await _context.Sets
                .Include(s => s.Shelf)
                .FirstOrDefaultAsync(s => s.Id == setId);
            if (set == null)
            {
                return "Set not found.";
            }

            var shelf = set.Shelf;
            if (shelf == null)
            {
                return "Shelf not found.";
            }

            if (book.Hight > shelf.Hight)
            {
                return "Book is too tall for the shelf.";
            }

            if (shelf.Hight - book.Hight >= 10)
            {
                return "Warning: Book is significantly shorter than the shelf height.";
            }

            int totalBooksWidth = set.Books
                .Sum(b => b.Width);
            if (totalBooksWidth + book.Width > shelf.Width)
            {
                return "Not enough space on the shelf for the book.";
            }

            set.Books
                .Add(book);
            await _context
                .SaveChangesAsync();
            return "Book added successfully.";
        }
    }
}
