using System.Data.SqlClient;
using System.Data;
using Basic_model.Models;
using System.Net;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace Basic_model.Bussiness_logic
{
    public class CurdOperations
    {
        public static bool Insert(Curd obj)
        {
            bool result = false;
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];

            SqlConnection con = new SqlConnection(dbconnectionstr);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert_reg", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", obj.Name);
                cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(obj.DOB));
                cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                cmd.Parameters.AddWithValue("@Mobile", obj.Mobile);
                cmd.Parameters.AddWithValue("@Email", obj.Email);
                cmd.Parameters.AddWithValue("@Password", obj.Password);
                cmd.Parameters.AddWithValue("@Role", obj.Role);
                cmd.Parameters.AddWithValue("@Status", obj.Status);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    return true;
                    // result = true;             
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            return true;
        }
        public static DataTable Loginpage(Login obj)
        {
            //bool result = false;
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];

            SqlConnection con = new SqlConnection(dbconnectionstr);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("login_check", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", obj.Email);
                cmd.Parameters.AddWithValue("@Password", obj.Password);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
                /*SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    return true;

                }
                else 
                {
                    return false;
                }*/
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            
        }

        public static List<DisplayModel> Getalldetails()
        {
            List<DisplayModel> obj = new List<DisplayModel>();
            bool result = false;
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];

            SqlConnection con = new SqlConnection(dbconnectionstr);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("get_all_Details", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    obj.Add(
                        new DisplayModel
                        {
                            Users_id = Convert.ToInt32(dr["Users_id"].ToString()),
                            Name = dr["Name"].ToString(),
                            DOB = Convert.ToDateTime(dr["DOB"].ToString()),
                            Gender = dr["Gender"].ToString(),
                            Mobile = dr["Mobile"].ToString(),
                            Email = dr["Email"].ToString(),
                            Password = dr["Password"].ToString(),
                            Role = dr["Role"].ToString(),
                            Status = Convert.ToBoolean(dr["status"].ToString())
                        }
                        );
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        public static Curd GetDataById(int Users_id) 
        {
            Curd obj = null;
            bool result = false;
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];

            SqlConnection sc = new SqlConnection(dbconnectionstr);
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("get_Details_Userid", sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Users_id", Users_id);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new Curd();
                    obj.Users_id = Convert.ToInt32(ds.Tables[0].Rows[i]["Users_id"].ToString());
                    obj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    obj.DOB = Convert.ToDateTime(ds.Tables[0].Rows[i]["DOB"].ToString());
                    obj.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    obj.Mobile = ds.Tables[0].Rows[i]["Mobile"].ToString();
                    obj.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                    obj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                    obj.Role = ds.Tables[0].Rows[i]["Role"].ToString();
                    obj.Status = Convert.ToBoolean(ds.Tables[0].Rows[i]["Status"].ToString());
                }
                return obj;
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                sc.Close();
            }
        }

        public static bool Update(Curd obj)
        {
            bool result = false;
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];

            SqlConnection con = new SqlConnection(dbconnectionstr);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update_details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", obj.Name);
                cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(obj.DOB));
                cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                cmd.Parameters.AddWithValue("@Mobile", obj.Mobile);
                cmd.Parameters.AddWithValue("@Email", obj.Email);
                cmd.Parameters.AddWithValue("@Password", obj.Password);
                cmd.Parameters.AddWithValue("@Role", obj.Role);
                cmd.Parameters.AddWithValue("@Status", obj.Status);
                cmd.Parameters.AddWithValue("@Users_id", obj.Users_id);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    return true;
                                
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            
        }

        public static bool Delete(int Users_id)
        {
            bool result = false;
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];

            SqlConnection con = new SqlConnection(dbconnectionstr);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete_details", con);
                cmd.CommandType = CommandType.StoredProcedure;
               
                cmd.Parameters.AddWithValue("@Users_id", Users_id);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    return true;
                    // result = true;             
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            
        }

        public static List<DropModel> Customer_order()
        {
            List<DropModel> obj = new List<DropModel>();
            bool result = false;
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];

            SqlConnection con = new SqlConnection(dbconnectionstr);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("uses_CustomerId", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    
                    obj.Add(
                        new DropModel
                        {
                            CustomerID = dr["CustomerID"].ToString()
                        }
                            
                        );
                }
                return obj;
            }



            catch (Exception ex)
            {
                throw;
            }
            
        }

    
        public static List<Drop_displayModel> Get_orders_details(string CustomerID)
        {
            List<Drop_displayModel> obj =  new List<Drop_displayModel>(); 
            bool result = false;
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            SqlConnection sc = new SqlConnection(dbconnectionstr);
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Customerid_vw_details", sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    obj.Add(
                         new Drop_displayModel
                         {
                             CustomerID = dr["CustomerID"].ToString(),
                             OrderDate =Convert.ToDateTime( dr["OrderDate"].ToString()),
                             Freight = Convert.ToDouble(dr["Freight"].ToString()),
                             ShipName = dr["ShipName"].ToString(),
                             ShipAddress = dr["ShipAddress"].ToString()
                         }
                         );
                }

          
                return obj;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                sc.Close();
            }


        }
        public static bool Deleteupload(int Id)
        {
            bool result = false;
            var dbconfig = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            SqlConnection con = new SqlConnection(dbconnectionstr);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteFiles", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return true;
        }

        public static List<Order_date_details> Orderdate_details(DateTime fromdate , DateTime todate)
        {
              List<Order_date_details> obj = new List<Order_date_details>();
              bool result = false;
              var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
              string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];

              SqlConnection con = new SqlConnection(dbconnectionstr);
              try
              {
                  con.Open();
                  SqlCommand cmd = new SqlCommand("Get_details_orderdate", con);
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddWithValue("@fromdate", Convert.ToDateTime(fromdate));
                  cmd.Parameters.AddWithValue("@todate", Convert.ToDateTime(todate));
                  SqlDataReader dr = cmd.ExecuteReader();
                  while (dr.Read())
                  {
                      obj.Add(
                           new Order_date_details
                           {
                               CustomerID = dr["CustomerID"].ToString(),
                               OrderDate = Convert.ToDateTime(dr["OrderDate"].ToString()),
                               Freight = Convert.ToDouble(dr["Freight"].ToString()),
                               ShipName = dr["ShipName"].ToString(),
                               ShipAddress = dr["ShipAddress"].ToString()
                           }
                           );
                  }

                return obj;
              }
              catch (Exception)
              {
                  throw;
              }
              finally
              {
                  con.Close();
              }

        }
        public static bool Forget_Email(string Email)
        {
            bool result = false;
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];

            SqlConnection con = new SqlConnection(dbconnectionstr);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("for_emailcheck", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", Email);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    return true;

                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }


        }

        public static bool Resest_pas(string password, string Email)
        {
            bool result = false;
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];

            SqlConnection con = new SqlConnection(dbconnectionstr);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Reset_ByEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", password);
                
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }


        }
    }
 }




                           