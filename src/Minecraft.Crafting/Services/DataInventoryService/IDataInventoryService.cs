using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Models;

namespace MinecraftCrafting.Services.DataInventoryService
{
    public interface IDataInventoryService
    {
        public Task<IList<Item>> GetAllItems();
        public Task<IList<int>> GetListNumberOfItems();

        public Task SaveInventory(IList<Item> items, IList<int> listNumberOfItems);
    }
}
