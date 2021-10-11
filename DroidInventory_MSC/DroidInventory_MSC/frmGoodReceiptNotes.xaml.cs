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
using ZXing.Net.Mobile.Forms;

namespace DroidInventory_MSC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmGoodReceiptNotes : ContentPage
    {
        int StoreID = 0;
        string StoreName, UserID;
        public frmGoodReceiptNotes()
        {
            InitializeComponent();


            UserID = (Application.Current as App).UserID;
            StoreID = (Application.Current as App).StoreID != "0" ? Convert.ToInt32((Application.Current as App).StoreID.Split('^')[0]) : 0;
            StoreName = (Application.Current as App).StoreID != "0" ? (Application.Current as App).StoreID.Split('^')[1] : "";
            lblStoreName.Text = StoreName;

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

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {

            txtQty.Text = e.NewValue.ToString();
        }

        private async void btnAddtoList_Clicked(object sender, EventArgs e)

        {

            if (string.IsNullOrWhiteSpace(lblItemName.Text.Trim()))
            {
                await DisplayAlert("Warning", "No Item Found !!!", "OK");
                return;
            }


            if (string.IsNullOrWhiteSpace(StoreName))
            {
                await DisplayAlert("Warning", "No Store Found !!!", "OK");
                return;
            }



            string ItemName;
            string InnerCode;
            string SealPrefix;
            string CASESize;
            GETProperites(lblItemName.Text.Trim(), out ItemName, out InnerCode, out SealPrefix, out CASESize);

            if (string.IsNullOrWhiteSpace(InnerCode) || string.IsNullOrWhiteSpace(ItemName) || string.IsNullOrWhiteSpace(SealPrefix) || string.IsNullOrWhiteSpace(CASESize))
            {
                await DisplayAlert("Warning", "Incomplete Data !!!", "OK");
                lblItemName.Text = "";
                return;
            }



            string url = string.Format((Application.Current as App).APIUrl + "I_Items?StoreID={0}&ItemName={1}&InnerCode={2}&SealPrefix={3}&CASESize={4}&UserID={5}", StoreID, ItemName.Trim(), InnerCode.Trim(), SealPrefix.Trim(), CASESize, UserID);
            HttpClient _client = new HttpClient();
            var Res = await _client.GetStringAsync(url);
            string Obsusers = JsonConvert.DeserializeObject<string>(Res);
            await DisplayAlert("Warning", Obsusers, "OK");
            if (Obsusers.ToLower() == "success")
                lblItemName.Text = "";


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

        private void GETProperites(string QRCode, out string ItemName, out string InnerCode, out string SealPrefix, out string CASESize)
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



            QRCode = QRCode.Replace("     ", " ").Replace("    ", " ").Replace("   ", " ").Replace("  ", " ").Trim();
            int CASENO_Index = QRCode.LastIndexOf("CASE NO:") + "CASE NO:".Length;
            int SEALNO_Index = QRCode.LastIndexOf("SEAL NO:") + "SEAL NO:".Length;
            int QTY_Index = QRCode.LastIndexOf("QTY:") + "QTY:".Length;
            int PCS_Index = QRCode.LastIndexOf("PCS") + "PCS".Length;
            CASESize = QRCode.Substring(QTY_Index, QRCode.LastIndexOf("PCS") - QTY_Index).Trim();
            InnerCode = QRCode.Substring(CASENO_Index, QRCode.LastIndexOf("SEAL NO:") - CASENO_Index).Trim();
            ItemName = "CASE NO:" + QRCode.Substring(CASENO_Index, QRCode.LastIndexOf("SEAL NO:") - CASENO_Index).Trim();
            SealPrefix = QRCode.Substring(QRCode.Length - 23, 11).Replace(" ", "").Trim();


        }
    }
}