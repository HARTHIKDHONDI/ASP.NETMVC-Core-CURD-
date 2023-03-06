using Microsoft.AspNetCore.Mvc;
using Basic_model.Models;
using Basic_model.Bussiness_logic;
using System.Data;
using System.Dynamic;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Basic_model.Controllers
{
    public class RegisterController : Controller
    {


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(Curd obj)
        {
            if (ModelState.IsValid)
            {
                bool res = CurdOperations.Insert(obj);
                if (res == true)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.errormsg = "Something wents worng!";
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login obj)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = new DataTable();
                dt = CurdOperations.Loginpage(obj);
                if (dt.Rows.Count > 0)
                {
                    HttpContext.Session.SetString("Loginpersonsname", dt.Rows[0]["Name"].ToString());
                    HttpContext.Session.SetString("LoginTime", System.DateTime.Now.ToShortTimeString());
                    ClaimsIdentity identity = null;
                    bool isAuthentic = false;
                    if (dt.Rows[0]["Role"].ToString() == "Admin")
                    {
                        identity = new ClaimsIdentity(new[]
                            {
                    new Claim(ClaimTypes.Name,obj.Email),
                    new Claim(ClaimTypes.Role,"Admin")
                    },
                             CookieAuthenticationDefaults.AuthenticationScheme);
                        isAuthentic = true;
                    }
                    else if (dt.Rows[0]["Role"].ToString() == "Student")
                    {
                        identity = new ClaimsIdentity(new[]
                            {
                    new Claim(ClaimTypes.Name,obj.Email),
                    new Claim(ClaimTypes.Role,"Student")
                    },
                             CookieAuthenticationDefaults.AuthenticationScheme);
                        isAuthentic = true;
                    }
                    if (isAuthentic)
                    {
                        var principals = new ClaimsPrincipal(identity);
                        var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principals);
                        return RedirectToAction("Home");
                    }

                   /* if (dt.Rows[0]["Status"].ToString().ToLower() == "true")
                       {


                           if (dt.Rows[0]["Role"].ToString() == "Student")
                           {
                               //TempData["otp"] = "1111";
                              // return RedirectToAction("verifyOTP");
                               Random rand = new Random();
                               TempData["otp"] = rand.Next(1111, 9999).ToString();
                               bool data = sendEmail(obj.Email);
                               if (data)
                               {
                                   return RedirectToAction("verifyOTP");
                               }
                               else
                               {
                                   return View();
                               }
                               //return RedirectToAction("Home");
                           }
                           else if (dt.Rows[0]["Role"].ToString() == "Admin")
                           {
                               return RedirectToAction("Admin_Home");
                           }
                       }*/
                    else
                    {
                        ViewBag.errormsg = "User is Inactive";
                    }
                    /*if (dt.Rows[0]["Status"].ToString().ToLower()=="true")
                    {
                        return RedirectToAction("Home");
                    }
                    else
                    {
                        ViewBag.errormsg = "User is Inactive";
                    }*/
                }
                else
                {
                    ViewBag.errormsg = "Something wents worng!";
                }
            }

            return View();
        }

        public IActionResult EndSession()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Register");
        }

        [HttpGet]
        [SetSessionGlobally]
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        [SetSessionGlobally]
        public IActionResult Admin_Home()
        {
            return View();
        }

        [HttpGet]
        [SetSessionGlobally]
        public IActionResult Display()
        {
            return View(CurdOperations.Getalldetails());
        }

        [HttpGet]
        [SetSessionGlobally]
        public IActionResult Edit(int? Users_id)
        {
            return View(CurdOperations.GetDataById((int)Users_id));
        }
        [HttpPost]
        public IActionResult Edit(Curd obj)
        {
            bool res = CurdOperations.Update(obj);
            if (res == true)
            {
                return RedirectToAction("Display");
            }
            else
            {
                ViewBag.errormsg = "Something went worng!";
                return View();
            }

        }

        [HttpGet]
        public IActionResult delete_u(int? Users_id)
        {
            bool res = CurdOperations.Delete((int)(Users_id));
            if (res == true)
            {
                return RedirectToAction("Display");
            }
            else
            {
                return View();
            }


        }

        [HttpGet]
        [SetSessionGlobally]
        public IActionResult dropdown_D()
        {
            ViewBag.data = CurdOperations.Customer_order();
            return View();
        }
        [HttpPost]
        [SetSessionGlobally]
        public IActionResult dropdown_D(string CustomerID)
        {
            ViewBag.data = CurdOperations.Customer_order();
            ViewBag.orders = CurdOperations.Get_orders_details((string)CustomerID);
            return View();

        }
        [HttpGet]
        public IActionResult od_date(DateTime? fromdate, DateTime? todate)
        {
            List<Order_date_details> obj = new List<Order_date_details>();
            bool result = false;
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];

            SqlConnection con = new SqlConnection(dbconnectionstr);

            try
            {
                con.Open();

                string query = "select CustomerID,OrderDate,Freight,ShipName,ShipAddress from Orders where OrderDate between '" + fromdate + " 'and '" + todate + " '";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
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


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ModelState.Clear();
                con.Close();
            }
            return View(obj);
        }

        /*  [HttpPost]
          public IActionResult od_date(DateTime? fromdate, DateTime? todate)
          {

              return View(CurdOperations.Orderdate_details((DateTime)fromdate,(DateTime)todate));
          }*/

        [HttpGet]
        [SetSessionGlobally]
        public IActionResult Index()
        {
            return View(PopulateFiles());
        }

        [HttpPost]
        [SetSessionGlobally]
        public IActionResult Index(List<IFormFile?> postedFiles)
        {
            string constr = @"Data Source=DESKTOP-5CUQIQU;Initial Catalog=MVC_Pratices;uid=sa;pwd=123456;";
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                string type = postedFile.ContentType;
                byte[] bytes = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    postedFile.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "INSERT INTO Files(Name, ContentType, Data) VALUES (@Name, @ContentType, @Data)";
                        cmd.Parameters.AddWithValue("@Name", fileName);
                        cmd.Parameters.AddWithValue("@ContentType", type);
                        cmd.Parameters.AddWithValue("@Data", bytes);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }

            return View(PopulateFiles());
        }
        [SetSessionGlobally]
        public IActionResult DELETE(int? Id)
        {
            bool res = CurdOperations.Deleteupload((int)Id);
            if (res == true)
            {
                return RedirectToAction("Index");
            }
            else
            {

                return View();
            }
            return View();
        }
        [SetSessionGlobally]
        public FileResult DownloadFile(int fileId)
        {
            FileModel model = PopulateFiles().Find(x => x.Id == Convert.ToInt32(fileId));
            string fileName = model.Name;
            string contentType = model.ContentType;
            byte[] bytes = model.Data;
            return File(bytes, contentType, fileName);
        }

        private static List<FileModel> PopulateFiles()
        {
            string constr = @"Data Source=DESKTOP-5CUQIQU;Initial Catalog=MVC_Pratices;uid=sa;pwd=123456;";
            List<FileModel> files = new List<FileModel>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM Files";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            files.Add(new FileModel
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Name = sdr["Name"].ToString(),
                                ContentType = sdr["ContentType"].ToString(),
                                Data = (byte[])sdr["Data"]
                            });
                        }
                    }
                    con.Close();
                }
            }

            return files;
        }


         public bool sendEmail(string receiver)
          {
              bool chk = false;
              try
              {
                  MailMessage mail = new MailMessage();
                  mail.From = new MailAddress("hcsm.kcea@gmail.com");
                  mail.To.Add(receiver);
                  mail.IsBodyHtml = true;
                  mail.Subject = "otp";
                  mail.Body = "your otp is:" + TempData["otp"];
                  SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                  client.Credentials = new NetworkCredential("hcsm.kcea@gmail.com","gmjchnlekonusqsr");
                  client.EnableSsl = true;
                  client.Send(mail);
                  chk = true;
              }
              catch (Exception ex)
              {
                  throw;
              }
              return chk;

          }

        [HttpGet]
        public IActionResult verifyOTP()
        {
            return View();
        }
        [HttpPost]
        public IActionResult verifyOTP(OTPModel obj)
        {
            string a = TempData["otp"].ToString();
            if (obj.OTPCode == a)
            {
                return RedirectToAction("Home");
            }
            else
            {
                ViewBag.error = "Entered OTP is not Correct";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Forget_Pass()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Forget_Pass(Forg_Email obj)
        {
            TempData["Eid"] = obj.Email;
            bool res = CurdOperations.Forget_Email(obj.Email);
            if (res == true)
            {
                //Random rand = new Random();
                //TempData["otp"] = rand.Next(1111, 9999).ToString();
                TempData["otp"] = "1111";


                return RedirectToAction("verifyOTPEmail");



            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public IActionResult verifyOTPEmail()
        {
            return View();
        }
        [HttpPost]
        public IActionResult verifyOTPEmail(OTPModel obj)
        {
            string a = TempData["otp"].ToString();
            if (obj.OTPCode == a)
            {
                return RedirectToAction("ResetPassword");
            }
            else
            {
                ViewBag.error = "Entered OTP is not Correct";
                return View();
            }
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(Reset_Password obj)
        {
            bool res = CurdOperations.Resest_pas(obj.Password, Convert.ToString(TempData["Eid"]));
            if (res == true)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.errormsg = "Something went worng!";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Authentication()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Authentication(RoleBasedModel obj)
        {
            
            return View();
        }

    }




}
