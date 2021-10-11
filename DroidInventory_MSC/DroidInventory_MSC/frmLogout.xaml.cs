using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DroidInventory_MSC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmLogout : ContentPage
    {
        public frmLogout()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Application.Current.MainPage = new NavigationPage(new frmLogin());
        }

    }
}