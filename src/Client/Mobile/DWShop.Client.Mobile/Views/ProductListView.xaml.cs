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
}