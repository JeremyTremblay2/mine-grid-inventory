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
using System;
using IModalService = Blazored.Modal.Services.IModalService;

namespace Minecraft.Crafting.Components
{
    /// <summary>
    /// A component that displays a list of available items.
    /// </summary>
    public partial class ListAvailableItems
    {
        /// <summary>
        /// The list of available items.
        /// </summary>
        private List<Item> items;

        /// <summary>
        /// The total number of items.
        /// </summary>
        private int totalItem;

        /// <summary>
        /// The currently selected item.
        /// </summary>
        private Item? currentSelectedItem;

        /// <summary>
        /// The data service used to retrieve item data.
        /// </summary>
        [Inject]
        public IDataItemsService DataService { get; set; }

        /// <summary>
        /// The web host environment.
        /// </summary>
        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        /// <summary>
        /// The navigation manager.
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// The modal service.
        /// </summary>
        [CascadingParameter]
        public IModalService Modal { get; set; }

        /// <summary>
        /// The parent inventory component.
        /// </summary>
        [CascadingParameter]
        public Inventory Parent { get; set; }

        /// <summary>
        /// A boolean value indicating whether the item can be dropped.
        /// </summary>
        [Parameter]
        public bool NoDrop { get; set; }

        /// <summary>
        /// Event handler called when reading data from the data grid.
        /// </summary>
        /// <param name="e">Data grid read data event arguments.</param>
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

        /// <summary>
        /// Event handler called when deleting an item from the list.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
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

        /// <summary>
        /// Event handler called when dragging starts on an item.
        /// </summary>
        private void OnDragStart()
        {
            Parent.IsDropped = true;
            Parent.CurrentDragItem = currentSelectedItem;
            Parent.IsDragBetweenInventoryAndInventory = false;
            Parent.IsDragBetweenListAndInventory = true;
            Parent.CurrentIndexOfCurrentDragItem = -1;
            Parent.Actions.Add(new InventoryAction { Action = "On drag start", Item = Parent.CurrentDragItem.Name });
        }

        /// <summary>
        /// Event handler called when dragging ends.
        /// </summary>
        private void OnDragEnd()
        {
            Parent.IsDropped = false;
            Parent.Actions.Add(new InventoryAction { Action = "On drag end", Item = Parent.CurrentDragItem.Name });
        }

        /// <summary>
        /// Event handler called when a row is updated.
        /// </summary>
        /// <param name="newRow">The updated row.</param>
        private void RowUpdated(Item newRow)
        {
            currentSelectedItem = newRow;
        }
    }
}
