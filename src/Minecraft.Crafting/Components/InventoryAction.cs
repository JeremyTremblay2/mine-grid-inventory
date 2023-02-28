using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Models;
using System;

namespace Minecraft.Crafting.Components
{
    /// <summary>
    /// Represents an action that can be performed on an inventory.
    /// </summary>
    public class InventoryAction
    {
        /// <summary>
        /// The type of action to perform, such as "add" or "remove".
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// The index of the item to perform the action on.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The item to add or remove from the inventory.
        /// </summary>
        public Item Item { get; set; }
    }
}
