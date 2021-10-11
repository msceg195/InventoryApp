using System;
using System.Collections.Generic;
using System.Text;

namespace DroidInventory_MSC.Models
{
    class MaterialIssueRequesitions
    {
        public Int32 RequesitionID { get; set; }
        public String RequesitionNo { get; set; }
        public String RequesitionDate { get; set; }
        public String EnglishName { get; set; }
        public Decimal TQty { get; set; }
        public String RName { get; set; }

    }


     class SC_MaterialIssueRequesitionsDetails

    {
        public Int32 MaterialIssueRequesitionDetailID { get; set; }
        public Int32 MaterialIssueRequesitionID { get; set; }
        public Decimal ItemID { get; set; }
        public Decimal Qty { get; set; }
        public Decimal UnitID { get; set; }
        public String Notes { get; set; }
        public Decimal IssuedQty { get; set; }
        public String InnerCode { get; set; }

    }
}
