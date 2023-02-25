using Blazor_PerretTremblay.Models;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Blazor_PerretTremblay.Components
{
    public partial class ItemInventory
    {
        private int Number = 0;

        [Parameter]
        public int Index { get; set; }

        private Item? _item;
        
        [Parameter]
        public Item? Item
        {
            get => _item;
            set
            {
                _item = value;
            }
        }

        [Parameter]
        public bool NoDrop { get; set; }

        [CascadingParameter]
        public Inventory Parent { get; set; }

        internal void OnDragEnter()
        {
            if (NoDrop)
            {
                return;
            }
        }

        internal void OnDragLeave()
        {
            if (NoDrop)
            {
                return;
            }

            Parent.DeleteItem(Item);
        }

        internal void OnDrop()
        {
            if (NoDrop)
            {
                return;
            }
            
            if(this.Item == null)
            {
                Item = Parent.CurrentDragItem;
            }
            else
            {
                if(this.Item.Id == Parent.CurrentDragItem?.Id)
                {
                    Number++;
                }
            }
        }

        private void OnDragStart()
        {
            Parent.CurrentDragItem = this.Item;
        }
    }
}
