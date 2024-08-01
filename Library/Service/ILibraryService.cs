using Library.Models;
using Library.ViewModel;

public interface ILibraryService
{
    Task<LibraryModel> AddLibrary(LibraryVm libraryVm);
    Task<LibraryModel?> UpdateLibrary(LibraryVm libraryVm);
    Task<IEnumerable<LibraryModel>> GetAllLibraries();
    Task<LibraryModel?> GetLibraryById(long id);
}
