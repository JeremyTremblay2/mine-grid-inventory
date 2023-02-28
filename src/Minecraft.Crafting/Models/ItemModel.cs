using System;
using System.ComponentModel.DataAnnotations;

namespace Minecraft.Crafting.Models
{
    /// <summary>
    /// Model class representing an item.
    /// </summary>
    public class ItemModel
    {
        /// <summary>
        /// Gets or sets the ID of the item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the display name of the item.
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "The display name must not exceed 50 characters.")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "The name must not exceed 50 characters.")]
        [RegularExpression(@"^[a-z''-'\s]{1,50}$", ErrorMessage = "Only lowercase characters are accepted.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the stack size of the item.
        /// </summary>
        [Required]
        [Range(1, 64)]
        public int StackSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum durability of the item.
        /// </summary>
        [Required]
        [Range(1, 125)]
        public int MaxDurability { get; set; }

        /// <summary>
        /// Gets or sets the enchantment categories of the item.
        /// </summary>
        public List<string> EnchantCategories { get; set; }

        /// <summary>
        /// Gets or sets the items that can be used to repair the item.
        /// </summary>
        public List<string> RepairWith { get; set; }

        /// <summary>
        /// Gets or sets whether the user accepts the conditions for using the item.
        /// </summary>
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the terms.")]
        public bool AcceptCondition { get; set; }

        /// <summary>
        /// Gets or sets the byte content of the image of the item.
        /// </summary>
        [Required(ErrorMessage = "The image of the item is mandatory!")]
        public byte[] ImageContent { get; set; }

        /// <summary>
        /// Gets or sets the base64 string representation of the image of the item.
        /// </summary>
        public string ImageBase64 { get; set; }
    }
}
