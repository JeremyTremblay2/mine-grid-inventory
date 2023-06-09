﻿using Minecraft.Crafting.Components;
using Minecraft.Crafting.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Minecraft.Crafting.Api.Models;

namespace Minecraft.Crafting.Services.DataItemsService
{
    /// <summary>
    /// Service for the loading and the saving of items.
    /// </summary>
    public class DataLocalIService : IDataItemsService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="localStorage">LocalStorage.</param>
        /// <param name="http">Http client;</param>
        /// <param name="webHostEnvironment">Web environnement.</param>
        /// <param name="navigationManager">Navgigation manager.</param>
        public DataLocalIService(
            ILocalStorageService localStorage,
            HttpClient http,
            IWebHostEnvironment webHostEnvironment,
            NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            _http = http;
            _webHostEnvironment = webHostEnvironment;
            _navigationManager = navigationManager;
        }

        /// <inheritdoc/>
        public async Task Add(ItemModel model)
        {
            // Get the current data
            var currentData = await _localStorage.GetItemAsync<List<Item>>("data");

            // Simulate the Id
            model.Id = currentData.Max(s => s.Id) + 1;

            // Add the item to the current data
            currentData.Add(ItemFactory.Create(model));

            // Save the data
            await _localStorage.SetItemAsync("data", currentData);
        }

        /// <inheritdoc/>
        public async Task<int> Count()
        {
            // Load data from the local storage
            var currentData = await _localStorage.GetItemAsync<Item[]>("data");

            // Check if data exist in the local storage
            if (currentData == null)
            {
                // this code add in the local storage the fake data
                var originalData = await _http.GetFromJsonAsync<Item[]>($"{_navigationManager.BaseUri}fake-data.json");
                await _localStorage.SetItemAsync("data", originalData);
            }

            return (await _localStorage.GetItemAsync<Item[]>("data")).Length;
        }

        /// <inheritdoc/>
        public async Task<List<Item>> List(int currentPage, int pageSize)
        {
            // Load data from the local storage
            var currentData = await _localStorage.GetItemAsync<Item[]>("data");

            // Check if data exist in the local storage
            if (currentData == null)
            {
                // this code add in the local storage the fake data
                var originalData = await _http.GetFromJsonAsync<Item[]>($"{_navigationManager.BaseUri}fake-data.json");
                await _localStorage.SetItemAsync("data", originalData);
            }

            return (await _localStorage.GetItemAsync<Item[]>("data")).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <inheritdoc/>
        public async Task<Item> GetById(int id)
        {
            // Get the current data
            var currentData = await _localStorage.GetItemAsync<List<Item>>("data");

            // Get the item int the list
            var item = currentData.FirstOrDefault(w => w.Id == id);

            // Check if item exist
            if (item == null)
            {
                throw new Exception($"Unable to found the item with ID: {id}");
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task Update(int id, ItemModel model)
        {
            // Get the current data
            var currentData = await _localStorage.GetItemAsync<List<Item>>("data");

            // Get the item int the list
            var item = currentData.FirstOrDefault(w => w.Id == id);

            // Check if item exist
            if (item == null)
            {
                throw new Exception($"Unable to found the item with ID: {id}");
            }

            // Modify the content of the item
            ItemFactory.Update(item, model);

            // Save the data
            await _localStorage.SetItemAsync("data", currentData);
        }

        /// <inheritdoc/>
        public async Task Delete(int id)
        {
            // Get the current data
            var currentData = await _localStorage.GetItemAsync<List<Item>>("data");

            // Get the item int the list
            var item = currentData.FirstOrDefault(w => w.Id == id);

            // Delete item in
            currentData.Remove(item);

            // Save the data
            await _localStorage.SetItemAsync("data", currentData);
        }
    }
}
