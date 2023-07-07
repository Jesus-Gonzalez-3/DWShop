namespace DWShop.Client.Mobile.Views
{
    public class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage(ProductListView productList, BasketView basketView)
        {
            Children.Add(productList);
            Children.Add(basketView);
            Children.Add(basketView);
        }
    }
}
