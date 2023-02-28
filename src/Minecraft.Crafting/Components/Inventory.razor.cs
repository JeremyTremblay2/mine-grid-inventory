using MinecraftCrafting.Services.DataInventoryService;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Minecraft.Crafting.Pages;
using Minecraft.Crafting.Api.Models;
using Item = Minecraft.Crafting.Api.Models.Item;
using Blazorise;

namespace Minecraft.Crafting.Components
{
    /// <summary>
    /// Represents an inventory in the game.
    /// </summary>
    public partial class Inventory
    {
        /// <summary>
        /// Gets or sets a boolean value indicating whether an item has been dropped.
        /// </summary>
        public bool IsDropped { get; set; } = false;

        /// <summary>
        /// Gets or sets the list of actions performed on the inventory.
        /// </summary>
        public ObservableCollection<InventoryAction> Actions { get; set; }

        /// <summary>
        /// Gets or sets the current item being dragged.
        /// </summary>
        public Item? CurrentDragItem { get; set; }

        /// <summary>
        /// Gets or sets the index of the current dragged item.
        /// </summary>
        public int CurrentIndexOfCurrentDragItem { get; set; }

        /// <summary>
        /// Gets or sets the list of inventory models.
        /// </summary>
        public IList<InventoryModel> InventoryModels { get; set; }

        /// <summary>
        /// Gets or sets a boolean value indicating whether an item is being dragged between a list and an inventory.
        /// </summary>
        public bool IsDragBetweenListAndInventory { get; set; }

        /// <summary>
        /// Gets or sets a boolean value indicating whether an item is being dragged between two inventories.
        /// </summary>
        public bool IsDragBetweenInventoryAndInventory { get; set; }

        /// <summary>
        /// Gets or sets the JavaScript runtime.
        /// </summary>
        [Inject]
        public IDataInventoryService DataInventoryService { get; set; }


        /// <summary>
        /// Gets or sets the inventory service used to store and retrieve data.
        /// </summary>
        [Inject]
        public ILogger<Inventory> Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inventory"/> class.
        /// </summary>
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
                    await InitInventoryInStorage();
            }
            catch (Exception e)
            {
                await InitInventoryInStorage();
            }
            StateHasChanged();
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task InitInventoryInStorage()
        {
            for (int i = 0; i < InventoryPage.NBMAXITEM; i++)
            {
                await DataInventoryService.AddInventoryModel(new InventoryModel() { Position = i });
            }
            InventoryModels = await DataInventoryService.GetInventory();
        }

        private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                foreach (InventoryAction action in e.NewItems)
                {
                    Logger.LogInformation($"{action.Action} : ${action.Item} with index {action.Index}");
                }
            }
        }

        /// <summary>
        /// Adds an item to the inventory at the specified index.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="index">The index where to add the item.</param>
        public void AddItem(InventoryModel item, int index)
        {
            InventoryModels.Insert(index, item);
        }

        /// <summary>
        /// Updates an item in the inventory at the specified index.
        /// </summary>
        /// <param name="inventoryModel">The new inventory model for the item.</param>
        public async Task UpdateItemInventory(InventoryModel inventoryModel)
        {
            await DataInventoryService.UpdateInventoryModel(inventoryModel);
        }

        /// <summary>
        /// Deletes the item from the inventory model at the current index of the dragged item.
        /// </summary>
        public async Task DeleteOlderItemInventory()
        {
            var olderInventoryModel = InventoryModels.ElementAt(CurrentIndexOfCurrentDragItem);
            olderInventoryModel.ItemName = null;
            olderInventoryModel.NumberItem = 0;
            await DataInventoryService.UpdateInventoryModel(olderInventoryModel);
        }

        /// <summary>
        /// Saves the current inventory to the data inventory service.
        /// </summary>
        /// <returns>A task representing the asynchronous save operation.</returns>
        public async Task SaveInventory()
        {
            // await DataInventoryService.SaveInventory(Item, ListNumberOfItemsByIndex);
        }
    }
}
