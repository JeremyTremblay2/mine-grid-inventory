using Microsoft.AspNetCore.Components;
using Minecraft.Crafting.Api.Models;
using System;
using Item = Minecraft.Crafting.Api.Models.Item;

namespace Minecraft.Crafting.Components
{
    /// <summary>
    /// Represents a single slot in the inventory.
    /// </summary>
    public partial class ItemInventory
    {
        /// <summary>
        /// The index of this item in the inventory.
        /// </summary>
        [Parameter]
        public int Index { get; set; }

        /// <summary>
        /// The inventory model for this item.
        /// </summary>
        public InventoryModel InventoryModel
        {
            get => Parent.InventoryModels.ElementAt(Index);
        }

        /// <summary>
        /// The parent inventory of this item.
        /// </summary>
        [CascadingParameter]
        public Inventory Parent { get; set; }

        /// <summary>
        /// Called when the user drops an item onto this slot.
        /// </summary>
        internal async void OnDrop()
        {
            Parent.Actions.Add(new InventoryAction { Action = "On start", Item = Parent.CurrentDragItem.Name, Index = this.Index });
            Parent.IsDropped = true;
            if (InventoryModel.ItemName == null)
            {
                InventoryModel.ItemName = Parent.CurrentDragItem.DisplayName;
                InventoryModel.ImageBase64 = Parent.CurrentDragItem.ImageBase64;
                if (Parent.CurrentIndexOfCurrentDragItem < 0)
                {
                    InventoryModel.NumberItem = 1;
                    await Parent.UpdateItemInventory(InventoryModel);
                }
                else
                {
                    InventoryModel.NumberItem += Parent.InventoryModels.ElementAt(Parent.CurrentIndexOfCurrentDragItem).NumberItem;
                    await Parent.UpdateItemInventory(InventoryModel);
                    await Parent.DeleteOlderItemInventory();
                }
            }
            else
            {
                if (InventoryModel.ItemName.Equals(Parent.CurrentDragItem.DisplayName))
                {
                    if (Parent.CurrentIndexOfCurrentDragItem < 0)
                    {
                        InventoryModel.NumberItem++;
                        await Parent.UpdateItemInventory(InventoryModel);
                    }
                    else
                    {
                        InventoryModel.NumberItem += Parent.InventoryModels.ElementAt(Parent.CurrentIndexOfCurrentDragItem).NumberItem;
                        await Parent.UpdateItemInventory(InventoryModel);
                        await Parent.DeleteOlderItemInventory();
                    }
                }
            }

            

            //await Parent.SaveInventory();
        }

        /// <summary>
        /// Called when the user starts dragging an item from this slot.
        /// </summary>
        private void OnDragStart()
        {
            Parent.CurrentDragItem = new Item() { DisplayName = InventoryModel.ItemName };
            Parent.CurrentIndexOfCurrentDragItem = Index;
            Parent.IsDragBetweenInventoryAndInventory = true;
            Parent.IsDragBetweenListAndInventory = false;

            Parent.Actions.Add(new InventoryAction { Action = "On drag start", Item = Parent.CurrentDragItem.Name, Index = this.Index });
        }

        /// <summary>
        /// Called when the user finishes dragging an item from this slot.
        /// </summary>
        private async void OnDragEnd()
        {
            Parent.Actions.Add(new InventoryAction { Action = "On drag end", Item = Parent.CurrentDragItem.Name, Index = this.Index });
            if (!Parent.IsDropped)
            {
                await Parent.DeleteOlderItemInventory();
            }
            Parent.IsDropped = false;
            StateHasChanged();
        }
    }
}
