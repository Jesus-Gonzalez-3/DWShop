using DWShop.Application.Features.Catalog.Commands.Delete;
using DWShop.Client.Infrastructure.Managers.Products.Edit;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DWShop.Web.Client.Pages.Products
{
    public partial class DeleteProduct
    {
        [CascadingParameter]
        private MudDialogInstance DialogInstance { get; set; }

        [Parameter]
        public DeleteCatalogCommand DeleteProductCommand { get; set; }
        [Inject]
        public IAddEditDeleteProductManager ProductManager { get; set; }

        private async Task DeleteAsync()
        {
            var response = await ProductManager.DeleteProduct(DeleteProductCommand);
            if (response.Succeded)
            {
                _snackBar.Add("Registro Eliminado correctamente", Severity.Success);
                DialogInstance.Close();
            }
            else
                foreach (var message in response.Messages)
                    _snackBar.Add(message, Severity.Error);

        }
    }
}
