using MinecraftCrafting.Services.DataInventoryService;
using Microsoft.AspNetCore.Components;
using Minecraft.Crafting.Models;
using Minecraft.Crafting.Api.Models;

namespace Minecraft.Crafting.Pages
{
    public partial class InventoryPage
    {
        public const int NBMAXITEM = 18;

        public IList<Item> Items { get; set; }

        public IList<int> ListNumberOfItemsByIndex { get; set; }

        [Inject]
        public IDataInventoryService DataInventoryService { get; set; }


       
    }
}
