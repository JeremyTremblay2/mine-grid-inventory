using Blazor_PerretTremblay.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Blazor_PerretTremblay.Components
{
    public partial class Inventory
    {
        private Item _recipeResult;
       
        public ObservableCollection<InventoryAction> Actions { get; set; }

        public Item? CurrentDragItem { get; set; }

        [Parameter]
        public List<Item> Items { get; set; }


        /// <summary>
        /// Gets or sets the java script runtime.
        /// </summary>
        [Inject]
        internal IJSRuntime JavaScriptRuntime { get; set; }


        public Inventory()
        {
            Actions = new ObservableCollection<InventoryAction>();
            Actions.CollectionChanged += OnActionsCollectionChanged;
        }

        private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            JavaScriptRuntime.InvokeVoidAsync("Crafting.AddActions", e.NewItems);
        }

        public void DeleteItem(Item item)
        {
            Items.Remove(item);
        }
    }
}
