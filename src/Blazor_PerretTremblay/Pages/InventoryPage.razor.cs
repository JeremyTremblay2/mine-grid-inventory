using Blazor_PerretTremblay.Models;
using Blazor_PerretTremblay.Services.DataInventoryService;
using Microsoft.AspNetCore.Components;

namespace Blazor_PerretTremblay.Pages
{
    public partial class InventoryPage
    {
        public const int NBMAXITEM = 18;

        public IList<Item> Items { get; set; }

        public IList<int> ListNumberOfItemsByIndex { get; set; }

        [Inject]
        public IDataInventoryService DataInventoryService { get; set; }


       
    }
}
