using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels;

namespace DWShop.Client.Mobile.Views;

public partial class ProductListView : ContentPage
{
	public ProductListView( ProductListViewModel productListViewModel)
	{
		InitializeComponent();
		BindingContext = productListViewModel;
		productListViewModel.Navigation = Navigation;
	}

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		if(e.Item is not null && BindingContext is ProductListViewModel viewModel)
		{
			var selectedItem = e.Item as ProductModel;
			viewModel.DetailCommand.Execute(selectedItem);
		}
    }
}