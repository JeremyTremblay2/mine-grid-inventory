using Blazor_PerretTremblay.Models;
using Blazored.LocalStorage;
using Blazor_PerretTremblay.Services;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;

namespace Blazor_PerretTremblay.Pages
{
    public partial class List
    {
        private List<Item> items;

        private int totalItem;

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        private async Task OnReadData(DataGridReadDataEventArgs<Item> e)
        {
            if (e.CancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (!e.CancellationToken.IsCancellationRequested)
            {
                items = await DataService.List(e.Page, e.PageSize);
                totalItem = await DataService.Count();
            }
        }
    }
}
