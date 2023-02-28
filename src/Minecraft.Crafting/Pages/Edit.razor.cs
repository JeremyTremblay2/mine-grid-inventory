using Minecraft.Crafting.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Minecraft.Crafting.Services.DataItemsService;
using Microsoft.Extensions.Localization;
using System;

namespace Minecraft.Crafting.Pages
{
    /// <summary>
    /// Represents a page for editing an item.
    /// </summary>
    public partial class Edit
    {
        [Parameter]
        public int Id { get; set; }

        /// <summary>
        /// The default enchant categories.
        /// </summary>
        private List<string> enchantCategories = new List<string>() { "armor", "armor_head", "armor_chest", "weapon", "digger", "breakable", "vanishable" };

        /// <summary>
        /// The current item model
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
        /// The data items service used to retrieve and manipulate items data.
        /// </summary>
        [Inject]
        public IDataItemsService DataService { get; set; }

        /// <summary>
        /// The hosting environment information service.
        /// </summary>
        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        /// <summary>
        /// The navigation manager used for navigating to different pages.
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// The localizer service used to provide localized strings.
        /// </summary>
        [Inject]
        public IStringLocalizer<List> Localizer { get; set; }

        /// <summary>
        /// Called when the component has been initialized.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            // Get the item to edit
            var item = await DataService.GetById(Id);

            // Load the default image file
            var fileContent = await File.ReadAllBytesAsync($"{WebHostEnvironment.WebRootPath}/images/default.png");

            // Set the model with the item
            itemModel = ItemFactory.ToModel(item, fileContent);
        }

        /// <summary>
        /// Handles the submit event of the form.
        /// </summary>
        private async void HandleValidSubmit()
        {
            await DataService.Update(Id, itemModel);

            // Navigate to the list page
            NavigationManager.NavigateTo("list");
        }

        /// <summary>
        /// Loads the image file into the item model.
        /// </summary>
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
        /// Handles a change to the enchant categories checkboxes.
        /// </summary>
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
        /// Handles a change to the repair with checkboxes.
        /// </summary>
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
