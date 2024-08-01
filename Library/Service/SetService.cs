using Library.Models;
using Library.ViewModel;
using Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Service
{
    public class SetService : ISetService
    {
        private readonly ApplicationDbContext _context;

        public SetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SetModel> AddSet(SetVm setVm)
        {
            SetModel model = new()
            {
                SetName = setVm.SetName,
                ShelfId = setVm.ShelfId
            };
            await _context.Sets
                .AddAsync(model);
            await _context
                .SaveChangesAsync();
            return model;
        }

        public async Task<SetModel?> UpdateSet(SetVm setVm)
        {
            var set = await _context.Sets
                .FindAsync(setVm.Id);
            if (set != null)
            {
                set.SetName = setVm.SetName;
                set.ShelfId = setVm.ShelfId;
                _context.Sets
                    .Update(set);
                await _context
                    .SaveChangesAsync();
            }
            return set;
        }

        public async Task<IEnumerable<SetModel>> GetAllSets() =>
            await _context.Sets
            .Include(s => s.Shelf)
            .ToListAsync();

        public async Task<SetModel?> GetSetById(long id) =>
            await _context.Sets
            .Include(s => s.Shelf)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
