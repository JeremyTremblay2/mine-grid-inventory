using MinecraftCrafting.Services.DataInventoryService;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Minecraft.Crafting.Pages;
using Minecraft.Crafting.Api.Models;
using Item = Minecraft.Crafting.Api.Models.Item;
using Blazorise;
using Minecraft.Crafting.Services.DataItemsService;

namespace Minecraft.Crafting.Components
{
    public partial class Inventory
    {

        public bool IsDropped { get; set; } = false;

        public ObservableCollection<InventoryAction> Actions { get; set; }

        /// <summary>
        /// The current Item which is dragged.
        /// </summary>
        public Item? CurrentDragItem { get; set; }

        /// <summary>
        /// The index of the current draggued item.
        /// </summary>
        public int CurrentIndexOfCurrentDragItem { get; set; }

        public IList<InventoryModel> InventoryModels { get; set; }

        public bool IsDragBetweenListAndInventory { get; set; }
        public bool IsDragBetweenInventoryAndInventory { get; set; }

        /// <summary>
        /// Gets or sets the java script runtime.
        /// </summary>
        [Inject]
        internal IJSRuntime JavaScriptRuntime { get; set; }

        [Inject]
        public IDataInventoryService DataInventoryService { get; set; }

        public Inventory()
        {
            Actions = new ObservableCollection<InventoryAction>();
            Actions.CollectionChanged += OnActionsCollectionChanged;
            InventoryModels = new List<InventoryModel>();
            for (int i = 0; i < InventoryPage.NBMAXITEM; i++)
            {
                InventoryModels.Add(new InventoryModel() { Position = i });
            }
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                InventoryModels = await DataInventoryService.GetInventory();
                if (InventoryModels.Count == 0)
                    await InitInventoryInAPI();
            }
            catch (Exception e)
            {
                await InitInventoryInAPI();
            }
            StateHasChanged();
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task InitInventoryInAPI()
        {
            for (int i = 0; i < InventoryPage.NBMAXITEM; i++)
            {
                await DataInventoryService.AddInventoryModel(new InventoryModel() { Position = i });
            }
            InventoryModels = await DataInventoryService.GetInventory();
        }

        private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            JavaScriptRuntime.InvokeVoidAsync("Crafting.AddActions", e.NewItems);
        }

        public void AddItem(InventoryModel item, int index)
        {
            InventoryModels.Insert(index, item);
        }

        public async Task UpdateInventoryModel(InventoryModel inventoryModel)
        {
            await DataInventoryService.UpdateInventoryModel(inventoryModel);
        }

        public async Task DeleteOlderItemInventory()
        {
            var olderInventoryModel = InventoryModels.ElementAt(CurrentIndexOfCurrentDragItem);
            olderInventoryModel.ItemName = null;
            olderInventoryModel.NumberItem = 0;
            await DataInventoryService.UpdateInventoryModel(olderInventoryModel);
        }
    }
}
