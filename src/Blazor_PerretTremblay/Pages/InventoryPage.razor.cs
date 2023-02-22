using Blazor_PerretTremblay.Models;

namespace Blazor_PerretTremblay.Pages
{
    public partial class InventoryPage
    {
        public const int NBMAXITEM = 18;
        public List<Item> Items { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Items = new List<Item>();
            for (int i = 0; i < NBMAXITEM; i++)
            {
                Items.Add(new Item());
            }
        }
    }
}
