using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Components;
using Minecraft.Crafting.Models;

namespace Minecraft.Crafting.Services.DataItemsService
{
    /// <summary>
    /// Service for the loading and the saving of items.
    /// </summary>
    public interface IDataItemsService
    {
        /// <summary>
        /// Add an item.
        /// </summary>
        /// <param name="model">Item to add.</param>
        /// <returns></returns>
        Task Add(ItemModel model);

        /// <summary>
        /// Return the number of all items.
        /// </summary>
        /// <returns>Number of all items.</returns>
        Task<int> Count();

        /// <summary>
        /// Return the list of items according to the current page and the page size.
        /// </summary>
        /// <param name="currentPage">Current page.</param>
        /// <param name="pageSize">Number of elements to return.</param>
        /// <returns>A list.</returns>
        Task<List<Item>> List(int currentPage, int pageSize);

        /// <summary>
        /// Get an item by its id.
        /// </summary>
        /// <param name="id">Item's id to retrieve.</param>
        /// <returns>Item.</returns>
        Task<Item> GetById(int id);

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="id">Item's id to update.</param>
        /// <param name="model">>Item to modify.</param>
        /// <returns>A task.</returns>
        Task Update(int id, ItemModel model);

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="id">Item's id to delete.</param>
        /// <returns>A Task.</returns>
        Task Delete(int id);

        Task<List<CraftingRecipe>> GetRecipes();
    }
}
