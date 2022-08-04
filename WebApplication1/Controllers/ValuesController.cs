using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Lib;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values_ 查詢 _若沒有輸入訂單，則查詢全部訂單
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 _ 查詢<有帶參數> 輸入訂單號碼，查詢目前庫存量及訂單數量
        //public string Get(int id)
        public string Get(OrderModel model)
        {
            SqlDataAccess db = new SqlDataAccess();
            List<OrderModel> DATA = db.OrderDATA();
            DATA.Where(s => s.OrderNume == model.Order || s.ItemNo == model.Order).ToList();

            return DATA + "";
        }

        // POST api/values _ 新增
        //public void Post([FromBody] string value)
        public void Post([FromBody] OrderModel value)
        {

        }

        // PUT api/values/5 _ 修改
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5 _刪除
        //public void Delete(int id)
        //{
        //}
    }
}
