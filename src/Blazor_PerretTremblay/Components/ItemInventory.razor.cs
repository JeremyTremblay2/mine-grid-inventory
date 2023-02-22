using Blazor_PerretTremblay.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor_PerretTremblay.Components
{
    public partial class ItemInventory
    {
        [Parameter]
        public int Index { get; set; }

        private Item _item;

        [Parameter]
        public Item Item
        {
            get => _item;
            set
            {
                if (value == null)
                {
                    _item = new Item();
                }
            }
        }

        [Parameter]
        public bool NoDrop { get; set; }

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

        }

        internal void OnDrop()
        {
            if (NoDrop)
            {
                return;
            }
        }

        private void OnDragStart()
        {
        }
    }
}
