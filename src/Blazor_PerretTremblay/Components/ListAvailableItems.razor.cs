using Blazor_PerretTremblay.Modals;
using Blazor_PerretTremblay.Models;
using Blazor_PerretTremblay.Services;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using IModalService = Blazored.Modal.Services.IModalService;

namespace Blazor_PerretTremblay.Components
{
    public partial class ListAvailableItems
    {
        private List<Item> items;

        private int totalItem;

        private Item? currentSelectedItem;

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        [CascadingParameter]
        public Inventory Parent { get; set; }

        [Parameter]
        public bool NoDrop { get; set; }

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

        private async void OnDelete(int id)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(Item.Id), id);

            var modal = Modal.Show<DeleteConfirmation>("Delete Confirmation", parameters);
            var result = await modal.Result;

            if (result.Cancelled)
            {
                return;
            }

            await DataService.Delete(id);

            // Reload the page
            NavigationManager.NavigateTo("list", true);
        }

       

        private void OnDragStart()
        {
            Parent.CurrentDragItem = currentSelectedItem;

            Parent.Actions.Add(new InventoryAction { Action = "Drag Start", Item = currentSelectedItem }); ;
        }

        private void OnDragEnd()
        {
            Parent.CurrentDragItem = null;
            currentSelectedItem = null;
        }
        private void RowUpdated(Item newRow)
        {
            currentSelectedItem = newRow;
        }
    }
}
