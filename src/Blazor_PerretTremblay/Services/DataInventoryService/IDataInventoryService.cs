using Blazor_PerretTremblay.Models;

namespace Blazor_PerretTremblay.Services.DataInventoryService
{
    public interface IDataInventoryService
    {
        public Task<IList<Item>> GetAllItems();
        public Task<IList<int>> GetListNumberOfItems();

        public Task SaveInventory(IList<Item> items, IList<int> listNumberOfItems);
    }
}
