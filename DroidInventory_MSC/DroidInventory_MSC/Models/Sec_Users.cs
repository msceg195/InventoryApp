using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Linq;
using System.Web;

namespace Inventory_API.Models
{
    
    public class Sec_Users
    {
        public Int32 ID { set; get; }
        public String UserName { set; get; }
        public String Password { set; get; }
        public Int32 GroupID { set; get; }
        public Boolean Enabled { set; get; }
        public Boolean ISPOS { set; get; }
        public Boolean OnLine { set; get; }
        public String Email { set; get; }
        public String EmailPassword { set; get; }
        public String StoreID { set; get; }
        public String IsMaterialIssue { set; get; }
        public String IsStoreTransfer { set; get; }
        public String IsGoodReceipt { set; get; }
    }




   

}