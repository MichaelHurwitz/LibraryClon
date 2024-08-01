using Library.Models;
using Library.ViewModel;

public interface IShelfService
{
    Task<ShelfModel> AddShelf(ShelfVm shelfVm);
    Task<ShelfModel?> UpdateShelf(ShelfVm shelfVm);
    Task<IEnumerable<ShelfModel>> GetAllShelves();
    Task<ShelfModel?> GetShelfById(long id);
    Task<string> AddBookToShelf(long shelfId, BookModel book);
}
