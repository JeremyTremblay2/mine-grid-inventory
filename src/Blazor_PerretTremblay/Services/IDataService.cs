using Blazor_PerretTremblay.Models;

namespace Blazor_PerretTremblay.Services
{
    public interface IDataService
    {
        Task Add(ItemModel model);

        Task<int> Count();

        Task<List<Item>> List(int currentPage, int pageSize);

        Task<Item> GetById(int id);

        Task Update(int id, ItemModel model);

        Task Delete(int id);
    }
}
