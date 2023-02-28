using MinecraftCrafting.Services.DataInventoryService;
using Microsoft.AspNetCore.Components;
using Minecraft.Crafting.Models;
using Minecraft.Crafting.Api.Models;
using System;

namespace Minecraft.Crafting.Pages
{
    /// <summary>
    /// Partial class representing the inventory page.
    /// </summary>
    public partial class InventoryPage
    {
        /// <summary>
        /// The maximum number of items in the inventory.
        /// </summary>
        public const int NBMAXITEM = 18;

        /// <summary>
        /// The list of items in the inventory.
        /// </summary>
        public IList<Item> Items { get; set; }

        /// <summary>
        /// The list of number of items by index.
        /// </summary>
        public IList<int> ListNumberOfItemsByIndex { get; set; }

        /// <summary>
        /// The data inventory service used to retrieve the inventory data.
        /// </summary>
        [Inject]
        public IDataInventoryService DataInventoryService { get; set; }
    }
}
