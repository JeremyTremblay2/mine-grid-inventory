using Minecraft.Crafting.Models;
using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Minecraft.Crafting.Services.DataItemsService;
using Minecraft.Crafting.Api.Models;
using System;

namespace Minecraft.Crafting.Modals
{
    /// <summary>
    /// A Blazor component for confirming deletion of an item.
    /// </summary>
    public partial class DeleteConfirmation
    {
        /// <summary>
        /// A modal instance.
        /// </summary>
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        /// <summary>
        /// The data service.
        /// </summary>
        [Inject]
        public IDataItemsService DataService { get; set; }

        /// <summary>
        /// The item's ID.
        /// </summary>
        [Parameter]
        public int Id { get; set; }

        /// <summary>
        /// The item deleted.
        /// </summary>
        private Item item = new Item();

        /// <summary>
        /// Initializes the component and gets the item to be deleted.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            item = await DataService.GetById(Id);
        }

        /// <summary>
        /// Confirms the deletion of the item and closes the modal.
        /// </summary>
        void ConfirmDelete()
        {
            ModalInstance.CloseAsync(ModalResult.Ok(true));
        }

        /// <summary>
        /// Cancels the deletion and closes the modal.
        /// </summary>
        void Cancel()
        {
            ModalInstance.CancelAsync();
        }
    }
}
