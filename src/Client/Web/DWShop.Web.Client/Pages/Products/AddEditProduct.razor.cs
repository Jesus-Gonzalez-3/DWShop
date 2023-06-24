using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Application.Features.Catalog.Commands.Delete;
using DWShop.Application.Features.Catalog.Commands.Update;
using DWShop.Client.Infrastructure.Managers.Products.Edit;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DWShop.Web.Client.Pages.Products
{
    public partial class AddEditProduct
    {
        [CascadingParameter]
        private MudDialogInstance DialogInstance { get; set; }

        [Parameter]
        public UpdateCatalogCommand EditProductCommand { get; set; } = new();


        [Inject]
        public IAddEditDeleteProductManager ProductManager { get; set; }
        private async Task UpdateAsync()
        {
            if (EditProductCommand.Id != 0)
            {
                var response = await ProductManager.EditProduct(EditProductCommand);
                if (response.Succeded)
                {
                    _snackBar.Add("Registro editado correctamente", Severity.Success);
                    DialogInstance.Close();
                }
                else
                    foreach (var message in response.Messages)
                        _snackBar.Add(message, Severity.Error);
            }
            else
            {
                var AddProductCommand = new CreateCatalogCommand()
                {
                    Name = EditProductCommand.Name,
                    Category = EditProductCommand.Category,
                    Description = EditProductCommand.Description,
                    Summary = EditProductCommand.Summary,
                    PhotoURL = EditProductCommand.PhotoURL,
                    Price = EditProductCommand.Price
                };
                var response = await ProductManager.AddProduct(AddProductCommand);
                if (response.Succeded)
                {
                    _snackBar.Add("Registro insertado correctamente", Severity.Success);
                    DialogInstance.Close();
                }
                else
                    foreach (var message in response.Messages)
                        _snackBar.Add(message, Severity.Error);
            }
        }
    }

}
