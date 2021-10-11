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
using ZXing.Net.Mobile.Forms;

namespace DroidInventory_MSC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmStoreTransferVouchers : ContentPage
    {
        ObservableCollection<StoreVoucherDetails> _details;
        string ToStoreName, FromStoreName, UserID, IssueRequesitionName;
        int ToStoreID, FromStoreID, IssueRequesitionID;
        decimal IssueRequesitionQty;

        public frmStoreTransferVouchers()
        {
            InitializeComponent();
            UserID = (Application.Current as App).UserID;
            FromStoreID = (Application.Current as App).StoreID != "0" ? Convert.ToInt32((Application.Current as App).StoreID.Split('^')[0]) : 0;
            FromStoreName = (Application.Current as App).StoreID != "0" ? (Application.Current as App).StoreID.Split('^')[1] : "";
            lblFromStoreName.Text = FromStoreName;

            _details = new ObservableCollection<StoreVoucherDetails>();
            listv1.ItemsSource = _details;
        }






        private void btnAddtoList_Clicked(object sender, EventArgs e)

        {
            if (string.IsNullOrWhiteSpace(lblItemName.Text.Trim()))
            {
                DisplayAlert("Warning", "Complete Data", "OK");
                return;
            }


            foreach (var item in _details)
            {
                if (item.ItemName == lblItemName.Text.Trim())
                {
                    DisplayAlert("Warning", "Item already exists", "OK");
                    return;
                }

            }


            _details.Add(new StoreVoucherDetails() { ItemName = lblItemName.Text.Trim() });
            listv1.ItemsSource = _details;
            ListTab1.Title = "Details (" + _details.Count.ToString() + ")";
            lblItemName.Text = "";


        }

        private void btnFromStore_Clicked(object sender, EventArgs e)
        {
            var _page = new StorePage();
            _page.SharedStoreList.ItemSelected += (source, args) =>
            {
                var item = args.SelectedItem;
                Stores _s = args.SelectedItem as Stores;
                lblFromStoreName.Text = _s.StoreName;
                FromStoreName = _s.StoreName;
                FromStoreID = _s.StoreID;
                Navigation.PopAsync();
            };
            Navigation.PushAsync(_page);
        }

        private async void btnAddtoDB_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ToStoreID.ToString()) || string.IsNullOrWhiteSpace(FromStoreID.ToString()))
            {
                await DisplayAlert("Warning", "No Store Found !!!", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(IssueRequesitionID.ToString()) )
            {
                await DisplayAlert("Warning", "No Issue Requesition Found !!!", "OK");
                return;
            }


            ObservableCollection<StoreVoucherDetails> _test = listv1.ItemsSource as ObservableCollection<StoreVoucherDetails>;
            if (_test.Count == 0)
            {
                await DisplayAlert("Warning", "No Items Found !!!", "OK");
                return;
            }

            if (Convert.ToDecimal(_test.Count) != IssueRequesitionQty)
            {
                await DisplayAlert("Warning", "Transfer Qty & Requesition Qty not mach !!!", "OK");
                return;
            }

            string ItemName;
            string ItemNames = ",";
            foreach (var item  in _test)
            {
                GETProperites(item.ItemName.Trim(), out ItemName);
                if (ItemName != "")
                    ItemNames += ItemName.Trim() + ",";
            }

            if (ItemNames == ",")
            {
                await DisplayAlert("Warning", "Incomplete Data !!!", "OK"); 

                return;
            }


            string url = string.Format((Application.Current as App).APIUrl + "ITransfeer?SourceStoreID={0}&DestinationStoreID={1}&ItemNames='{2}'&MaterialIssueRequesitionID={3}&UserID={4}", FromStoreID, ToStoreID, ItemNames, IssueRequesitionID.ToString(), UserID);
            HttpClient _client = new HttpClient();
            var Res = await _client.GetStringAsync(url);
            string Obsusers = JsonConvert.DeserializeObject<string>(Res);
            await DisplayAlert("Warning", Obsusers, "OK");
            if (Obsusers.ToLower() == "success")
            {
                _details = new ObservableCollection<StoreVoucherDetails> { };
                listv1.ItemsSource = _details;
                ListTab1.Title = "Details ( 0 )";
                lblItemName.Text = "";
                lblIssueRequesitionName.Text = "";
                IssueRequesitionName = "";
                IssueRequesitionID = 0;
                IssueRequesitionQty = 0;
            }

        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

            var svd = (sender as MenuItem).CommandParameter as StoreVoucherDetails;
            _details.Remove(svd);
        }

        private void btnToStore_Clicked(object sender, EventArgs e)
        {
            var _page = new StorePage();
            _page.SharedStoreList.ItemSelected += (source, args) =>
            {

                var item = args.SelectedItem;
                Stores _s = args.SelectedItem as Stores;
                lblToStoreName.Text = _s.StoreName;
                ToStoreName = _s.StoreName;
                ToStoreID = _s.StoreID;


                Navigation.PopAsync();
            };
            Navigation.PushAsync(_page);
        }
        private async void btnScan_Clicked(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    lblItemName.Text = result.Text;
                });
            };

            await Navigation.PushAsync(scanPage);
        }


        private void GETProperites(string QRCode, out string ItemName)
        {
            /*
           //QRCode = "MANUFACTURER:Shanghai Jinfan MSC COLOR:LIGHT YELLOW QTY:200PCS CASE NO:0359-1140 SEAL NO:FJ07973401 FJ07973600";
            QRCode = "MANUFACTURER:Shanghai Jinfan MSC COLOR:LIGHT YELLOW  QTY:200PCS  CASE NO:0359-1140  SEAL NO:FJ 07973401 FJ 07973600";
            int CASENO_Index = QRCode.LastIndexOf("CASE NO:") + "CASE NO:".Length;
            int SEALNO_Index = QRCode.LastIndexOf("SEAL NO:") + "SEAL NO:".Length;
            int QTY_Index = QRCode.LastIndexOf("QTY:") + "QTY:".Length;
            int PCS_Index = QRCode.LastIndexOf("PCS") + "PCS".Length;
            CASESize = QRCode.Substring(QTY_Index, QRCode.LastIndexOf("PCS") - QTY_Index);
            InnerCode = QRCode.Substring(CASENO_Index, QRCode.LastIndexOf("SEAL NO:") - CASENO_Index);
            ItemName = "CASE NO:" + QRCode.Substring(CASENO_Index, QRCode.LastIndexOf("SEAL NO:") - CASENO_Index);
            SealPrefix = QRCode.Substring(SEALNO_Index, QRCode.LastIndexOf(" ") - SEALNO_Index);
            */



            QRCode = QRCode.Replace("     ", " ").Replace("    ", " ").Replace("   ", " ").Replace("  ", " ");
            int CASENO_Index = QRCode.LastIndexOf("CASE NO:") + "CASE NO:".Length;
            int SEALNO_Index = QRCode.LastIndexOf("SEAL NO:") + "SEAL NO:".Length;
            int QTY_Index = QRCode.LastIndexOf("QTY:") + "QTY:".Length;
            int PCS_Index = QRCode.LastIndexOf("PCS") + "PCS".Length;
            ItemName = "CASE NO:" + QRCode.Substring(CASENO_Index, QRCode.LastIndexOf("SEAL NO:") - CASENO_Index);



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
                IssueRequesitionQty = _s.TQty;
                Navigation.PopAsync();
            };
            Navigation.PushAsync(_page);
        }



    }
}