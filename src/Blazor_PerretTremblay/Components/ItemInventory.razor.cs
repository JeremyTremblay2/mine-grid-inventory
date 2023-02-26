using Blazor_PerretTremblay.Models;
using Blazor_PerretTremblay.Services.DataInventoryService;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Blazor_PerretTremblay.Components
{
    public partial class ItemInventory
    {
        [Parameter]
        public int Number { get; set; } = 0;

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

        [CascadingParameter]
        public Inventory Parent { get; set; }

        internal void OnDragLeave()
        {
            if (Parent.IsDragBetweenListAndInventory || (Item != null && !this.Item.Equals(Parent.CurrentDragItem)))
            {
                return;
            }

            Item = null;
            Number = 0;
            Parent.DeleteItem(Index);
        }

        internal async void OnDrop()
        {
            if(this.Item == null)
            {
                Item = Parent.CurrentDragItem;
                Parent.AddItem(Item, Index);
                if (Parent.CurrentIndexOfCurrentDragItem < 0)
                    Number = 1;
                else
                {
                    Number += Parent.ListNumberOfItemsByIndex.ElementAt(Parent.CurrentIndexOfCurrentDragItem);
                    Parent.ListNumberOfItemsByIndex.Insert(Parent.CurrentIndexOfCurrentDragItem, 0);
                }
            }
            else
            {
                if(this.Item.Id == Parent.CurrentDragItem?.Id)
                {
                    if (Parent.CurrentIndexOfCurrentDragItem < 0)
                        Number++;
                    else
                    {
                        Number += Parent.ListNumberOfItemsByIndex.ElementAt(Parent.CurrentIndexOfCurrentDragItem);
                        Parent.ListNumberOfItemsByIndex.Insert(Parent.CurrentIndexOfCurrentDragItem, 0);
                    }
                }
            }

            Parent.ListNumberOfItemsByIndex.Insert(Index, Number);
            await Parent.SaveInventory();
        }

        private void OnDragStart()
        {
            Parent.CurrentDragItem = this.Item;
            Parent.IsDragBetweenInventoryAndInventory = true;
            Parent.IsDragBetweenListAndInventory = false;
            Parent.CurrentIndexOfCurrentDragItem = Index;
        }
    }
}
