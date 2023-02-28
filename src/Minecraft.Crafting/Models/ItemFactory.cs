using Minecraft.Crafting.Api.Models;
using System;

namespace Minecraft.Crafting.Models
{
    /// <summary>
    /// A factory class that provides methods for creating, updating and converting between <see cref="Item"/> and <see cref="ItemModel"/>.
    /// </summary>
    public static class ItemFactory
    {
        /// <summary>
        /// Converts an <see cref="Item"/> object and its image content to an <see cref="ItemModel"/> object.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> object to be converted.</param>
        /// <param name="imageContent">The image content of the item.</param>
        /// <returns>An <see cref="ItemModel"/> object that represents the converted <see cref="Item"/> object.</returns>
        public static ItemModel ToModel(Item item, byte[] imageContent)
        {
            return new ItemModel
            {
                Id = item.Id,
                DisplayName = item.DisplayName,
                Name = item.Name,
                RepairWith = item.RepairWith,
                EnchantCategories = item.EnchantCategories,
                MaxDurability = item.MaxDurability,
                StackSize = item.StackSize,
                ImageContent = imageContent,
                ImageBase64 = string.IsNullOrWhiteSpace(item.ImageBase64) ? Convert.ToBase64String(imageContent) : item.ImageBase64
            };
        }

        /// <summary>
        /// Creates a new <see cref="Item"/> object from an <see cref="ItemModel"/> object.
        /// </summary>
        /// <param name="model">The <see cref="ItemModel"/> object that represents the new <see cref="Item"/> object.</param>
        /// <returns>A new <see cref="Item"/> object that is created from the <see cref="ItemModel"/> object.</returns>
        public static Item Create(ItemModel model)
        {
            return new Item
            {
                Id = model.Id,
                DisplayName = model.DisplayName,
                Name = model.Name,
                RepairWith = model.RepairWith,
                EnchantCategories = model.EnchantCategories,
                MaxDurability = model.MaxDurability,
                StackSize = model.StackSize,
                CreatedDate = DateTime.Now,
                ImageBase64 = Convert.ToBase64String(model.ImageContent)
            };
        }

        /// <summary>
        /// Updates the properties of an <see cref="Item"/> object with the values from an <see cref="ItemModel"/> object.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> object to be updated.</param>
        /// <param name="model">The <see cref="ItemModel"/> object that contains the new values for the <see cref="Item"/> object.</param>
        public static void Update(Item item, ItemModel model)
        {
            item.DisplayName = model.DisplayName;
            item.Name = model.Name;
            item.RepairWith = model.RepairWith;
            item.EnchantCategories = model.EnchantCategories;
            item.MaxDurability = model.MaxDurability;
            item.StackSize = model.StackSize;
            item.UpdatedDate = DateTime.Now;
            item.ImageBase64 = Convert.ToBase64String(model.ImageContent);
        }
    }
}
