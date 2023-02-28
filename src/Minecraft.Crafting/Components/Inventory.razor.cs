using MinecraftCrafting.Services.DataInventoryService;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Minecraft.Crafting.Pages;
using Minecraft.Crafting.Api.Models;
using Item = Minecraft.Crafting.Api.Models.Item;
using Blazorise;
using System;

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
        internal IJSRuntime JavaScriptRuntime { get; set; }

        /// <summary>
        /// Gets or sets the inventory service used to store and retrieve data.
        /// </summary>
        [Inject]
        public IDataInventoryService DataInventoryService { get; set; }

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
                InventoryModels.Add(new InventoryModel());
            }
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            /*Items = await DataInventoryService.GetAllItems();
            ListNumberOfItemsByIndex = await DataInventoryService.GetListNumberOfItems();*/
            await base.OnInitializedAsync();
        }

        private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            JavaScriptRuntime.InvokeVoidAsync("Crafting.AddActions", e.NewItems);
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
        /// <param name="index">The index of the item to update.</param>
        /// <param name="itemInventory">The new inventory model for the item.</param>
        public void UpdateItemInventory(int index, InventoryModel itemInventory)
        {
            var item = InventoryModels.ElementAt(index);
            item.ItemName = itemInventory.ItemName;
            item.NumberItem = itemInventory.NumberItem;
        }

        /// <summary>
        /// Deletes the item from the inventory model at the current index of the dragged item.
        /// </summary>
        public void DeleteOlderItemInventory()
        {
            var olderInventoryModel = InventoryModels.ElementAt(CurrentIndexOfCurrentDragItem);
            olderInventoryModel.ItemName = null;
            olderInventoryModel.NumberItem = 0;
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
