using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class HomeViewModel
    {
        [DisplayName("訂單號碼")]
        public string OrderNume { get; set; }

        [DisplayName("料號")]
        public string ItemNo { get; set; }

        [DisplayName("訂單數量")]
        public int QTY { get; set; }

        [DisplayName("目前庫存量")]
        public int Inventory_QTY { get; set; }

        public List<OrderModel> ALL_DATA { get; set; }
    }
}