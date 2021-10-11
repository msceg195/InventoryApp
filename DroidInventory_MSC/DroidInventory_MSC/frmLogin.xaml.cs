using Inventory_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DroidInventory_MSC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmLogin : ContentPage
    {

        public frmLogin()
        {
            InitializeComponent();
            var app = Application.Current as App;
            if (app.IsRemember)
            {
                txtUserName.Text = app.UserName;
                txtPWD.Text = app.UserPWD;
                chkRMe.IsChecked = true;
            }
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            lblMSG.Text = "";
            string Username = Convert.ToString(txtUserName.Text);
            string Password = Convert.ToString(txtPWD.Text);
            if (string.IsNullOrWhiteSpace(Username))
            {
                lblMSG.Text = "Username is required";
                return;
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                lblMSG.Text = "Password is required";
                return;
            }

            string url = string.Format((Application.Current as App).APIUrl + "Sec_Users?UN={0}&PWD={1}&CN={2}", Username, Password);
            HttpClient _client = new HttpClient();
            var Res = await _client.GetStringAsync(url);
            List<Sec_Users> Obsusers = JsonConvert.DeserializeObject<List<Sec_Users>>(Res);
            if (int.Parse(Obsusers.Count.ToString()) == 1)
            {
                #region APPProperties
                var app = Application.Current as App;
                app.UserID = Obsusers[0].ID.ToString();
                app.UserName = Obsusers[0].UserName;
                app.UserPWD = Password;
                app.StoreID = Obsusers[0].StoreID;
                app.IsGoodReceipt = Obsusers[0].IsGoodReceipt;
                app.IsMaterialIssue = Obsusers[0].IsMaterialIssue;
                app.IsStoreTransfer = Obsusers[0].IsStoreTransfer;
                app.IsRemember = chkRMe.IsChecked;
                await Application.Current.SavePropertiesAsync();
                if (Obsusers[0].IsGoodReceipt == "0" && Obsusers[0].IsMaterialIssue == "0" && Obsusers[0].IsStoreTransfer == "0")
                {
                    lblMSG.Text = "No privileges ,Please contact IT admin";
                    app.UserName = "";
                    app.UserPWD = "";
                    return;
                }

                #endregion
                Application.Current.MainPage = new frmMain();
            }
            else
            {
                lblMSG.Text = "Invalid Username or Password";
                return;
            }




        }


    }
}