using Library.Models;
using Library.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service
{
    public interface IBookService
    {
        Task<BookModel> AddBook(BookVm bookVm);
        Task<BookModel?> UpdateBook(BookVm bookVm);
        Task<BookModel?> GetBookById(long id);
        Task<IEnumerable<BookModel>> GetAllBooks();
    }
}
