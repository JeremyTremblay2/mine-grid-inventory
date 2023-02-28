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
        private string api_url;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="http">Http client to request the api.</param>
        public DataApiService(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            api_url = configuration["API_URL"];
        }

        /// <inheritdoc/>
        public async Task Add(ItemModel model)
        {
            // Get the item
            var item = ItemFactory.Create(model);

            // Save the data
            await _http.PostAsJsonAsync($"{api_url}", item);
        }

        /// <inheritdoc/>
        public async Task<int> Count()
        {
            return await _http.GetFromJsonAsync<int>($"{api_url}/count");
        }

        /// <inheritdoc/>
        public async Task<List<Item>> List(int currentPage, int pageSize)
        {
            return await _http.GetFromJsonAsync<List<Item>>($"{api_url}/?currentPage={currentPage}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public async Task<Item> GetById(int id)
        {
            return await _http.GetFromJsonAsync<Item>($"{api_url}/{id}");
        }

        /// <inheritdoc/>
        public async Task Update(int id, ItemModel model)
        {
            // Get the item
            var item = ItemFactory.Create(model);

            await _http.PutAsJsonAsync($"{api_url}/{id}", item);
        }

        /// <inheritdoc/>
        public async Task Delete(int id)
        {
            await _http.DeleteAsync($"{api_url}/{id}");
        }
    }
}
