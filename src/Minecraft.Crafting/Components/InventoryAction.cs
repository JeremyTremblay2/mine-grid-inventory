using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Models;

namespace Minecraft.Crafting.Components
{
    public class InventoryAction
    {
        public string Action { get; set; }
        public int Index { get; set; }
        public string Item { get; set; }
    }
}
