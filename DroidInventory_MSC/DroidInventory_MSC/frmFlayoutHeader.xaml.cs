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
    public partial class frmFlayoutHeader : ContentView
    {
        public frmFlayoutHeader()
        {
            InitializeComponent();
            lblWelcome.Text = "Welcome "+Environment.NewLine + (Application.Current as App).UserName;
        }
    }
}