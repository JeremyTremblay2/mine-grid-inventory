using Minecraft.Crafting.Models;
using Blazored.LocalStorage;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Blazored.Modal.Services;
using Minecraft.Crafting.Modals;
using Blazored.Modal;
using Minecraft.Crafting.Services.DataItemsService;
using Minecraft.Crafting.Api.Models;
using Microsoft.Extensions.Localization;

namespace Minecraft.Crafting.Pages
{
    public partial class List
    {
        /// <summary>
        /// The list of items.
        /// </summary>
        private List<Item> items;

        /// <summary>
        /// The total number of items.
        /// </summary>
        private int totalItem;

        /// <summary>
        /// The data items service used to retrieve and manipulate items data.
        /// </summary>
        [Inject]
        public IDataItemsService DataService { get; set; }

        /// <summary>
        /// The hosting environment information service.
        /// </summary>
        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        /// <summary>
        /// The navigation manager used for navigating to different pages.
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// The cascading modal service used to display modal dialogs.
        /// </summary>
        [CascadingParameter]
        public IModalService Modal { get; set; }

        /// <summary>
        /// The localizer service used to provide localized strings.
        /// </summary>
        [Inject]
        public IStringLocalizer<List> Localizer { get; set; }

        /// <summary>
        /// Method called when the DataGrid needs to retrieve data for display.
        /// </summary>
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
        /// Method called when an item needs to be deleted.
        /// </summary>
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
    }
}
