using Blazored.LocalStorage;
using Minecraft.Crafting.Api.Models;
using MinecraftCrafting.Services.DataInventoryService;
using System.Reflection.Metadata.Ecma335;

namespace Minecraft.Crafting.Services.DataInventoryService
{
    public class DataLocalInventoryService : IDataInventoryService
    {
        private const string KEYINVENTORY = "inventory";
        private ILocalStorageService _localStorageService;

        public DataLocalInventoryService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task AddInventoryModel(InventoryModel inventoryModel)
        {
            var inventory = await _localStorageService.GetItemAsync<List<InventoryModel>>(KEYINVENTORY);
            if (inventory == null) inventory = new List<InventoryModel>();

            inventory.Add(inventoryModel);

            await _localStorageService.SetItemAsync(KEYINVENTORY, inventory);
        }

        public async Task<List<InventoryModel>> GetInventory()
        {
            var res = await _localStorageService.GetItemAsync<List<InventoryModel>>(KEYINVENTORY);
            if (res == null)
                res = new List<InventoryModel>();

            return res;
        }

        public async Task UpdateInventoryModel(InventoryModel inventoryModel)
        {
            var inventory = await _localStorageService.GetItemAsync<List<InventoryModel>>(KEYINVENTORY);
            if (inventory == null) return;

            var i = inventory.Where(inv => inv.Position == inventoryModel.Position).FirstOrDefault();
            if (i == null) return;

            i.ItemName = inventoryModel.ItemName;
            i.NumberItem = inventoryModel.NumberItem;
            i.ImageBase64 = inventoryModel.ImageBase64;

            await _localStorageService.SetItemAsync(KEYINVENTORY, inventory);
        }
    }
}
