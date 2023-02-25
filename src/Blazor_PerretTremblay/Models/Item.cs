namespace Blazor_PerretTremblay.Models
{
    public class Item : IEquatable<Item?>
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public int StackSize { get; set; }
        public int MaxDurability { get; set; }
        public List<string> EnchantCategories { get; set; }
        public List<string> RepairWith { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Item()
        {
            Name = "Bench";
            DisplayName = "Bench";
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Item);
        }

        public bool Equals(Item? other)
        {
            return other is not null &&
                   Id == other.Id;
        }
    }
}
