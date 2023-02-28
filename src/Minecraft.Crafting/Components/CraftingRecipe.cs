using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Models;
using System;

namespace Minecraft.Crafting.Components
{
    /// <summary>
    /// Represents a crafting recipe that requires a set of items as input and gives an item as output.
    /// </summary>
    public class CraftingRecipe
    {
        /// <summary>
        /// The item that this recipe will produce.
        /// </summary>
        public Item Give { get; set; }

        /// <summary>
        /// The set of items required to craft the output item.
        /// </summary>
        public List<List<string>> Have { get; set; }
    }
}
