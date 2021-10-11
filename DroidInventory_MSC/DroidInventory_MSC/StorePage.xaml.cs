using DroidInventory_MSC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DroidInventory_MSC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorePage : ContentPage
    {
        
        
        private ObservableCollection<Stores> ObsStores;

        public ListView SharedStoreList { get { return listStores; } }

        public StorePage()
        {
            InitializeComponent();

            GetStores();
        }

        private async void GetStores()
        {

            string url = (Application.Current as App).APIUrl + "store";
            HttpClient _client = new HttpClient();
            var res = await _client.GetStringAsync(url);
            var StoreList = JsonConvert.DeserializeObject<List<Stores>>(res);
            ObsStores = new ObservableCollection<Stores>(StoreList);
            listStores.ItemsSource = ObsStores;
        }



    }
}