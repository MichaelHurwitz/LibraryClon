using Library.Models;
using Library.ViewModel;
using Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Service
{
    public class LibraryService : ILibraryService
    {
        private readonly ApplicationDbContext _context;

        public LibraryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LibraryModel> AddLibrary(LibraryVm libraryVm)
        {
            LibraryModel model = new()
            {
                Ganre = libraryVm.Ganre
            };
            await _context.Library
                .AddAsync(model);
            await _context
                .SaveChangesAsync();
            return model;
        }

        public async Task<LibraryModel?> UpdateLibrary(LibraryVm libraryVm)
        {
            var library = await _context.Library
                .FindAsync(libraryVm.Id);
            if (library != null)
            {
                library.Ganre = libraryVm.Ganre;
                _context.Library
                    .Update(library);
                await _context
                    .SaveChangesAsync();
            }
            return library;
        }

        public async Task<IEnumerable<LibraryModel>> GetAllLibraries() =>
            await _context.Library
            .ToListAsync();

        public async Task<LibraryModel?> GetLibraryById(long id) =>
            await _context.Library
            .FindAsync(id);
    }
}
