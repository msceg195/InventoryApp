using DroidInventory_MSC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace DroidInventory_MSC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmMaterialIssueVouchers_Grage : ContentPage
    {
        List<SC_MaterialIssueRequesitionsDetails> MaterialRequesitionsDetailsList;
        string StoreName, UserID, IssueRequesitionName;
        int StoreID, IssueRequesitionID;

        public frmMaterialIssueVouchers_Grage()
        {
            InitializeComponent();
            UserID = (Application.Current as App).UserID;
        }


        private void btnIssueRequesition_Clicked(object sender, EventArgs e)
        {
            var _page = new MaterialIssueRequesitionsPage();
            _page.SharedMaterialRequesitionsList.ItemSelected += (source, args) =>
            {
                var item = args.SelectedItem;
                MaterialIssueRequesitions _s = args.SelectedItem as MaterialIssueRequesitions;
                lblIssueRequesitionName.Text = _s.RName;
                IssueRequesitionName = _s.RName;
                IssueRequesitionID = _s.RequesitionID;
                GetMaterialRequesitionsDetails(_s.RequesitionID);
                Navigation.PopAsync();
            };
            Navigation.PushAsync(_page);
        }

        private async void GetMaterialRequesitionsDetails(int RequesitionID)
        {

            string url = (Application.Current as App).APIUrl + "SC_MaterialIssueRequesitions?id=" + RequesitionID.ToString();
            HttpClient _client = new HttpClient();
            var res = await _client.GetStringAsync(url);
            MaterialRequesitionsDetailsList = JsonConvert.DeserializeObject<List<SC_MaterialIssueRequesitionsDetails>>(res);

        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {

            txtQty.Text = e.NewValue.ToString();
        }

        private void btnStore_Clicked(object sender, EventArgs e)
        {
            var _page = new StorePage();
            _page.SharedStoreList.ItemSelected += (source, args) =>
            {
                var item = args.SelectedItem;
                Stores _s = args.SelectedItem as Stores;
                lblStoreName.Text = _s.StoreName;
                StoreName = _s.StoreName;
                StoreID = _s.StoreID;
                Navigation.PopAsync();
            };
            Navigation.PushAsync(_page);
        }
        private async void btnScan_Clicked(object sender, EventArgs e)
        {
            #region way1
            //var scanPage = new ZXingScannerPage();
            //scanPage.OnScanResult += (result) =>
            //{
            //    scanPage.IsScanning = false;
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        Navigation.PopAsync();
            //        lblItemName.Text = result.Text;
            //    });
            //};

            //await Navigation.PushAsync(scanPage);
            #endregion

            #region way 2
            //var scanPage = new ZXingScannerPage();
            //var customOverlay = new StackLayout
            //{
            //    HorizontalOptions = LayoutOptions.Start,
            //    VerticalOptions = LayoutOptions.Start
            //};

            //var torch = new Button
            //{
            //    Text = "Torch"
            //};
            //torch.Clicked += delegate
            //{
            //    scanPage.ToggleTorch();
            //};
            //customOverlay.Children.Add(torch);

            //scanPage = new ZXingScannerPage(new ZXing.Mobile.MobileBarcodeScanningOptions { AutoRotate = true }, customOverlay: customOverlay);
            //scanPage.HeightRequest = 300;
            //scanPage.OnScanResult += (result) =>
            //{
            //    scanPage.IsScanning = false;

            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        Navigation.PopAsync();
            //        lblItemName.Text = result.Text;
            //    });
            //};
            //await Navigation.PushAsync(scanPage);
            #endregion

            #region way 3
            //var scanPage = new ZXingScannerPage();
            //// Navigate to our scanner page
            //await Navigation.PushAsync(scanPage);
            //scanPage.OnScanResult += (result) =>
            //{
            //    // Stop scanning
            //    scanPage.IsScanning = false;

            //    // Pop the page and show the result
            //    Device.BeginInvokeOnMainThread(async () =>
            //    {
            //        await Navigation.PopAsync();
            //        await DisplayAlert("Scanned Barcode", result.Text, "OK");
            //    });
            //};
            #endregion



            //setup options
            var options = new MobileBarcodeScanningOptions
            {

                AutoRotate = false,
                TryHarder = true,
                PossibleFormats = new List<ZXing.BarcodeFormat> { ZXing.BarcodeFormat.CODE_128 }
            };
            // add options and customize page
            var scanPage = new ZXingScannerPage(options)
            {
                DefaultOverlayTopText = "Align the barcode within the frame",
                DefaultOverlayBottomText = string.Empty,
                DefaultOverlayShowFlashButton = true
            };
            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);
            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    lblItemName.Text = result.Text;

                });
            };

        }




        


    }
}