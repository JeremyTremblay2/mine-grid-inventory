using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Minecraft.Crafting.Models;
using Minecraft.Crafting.Services.DataItemsService;
using Microsoft.Extensions.Localization;
using System;

namespace Minecraft.Crafting.Pages
{
    /// <summary>
    /// A partial class representing the Add component.
    /// </summary>
    public partial class Add
    {
        /// <summary>
        /// The default enchant categories.
        /// </summary>
        private List<string> enchantCategories = new List<string>() { "armor", "armor_head", "armor_chest", "weapon", "digger", "breakable", "vanishable" };

        /// <summary>
        /// The current item model.
        /// </summary>
        private ItemModel itemModel = new()
        {
            EnchantCategories = new List<string>(),
            RepairWith = new List<string>()
        };

        /// <summary>
        /// The default repair with.
        /// </summary>
        private List<string> repairWith = new List<string>() { "oak_planks", "spruce_planks", "birch_planks", "jungle_planks", "acacia_planks", "dark_oak_planks", "crimson_planks", "warped_planks" };

        /// <summary>
        /// The data items service.
        /// </summary>
        [Inject]
        public IDataItemsService DataService { get; set; }

        /// <summary>
        /// The navigation manager.
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// The localizer.
        /// </summary>
        [Inject]
        public IStringLocalizer<List> Localizer { get; set; }

        /// <summary>
        /// Handles the submit button click.
        /// </summary>
        private async void HandleValidSubmit()
        {
            await DataService.Add(itemModel);

            NavigationManager.NavigateTo("list");
        }

        /// <summary>
        /// Loads the image into the model.
        /// </summary>
        /// <param name="e">The input file change event arguments.</param>
        private async Task LoadImage(InputFileChangeEventArgs e)
        {
            // Set the content of the image to the model
            using (var memoryStream = new MemoryStream())
            {
                await e.File.OpenReadStream().CopyToAsync(memoryStream);
                itemModel.ImageContent = memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Handles the enchant categories checkbox change event.
        /// </summary>
        /// <param name="item">The item to add or remove.</param>
        /// <param name="checkedValue">The checked value.</param>
        private void OnEnchantCategoriesChange(string item, object checkedValue)
        {
            if ((bool)checkedValue)
            {
                if (!itemModel.EnchantCategories.Contains(item))
                {
                    itemModel.EnchantCategories.Add(item);
                }

                return;
            }

            if (itemModel.EnchantCategories.Contains(item))
            {
                itemModel.EnchantCategories.Remove(item);
            }
        }

        /// <summary>
        /// Handles the repair with checkbox change event.
        /// </summary>
        /// <param name="item">The item to add or remove.</param>
        /// <param name="checkedValue">The checked value.</param>
        private void OnRepairWithChange(string item, object checkedValue)
        {
            if ((bool)checkedValue)
            {
                if (!itemModel.RepairWith.Contains(item))
                {
                    itemModel.RepairWith.Add(item);
                }

                return;
            }

            if (itemModel.RepairWith.Contains(item))
            {
                itemModel.RepairWith.Remove(item);
            }
        }
    }
}
