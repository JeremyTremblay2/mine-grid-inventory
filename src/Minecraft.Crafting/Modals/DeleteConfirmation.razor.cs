using Minecraft.Crafting.Models;
using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Minecraft.Crafting.Services.DataItemsService;
using Minecraft.Crafting.Api.Models;

namespace Minecraft.Crafting.Modals
{
    public partial class DeleteConfirmation
    {
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Inject]
        public IDataItemsService DataService { get; set; }

        [Parameter]
        public int Id { get; set; }

        private Item item = new Item();

        protected override async Task OnInitializedAsync()
        {
            // Get the item
            item = await DataService.GetById(Id);
        }

        void ConfirmDelete()
        {
            ModalInstance.CloseAsync(ModalResult.Ok(true));
        }

        void Cancel()
        {
            ModalInstance.CancelAsync();
        }
    }
}
