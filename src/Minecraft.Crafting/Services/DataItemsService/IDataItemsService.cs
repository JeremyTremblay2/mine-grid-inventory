using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Components;
using Minecraft.Crafting.Models;

namespace Minecraft.Crafting.Services.DataItemsService
{
    public interface IDataItemsService
    {
        Task Add(ItemModel model);

        Task<int> Count();

        Task<List<Item>> List(int currentPage, int pageSize);

        Task<Item> GetById(int id);

        Task Update(int id, ItemModel model);

        Task Delete(int id);

        Task<List<CraftingRecipe>> GetRecipes();
    }
}
