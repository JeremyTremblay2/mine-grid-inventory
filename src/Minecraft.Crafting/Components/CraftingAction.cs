using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Models;
using System;

namespace Minecraft.Crafting.Components
{
    /// <summary>
    /// Represents an action that can be performed during the crafting process.
    /// </summary>
    public class CraftingAction
    {
        /// <summary>
        /// The type of action to perform, such as "craft" or "uncraft".
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// The index of the item to perform the action on.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The item to use in the crafting action.
        /// </summary>
        public Item Item { get; set; }
    }
}
