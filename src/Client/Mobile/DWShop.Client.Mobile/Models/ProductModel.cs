using DWShop.Client.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Client.Mobile.Models
{
    public class ProductModel: ObservableObject
    {
        private int id;
        public int Id { get => id; set => SetProperty(ref id, value); }


        private string productName;
        public string ProductName { get => productName; set => SetProperty(ref productName, value); }

        private string phothoUrl;
        public string PhothoUrl { get => phothoUrl; set => SetProperty(ref phothoUrl, value); }

        private decimal price;
        public decimal Price { get => price; set => SetProperty(ref price, value); }
    }
}
