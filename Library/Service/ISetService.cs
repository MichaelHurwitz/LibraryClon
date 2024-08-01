using Library.Models;
using Library.ViewModel;

public interface ISetService
{
    Task<SetModel> AddSet(SetVm setVm);
    Task<SetModel?> UpdateSet(SetVm setVm);
    Task<IEnumerable<SetModel>> GetAllSets();
    Task<SetModel?> GetSetById(long id);
}
