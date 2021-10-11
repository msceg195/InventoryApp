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
    public partial class MaterialIssueRequesitionsPage : ContentPage
    {
        private ObservableCollection<MaterialIssueRequesitions> ObsMaterialRequesitions;

        public ListView SharedMaterialRequesitionsList { get { return listMaterialRequesitions; } }
        public MaterialIssueRequesitionsPage()
        {
            InitializeComponent();
            GetMaterialRequesitions();
        }

        private async void GetMaterialRequesitions()
        {

            string url = (Application.Current as App).APIUrl + "SC_MaterialIssueRequesitions";
            HttpClient _client = new HttpClient();
            var res = await _client.GetStringAsync(url);
            var MaterialRequesitionsList = JsonConvert.DeserializeObject<List<MaterialIssueRequesitions>>(res);
            ObsMaterialRequesitions = new ObservableCollection<MaterialIssueRequesitions>(MaterialRequesitionsList);
            listMaterialRequesitions.ItemsSource = ObsMaterialRequesitions;
        }
    }
}