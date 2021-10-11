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
    public partial class frmMain : Shell
    {
        public frmMain()
        {

            

            InitializeComponent();
            
            if ((Application.Current as App).IsGoodReceipt != "0")
            {
                ShellContent SC_GoodReceiptNotes = new ShellContent
                {
                    Title = "Good Receipt Notes",
                    Icon = "plus.png",
                    IsTabStop = true,
                    Content = new frmGoodReceiptNotes()
                };
                FItems1.Items.Add(SC_GoodReceiptNotes);
            }


            if ((Application.Current as App).IsStoreTransfer != "0")
            {
                ShellContent SC_StoreTransfeerVoucher = new ShellContent
                {
                    Title = "Store Transfeer Voucher",
                    Icon = "swap_ico.png",
                    IsTabStop = true,
                    Content = new frmStoreTransferVouchers()
                };
                FItems1.Items.Add(SC_StoreTransfeerVoucher);
            }

            if ((Application.Current as App).IsMaterialIssue != "0")
            {
                ShellContent SC_MaterialIssueVouchers = new ShellContent
                {
                    Title = "Material Issue Voucher",
                    Icon = "minus.png",
                    IsTabStop = true,
                    Content = new frmMaterialIssueVouchers()
                };
                FItems1.Items.Add(SC_MaterialIssueVouchers);
            }




            ShellSection SS_LogOut = new ShellSection
            {
                Title = "Log Out",
                Icon = "dissatisfied.png",
                IsTabStop = true,
            };

            SS_LogOut.Items.Add(new ShellContent()
            {
                Content = new frmLogout()
            });
            myshell.Items.Add(SS_LogOut);




            //this.CurrentItem.CurrentItem = xxx;

            // Shell.SetTabBarIsVisible(LogOuttab, true);

            //if ((Application.Current as App).IsGoodReceipt != "0")
            //{
            //    this.CurrentItem.CurrentItem = GoodReceipttab;
            //}
            //else if ((Application.Current as App).IsStoreTransfer != "0")
            //{
            //    this.CurrentItem.CurrentItem = StoreTransfeertab;
            //}
            //else if ((Application.Current as App).IsMaterialIssue != "0")
            //{
            //    this.CurrentItem.CurrentItem = MaterialIssuetab;

            //}
            //else
            //{
            //    this.CurrentItem.CurrentItem = LogOuttab;
            //    Application.Current.MainPage = new NavigationPage(new frmLogin());
            //    return;
            //}


            //DisabledColor = "Brown"
            //UnselectedColor = "Blue"
        }

        //  private void Shell_Navigating(object sender, ShellNavigatingEventArgs e)
        // {

        //if (e.Target != null)
        // {


        //  var s = e.Target.Location.ToString();

        //if (s.Contains("StoreTransfeerVoucher"))
        //{
        //    if ((Application.Current as App).IsStoreTransfer == "0")
        //    {
        //        Shell.SetTabBarIsVisible(this, false);

        //        e.Cancel();
        //    }
        //    else
        //    {
        //        Shell.SetTabBarIsVisible(this, true);
        //    }

        //}

        //if (s.Contains("MaterialIssueVoucher"))
        //{
        //    if ((Application.Current as App).IsMaterialIssue == "0")
        //    {
        //        Shell.SetTabBarIsVisible(this, false);
        //        e.Cancel();
        //    }
        //    else
        //    {
        //        Shell.SetTabBarIsVisible(this, true);
        //    }

        //}

        //if (s.Contains("GoodReceiptNotes"))
        //{
        //    if ((Application.Current as App).IsGoodReceipt == "0")
        //    {
        //        Shell.SetTabBarIsVisible(this, false);
        //        e.Cancel();
        //    }
        //    else
        //    {
        //        Shell.SetTabBarIsVisible(this, true);
        //    }

        //}

        // }
        //}
    }
}



//   <Tab x:Name="GoodReceipttab" Route = "GoodReceiptNotes"  Title="Good Receipt Notes"     Icon="plus.png">
//        <ShellContent x:Name="xxx"  IsTabStop="True" ContentTemplate="{DataTemplate frm:frmGoodReceiptNotes}"  />
//    </Tab>
//    <Tab x:Name="StoreTransfeertab" Route = "StoreTransfeerVoucher" Title="Store Transfeer Voucher"  Icon="swap_ico.png" >
//        <ShellContent IsTabStop = "True" ContentTemplate="{DataTemplate frm:frmStoreTransferVouchers}"  />
//    </Tab>
//    <Tab x:Name="MaterialIssuetab" Route = "MaterialIssueVoucher" Title="Material Issue Voucher"  Icon="minus.png" >
//        <ShellContent IsTabStop = "True" ContentTemplate="{DataTemplate frm:frmMaterialIssueVouchers}"  />
//    </Tab>

//</FlyoutItem>
//<Tab x:Name="LogOuttab" Route = "LogOut" Title="Log Out"     Icon="dissatisfied.png" >
//    <ShellContent IsTabStop = "True"   ContentTemplate="{DataTemplate frm:frmLogout}"      />
//</Tab>