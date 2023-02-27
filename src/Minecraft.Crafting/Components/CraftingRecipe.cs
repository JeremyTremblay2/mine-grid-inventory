using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Models;

namespace Minecraft.Crafting.Components
{
    public class CraftingRecipe
    {
        public Item Give { get; set; }
        public List<List<string>> Have { get; set; }
    }
}
