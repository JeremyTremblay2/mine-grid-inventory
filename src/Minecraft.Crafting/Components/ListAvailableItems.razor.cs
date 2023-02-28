using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Components;
using Minecraft.Crafting.Modals;
using Minecraft.Crafting.Models;
using Minecraft.Crafting.Services.DataItemsService;
using IModalService = Blazored.Modal.Services.IModalService;

namespace Minecraft.Crafting.Components
{
    public partial class ListAvailableItems
    {
        private List<Item> items;

        private int totalItem;

        private Item? currentSelectedItem;

        [Inject]
        public IDataItemsService DataService { get; set; }

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
            Parent.IsDropped = true;
            Parent.CurrentDragItem = currentSelectedItem;
            Parent.IsDragBetweenInventoryAndInventory = false;
            Parent.IsDragBetweenListAndInventory = true;
            Parent.CurrentIndexOfCurrentDragItem = -1;
            Parent.Actions.Add(new InventoryAction { Action = "On drag start", Item = Parent.CurrentDragItem.Name });
        }

        private void OnDragEnd()
        {
            Parent.IsDropped = false;
            Parent.Actions.Add(new InventoryAction { Action = "On drag end", Item = Parent.CurrentDragItem.Name });
        }

        private void RowUpdated(Item newRow)
        {
            currentSelectedItem = newRow;
        }
    }
}
