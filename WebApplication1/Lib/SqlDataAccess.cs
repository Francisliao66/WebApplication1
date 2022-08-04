using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Lib
{
    public class SqlDataAccess
    {
        public string GetConnectString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<OrderModel> OrderDATA()
        {
            List<OrderModel> lists = new List<OrderModel>();
            using (SqlConnection conn = new SqlConnection(GetConnectString("DB_MVC")))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [Hsinchu_DB].[dbo].[Orders]", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            try
                            {
                                lists.Add(new OrderModel
                                {
                                    Id = Convert.ToInt32(dr["Id"] != DBNull.Value ? dr["Id"] : ""),
                                    OrderNume = Convert.ToString(dr["OrderNume"] != DBNull.Value ? dr["OrderNume"] : ""),
                                    ItemNo = Convert.ToString(dr["ItemNo"] != DBNull.Value ? dr["ItemNo"] : ""),
                                    QTY = Convert.ToInt32(dr["QTY"] != DBNull.Value ? dr["QTY"] : ""),
                                    Inventory_QTY = Convert.ToInt32(dr["Inventory_QTY"] != DBNull.Value ? dr["Inventory_QTY"] : ""),
                                });
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }
                    dr.Close();
                }
            }
            return lists;
        }

        #region 新增訂單庫存
        public OrderModel OrderupdateView(int Id)
        {
            OrderModel model = new OrderModel();
            using (SqlConnection conn = new SqlConnection(GetConnectString("DB_MVC")))
            {
                using (SqlCommand cmd = new SqlCommand("select * from [Hsinchu_DB].[dbo].[Orders] where Id = @Id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    SqlParameter[] p =
                    {
                        new SqlParameter("@Id", Id),
                    };
                    cmd.Parameters.AddRange(p);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            try
                            {
                                model = new OrderModel()
                                {
                                    OrderNume = Convert.ToString(dr["OrderNume"] != DBNull.Value ? dr["OrderNume"] : ""),
                                    ItemNo = Convert.ToString(dr["ItemNo"] != DBNull.Value ? dr["ItemNo"] : ""),
                                    QTY = Convert.ToInt32(dr["QTY"] != DBNull.Value ? dr["QTY"] : ""),
                                    Inventory_QTY = Convert.ToInt32(dr["Inventory_QTY"] != DBNull.Value ? dr["Inventory_QTY"] : ""),
                                };
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }
                    dr.Close();
                }
            }
            return model;
        }

        public void SP_Update_Inventory_QTY(OrderModel model)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectString("DB_MVC")))
            {
                using (SqlCommand Cmd = new SqlCommand("SP_Update_Inventory_QTY", conn))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p =
                    {
                        new SqlParameter("@Id", model.Id),
                        new SqlParameter("@OrderNume", model.OrderNume ?? ""),
                        new SqlParameter("@ItemNo", model.ItemNo ?? ""),
                        new SqlParameter("@QTY", model.QTY.ToString() ?? ""),
                        new SqlParameter("@Inventory_QTY", model.Inventory_QTY.ToString() ?? ""),
                    };
                    Cmd.Parameters.AddRange(p);
                    conn.Open();
                    Cmd.ExecuteNonQuery();
                }
            }
        }

        public void SP_INSERT_ORDER(OrderModel model)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectString("DB_MVC")))
            {
                using (SqlCommand Cmd = new SqlCommand("SP_INSERT_ORDER", conn))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p =
                    {
                        new SqlParameter("@OrderNume", model.OrderNume ?? ""),
                        new SqlParameter("@ItemNo", model.ItemNo ?? ""),
                        new SqlParameter("@QTY", model.QTY.ToString() ?? ""),
                        new SqlParameter("@Inventory_QTY", model.Inventory_QTY.ToString() ?? ""),
                    };
                    Cmd.Parameters.AddRange(p);
                    conn.Open();
                    Cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
}