using Minecraft.Crafting.Api.Controllers;
using Minecraft.Crafting.Api.Models;
using MinecraftCrafting.Services.DataInventoryService;

namespace Minecraft.Crafting.Services.DataInventoryService
{
    public class DataApiInventoryService : IDataInventoryService
    {
        private const string URLAPICONTROLLER = "https://localhost:7234/api/Inventory/";
        private readonly HttpClient _http;

        public DataApiInventoryService(HttpClient http)
        {
            this._http = http;
        }

        public async Task AddInventoryModel(InventoryModel inventoryModel)
        {
            var response = await _http.PostAsJsonAsync(URLAPICONTROLLER, inventoryModel);
        }

        public async Task<List<InventoryModel>> GetInventory()
        {
            return await _http.GetFromJsonAsync<List<InventoryModel>>(URLAPICONTROLLER);
        }

        public async Task UpdateInventoryModel(InventoryModel inventoryModel)
        {
            await _http.PutAsJsonAsync(URLAPICONTROLLER, inventoryModel);
        }
    }
}
