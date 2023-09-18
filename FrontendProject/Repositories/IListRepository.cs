using FrontendProject.Models;

namespace FrontendProject.Repositories
{
    public interface IListRepository
    {
        Task<IEnumerable<ListModel>> GetAllLists();
        Task<ListModel?> GetPopulatedList(int id);
        Task<ListModel> CreateList(string name);
        Task RenameList(int id, string name);
        Task DeleteList(int id);
    }
}
