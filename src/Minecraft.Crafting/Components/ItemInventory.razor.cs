using Microsoft.AspNetCore.Components;
using Minecraft.Crafting.Api.Models;
using Item = Minecraft.Crafting.Api.Models.Item;

namespace Minecraft.Crafting.Components
{
    public partial class ItemInventory
    {
        [Parameter]
        public int Index { get; set; }
        
        public InventoryModel InventoryModel
        {
            get => Parent.InventoryModels.ElementAt(Index);
        }

        [CascadingParameter]
        public Inventory Parent { get; set; }

        internal async void OnDrop()
        {
            Parent.Actions.Add(new InventoryAction { Action = "On start", Item = Parent.CurrentDragItem.Name, Index = this.Index });
            Parent.IsDropped = true;
            if (InventoryModel.ItemName == null)
            {
                InventoryModel.ItemName = Parent.CurrentDragItem.DisplayName;
                if (Parent.CurrentIndexOfCurrentDragItem < 0)
                    InventoryModel.NumberItem = 1;
                else
                {
                    InventoryModel.NumberItem += Parent.InventoryModels.ElementAt(Parent.CurrentIndexOfCurrentDragItem).NumberItem;
                    Parent.DeleteOlderItemInventory();
                }
            }
            else
            {
                if(InventoryModel.ItemName.Equals(Parent.CurrentDragItem.DisplayName))
                {
                    if (Parent.CurrentIndexOfCurrentDragItem < 0)
                        InventoryModel.NumberItem++;
                    else
                    {
                        InventoryModel.NumberItem += Parent.InventoryModels.ElementAt(Parent.CurrentIndexOfCurrentDragItem).NumberItem;
                        Parent.DeleteOlderItemInventory();
                    }
                }
            }

            //await Parent.SaveInventory();
        }

        private void OnDragStart()
        {
            Parent.CurrentDragItem = new Item() { DisplayName = InventoryModel.ItemName };
            Parent.CurrentIndexOfCurrentDragItem = Index;
            Parent.IsDragBetweenInventoryAndInventory = true;
            Parent.IsDragBetweenListAndInventory = false;

            Parent.Actions.Add(new InventoryAction { Action = "On drag start", Item = Parent.CurrentDragItem.Name, Index = this.Index });
        }

        private void OnDragEnd()
        {
            Parent.Actions.Add(new InventoryAction { Action = "On drag end", Item = Parent.CurrentDragItem.Name, Index = this.Index });
            if (!Parent.IsDropped)
            {
                Parent.DeleteOlderItemInventory();
            }
            Parent.IsDropped = false;
            StateHasChanged();
        }
    }
}
