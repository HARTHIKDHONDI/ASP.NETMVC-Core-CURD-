using Basic_model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Basic_model.Controllers
{
    public class MediaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        /*
         * Products products = GetProducts();
            ViewBag.Message = " ";
            return View();
        public void DownloadFile (String  file)
        {
            try
            {
                
                String filename = file;
                String filepath = "";
                filepath = Path.Combine("~/Files/"+ filename);
                
                Response.Clear();

                Response.Headers.Clear();
                Response.
                Response.ClearContent();
               
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename); 
                Response.Flush();
                Response.TransmitFile(filepath);
                Response.End();

                ViewBag.Message = ex.Message.ToString();

            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
            }










                    iewBag.Message

                catch (Exception ex)




            // HttpContext.ApplicationInstance.CompleteRequest();

            ViewBag.Message = ex.Message.ToString();

            45
        }*/
    }
}
