using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Components;
using Minecraft.Crafting.Models;

namespace Minecraft.Crafting.Services.DataItemsService
{
    /// <summary>
    /// Service for the loading and the saving of items.
    /// </summary>
    public class DataApiService : IDataItemsService
    {
        /// <summary>
        /// Http client to request the api.
        /// </summary>
        private readonly HttpClient _http;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="http">Http client to request the api.</param>
        public DataApiService(HttpClient http)
        {
            _http = http;
        }

        /// <inheritdoc/>
        public async Task Add(ItemModel model)
        {
            // Get the item
            var item = ItemFactory.Create(model);

            // Save the data
            await _http.PostAsJsonAsync("https://localhost:7234/api/Crafting/", item);
        }

        /// <inheritdoc/>
        public async Task<int> Count()
        {
            return await _http.GetFromJsonAsync<int>("https://localhost:7234/api/Crafting/count");
        }

        /// <inheritdoc/>
        public async Task<List<Item>> List(int currentPage, int pageSize)
        {
            return await _http.GetFromJsonAsync<List<Item>>($"https://localhost:7234/api/Crafting/?currentPage={currentPage}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public async Task<Item> GetById(int id)
        {
            return await _http.GetFromJsonAsync<Item>($"https://localhost:7234/api/Crafting/{id}");
        }

        /// <inheritdoc/>
        public async Task Update(int id, ItemModel model)
        {
            // Get the item
            var item = ItemFactory.Create(model);

            await _http.PutAsJsonAsync($"https://localhost:7234/api/Crafting/{id}", item);
        }

        /// <inheritdoc/>
        public async Task Delete(int id)
        {
            await _http.DeleteAsync($"https://localhost:7234/api/Crafting/{id}");
        }

        /// <inheritdoc/>
        public async Task<List<CraftingRecipe>> GetRecipes()
        {
            return await _http.GetFromJsonAsync<List<CraftingRecipe>>("https://localhost:7234/api/Crafting/recipe");
        }
    }
}
