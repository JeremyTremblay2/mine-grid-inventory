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

        [Inject]
        public IDataInventoryService DataInventoryService { get; set; }


        [Inject]
        public ILogger<Inventory> Logger { get; set; }

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

        protected override async Task OnInitializedAsync()
        {
            /*Items = await DataInventoryService.GetAllItems();
            ListNumberOfItemsByIndex = await DataInventoryService.GetListNumberOfItems();*/
            await base.OnInitializedAsync();
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

        public void AddItem(InventoryModel item, int index)
        {
            InventoryModels.Insert(index, item);
        }

        public void UpdateItemInventory(int index, InventoryModel itemInventory)
        {
            var item = InventoryModels.ElementAt(index);
            item.ItemName = itemInventory.ItemName;
            item.NumberItem = itemInventory.NumberItem;
        }

        public void DeleteOlderItemInventory()
        {
            var olderInventoryModel = InventoryModels.ElementAt(CurrentIndexOfCurrentDragItem);
            olderInventoryModel.ItemName = null;
            olderInventoryModel.NumberItem = 0;
        }

        public async Task SaveInventory()
        {
            // await DataInventoryService.SaveInventory(Item, ListNumberOfItemsByIndex);
        }
    }
}
