using Blazor_PerretTremblay.Models;
using Blazor_PerretTremblay.Services;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;

namespace Blazor_PerretTremblay.Pages
{
    public partial class List
    {
        private Item[] items;

        private int totalItem;

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            items = await Http.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json");
        }

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

        private async Task OnReadData(DataGridReadDataEventArgs<Item> e)
        {
            if (e.CancellationToken.IsCancellationRequested)
            {
                return;
            }

<<<<<<< Updated upstream
            // When you use a real API, we use this follow code
            //var response = await Http.GetJsonAsync<Item[]>( $"http://my-api/api/data?page={e.Page}&pageSize={e.PageSize}" );
=======
>>>>>>> Stashed changes
            var response = (await Http.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json"))
                .Skip((e.Page - 1) * e.PageSize)
                .Take(e.PageSize)
                .ToList();

            if (!e.CancellationToken.IsCancellationRequested)
            {
                totalItem = (await Http.GetFromJsonAsync<List<Item>>($"{NavigationManager.BaseUri}fake-data.json")).Count;
                items = new List<Item>(response).ToArray(); // an actual data for the current page
            }
        }
    }
}
