using Blazor_PerretTremblay.Models;

namespace Blazor_PerretTremblay.Components
{
    public class CraftingRecipe
    {
        public Item Give { get; set; }
        public List<List<string>> Have { get; set; }
    }
}
