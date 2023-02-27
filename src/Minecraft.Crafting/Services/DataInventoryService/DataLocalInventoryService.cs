using Blazored.LocalStorage;
using Blazorise;
using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Models;
using Minecraft.Crafting.Pages;

namespace MinecraftCrafting.Services.DataInventoryService
{
    public class DataLocalInventoryService : IDataInventoryService
    {
        private readonly ILocalStorageService _localStorage;
        private const string KEYITEMS = "inventoryItems";
        private const string KEYNUMBERITEMS = "inventoryNumberItems";

        public DataLocalInventoryService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<IList<Item>> GetAllItems()
        {
            var _currentData = await _localStorage.GetItemAsync<List<Item>>(KEYITEMS);
            if(_currentData == null)
            {
                _currentData = new List<Item>();
                for (int i = 0; i < InventoryPage.NBMAXITEM; i++)
                {
                    _currentData.Add(new Item());
                }
            }

            return _currentData;
        }

        public async Task<IList<int>> GetListNumberOfItems()
        {
            var _currentData = await _localStorage.GetItemAsync<List<int>>(KEYNUMBERITEMS);
            if (_currentData == null)
            {
                _currentData = new List<int>();
                for (int i = 0; i < InventoryPage.NBMAXITEM; i++)
                {
                    _currentData.Add(0);
                }
            }

            return _currentData;
        }

        public async Task SaveInventory(IList<Item> items, IList<int> listNumberOfItems)
        {
            await _localStorage.SetItemAsync(KEYITEMS, items);
            await _localStorage.SetItemAsync(KEYNUMBERITEMS, listNumberOfItems);
        }

        
    }
}
