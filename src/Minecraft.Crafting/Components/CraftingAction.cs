using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Models;

namespace Minecraft.Crafting.Components
{
    public class CraftingAction
    {
        public string Action { get; set; }
        public int Index { get; set; }
        public Item Item { get; set; }
    }
}
