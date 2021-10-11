using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace DroidInventory_MSC.Models
{
    public class Stores

    {
        public Int32 StoreID { get; set; }
        public String StoreNumber { get; set; }
        public String StoreName { get; set; }
        public Int32 Parent_ID { get; set; }
        public Boolean IsMain { get; set; }
        public Byte StoreLevel { get; set; }
        public String StoreRealCode { get; set; }
        public Boolean Locked { get; set; }
        public Boolean IsMainStore { get; set; }
    }




    

}