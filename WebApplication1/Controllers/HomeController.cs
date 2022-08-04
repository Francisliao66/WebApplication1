using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Lib;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        SqlDataAccess db = new SqlDataAccess();
        public ActionResult Index(OrderModel models,string Order,int? id)
        {
            //ValuesController api = new ValuesController();
            List<OrderModel> alldata = db.OrderDATA();

            HomeViewModel model = new HomeViewModel()
            {
                ALL_DATA = alldata,
            };

            //api.Get();

            ViewBag.Order = Order;
            ViewBag.number = alldata.Where(s => s.OrderNume == Order).Count();
            ViewBag.test = model.ALL_DATA.Where(s => s.ItemNo == Order).Count();

            if (!string.IsNullOrEmpty(Order))
            {
                model.ALL_DATA = model.ALL_DATA.Where(s => s.OrderNume.Contains(Order) || s.ItemNo.Contains(Order)).ToList();
            }
            return View(model);
        }

        #region 新增訂單
        public ActionResult InsertOrder(int Id)
        {
            OrderModel DATA = db.OrderupdateView(Id);
            return View(DATA);
        }
        [HttpPost]
        public ActionResult InsertOrder(OrderModel model)
        {
            db.SP_INSERT_ORDER(model);
            return RedirectToAction("Index");
        }
        #endregion

        #region 新增訂單庫存
        public ActionResult InsertInvQTY(int Id)
        {
            OrderModel DATA = db.OrderupdateView(Id);
            return View(DATA);
        }
        [HttpPost]
        public ActionResult InsertInvQTY(OrderModel model)
        {
            db.SP_Update_Inventory_QTY(model);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
