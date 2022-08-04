using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrderModel
    {
        [DisplayName("編號")]
        public int Id { get; set; }

        [DisplayName("訂單號碼")]
        public string OrderNume { get; set; }

        [DisplayName("料號")]
        public string ItemNo { get; set; }

        [DisplayName("訂單數量")]
        public int QTY { get; set; }

        [DisplayName("目前庫存量")]
        public int Inventory_QTY { get; set; }

        public string Order { get; set; }
    }
}