using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DroidInventory_MSC
{
    public partial class App : Application
    {

        private const string _url = "http://10.72.240.3:1981/api/";
       // private const string _url = "http://192.168.43.195/API_Inventory/api/";
        public string APIUrl { get { return _url; } }

        /*

        // private const string _ConName = "ConnectionString";
        private const string _ConName = "ConnectionString_Garage";
        public string ConnectionName { get { return _ConName; } }
        */
        #region MyRegion
        private const string RemeberKey = "IsRememberMe";
        private const string userIdKey = "UserID";
        private const string userNameKey = "UserName";
        private const string userPWDKey = "PWD";
        private const string StoreIDKey = "StoreID";
        private const string IsMaterialIssueKey = "IsMaterialIssue";
        private const string IsStoreTransferKey = "IsStoreTransfer";
        private const string IsGoodReceiptKey = "IsGoodReceipt";
        public string UserPWD
        {
            get
            {
                if (Properties.ContainsKey(userPWDKey))
                    return Properties[userPWDKey].ToString();
                else
                    return "";

            }
            set
            {
                Properties[userPWDKey] = value;
            }
        }
        public bool IsRemember
        {
            get
            {
                if (Properties.ContainsKey(RemeberKey))
                    return Convert.ToBoolean(Properties[RemeberKey].ToString());
                else
                    return false;

            }
            set
            {
                Properties[RemeberKey] = value;
            }
        }
        public string UserID
        {
            get
            {
                if (Properties.ContainsKey(userIdKey))
                    return Properties[userIdKey].ToString();
                else
                    return "";

            }
            set
            {
                Properties[userIdKey] = value;
            }
        }
        public string UserName
        {
            get
            {
                if (Properties.ContainsKey(userNameKey))
                    return Properties[userNameKey].ToString();
                else
                    return "";

            }
            set
            {
                Properties[userNameKey] = value;
            }
        }
        public string StoreID
        {
            get
            {
                if (Properties.ContainsKey(StoreIDKey))
                    return Properties[StoreIDKey].ToString();
                else
                    return "";

            }
            set
            {
                Properties[StoreIDKey] = value;
            }
        }
        public string IsMaterialIssue
        {
            get
            {
                if (Properties.ContainsKey(IsMaterialIssueKey))
                    return Properties[IsMaterialIssueKey].ToString();
                else
                    return "";

            }
            set
            {
                Properties[IsMaterialIssueKey] = value;
            }
        }
        public string IsStoreTransfer
        {
            get
            {
                if (Properties.ContainsKey(IsStoreTransferKey))
                    return Properties[IsStoreTransferKey].ToString();
                else
                    return "";

            }
            set
            {
                Properties[IsStoreTransferKey] = value;
            }
        }
        public string IsGoodReceipt
        {
            get
            {
                if (Properties.ContainsKey(IsGoodReceiptKey))
                    return Properties[IsGoodReceiptKey].ToString();
                else
                    return "";

            }
            set
            {
                Properties[IsGoodReceiptKey] = value;
            }
        }
        #endregion


        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new frmLogin()) { BarBackgroundColor = Color.FromHex("#fecb02") };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
