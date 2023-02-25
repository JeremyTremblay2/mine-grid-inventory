using Blazor_PerretTremblay.Models;
using Blazor_PerretTremblay.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Blazor_PerretTremblay.Components
{
    public partial class Inventory
    {
        public ObservableCollection<InventoryAction> Actions { get; set; }

        /// <summary>
        /// The current Item which is dragged.
        /// </summary>
        public Item? CurrentDragItem { get; set; }

        /// <summary>
        /// The index of the current draggued item.
        /// </summary>
        public int CurrentIndexOfCurrentDragItem { get; set; }

        /// <summary>
        /// List of all items contained in the inventory.
        /// </summary>
        [Parameter]
        public List<Item> Items { get; set; }

        /// <summary>
        /// List of all number of each items in the inventory.
        /// </summary>
        [Parameter]
        public List<int> ListNumberOfItemsByIndex { get; set; }

        public bool IsDragBetweenListAndInventory { get; set; }
        public bool IsDragBetweenInventoryAndInventory { get; set; }

        /// <summary>
        /// Gets or sets the java script runtime.
        /// </summary>
        [Inject]
        internal IJSRuntime JavaScriptRuntime { get; set; }


        public Inventory()
        {
            Actions = new ObservableCollection<InventoryAction>();
            Actions.CollectionChanged += OnActionsCollectionChanged;
            ListNumberOfItemsByIndex = new List<int>(InventoryPage.NBMAXITEM);
            for (int i=0; i < InventoryPage.NBMAXITEM; i++)
            {
                ListNumberOfItemsByIndex.Add(0);
            }
        }

        private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            JavaScriptRuntime.InvokeVoidAsync("Crafting.AddActions", e.NewItems);
        }

        public void AddItem(Item item, int index)
        {
            Items.Insert(index, item);
        }

        public void DeleteItem(int index)
        {
            Items.Insert(index, new Item());
        }
    }
}
