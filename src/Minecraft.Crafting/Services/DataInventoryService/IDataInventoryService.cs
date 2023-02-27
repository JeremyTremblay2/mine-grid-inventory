using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Models;

namespace MinecraftCrafting.Services.DataInventoryService
{
    public interface IDataInventoryService
    {

        public Task<List<InventoryModel>> GetInventory();
        public Task AddInventoryModel(InventoryModel inventoryModel);
        public Task UpdateInventoryModel(InventoryModel inventoryModel);

    }
}
