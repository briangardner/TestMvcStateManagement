using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestState.Helpers;

namespace TestState.Controllers
{
    public class HomeController : Controller
    {

       
        public ActionResult Index()
        {
            return View();
        }

        int Counter = 0; 
        public ActionResult About()// saving info on the server 
        {
            if (TempData["Counter"] == null)// very first request to About from that specific user
                TempData["Counter"] = 0;
            else // fetch the counter from tempdata
                Counter = (int)TempData["Counter"];



            Counter += 1;
            TempData["Counter"] = Counter; // save this for the next request! 

            ViewBag.Message = $"Counter = {Counter}";

            return View();
        }

        public ActionResult Contact()// saving info on a cookie 
        {
            HttpCookie cookie;
            if (Request.Cookies[Constants.CounterCookie] == null)// cookie expired, or the user cleared the cookies
            {
                // bake a new cookie
                cookie = new HttpCookie(Constants.CounterCookie);
                cookie.Value = "0";
                cookie.Expires = DateTime.UtcNow.AddYears(1); 
            }
            else
            {
                cookie = Request.Cookies[Constants.CounterCookie];
            }
            // fetch the info from the cookie 
        
            Counter = int.Parse(cookie.Value); // retrive the old Counter value 
            Counter += 1;
            cookie.Value = Counter.ToString();// save the new value back into the cookie 
            Response.Cookies.Add(cookie);// send the cookie back to the client 
            ViewBag.Message = $"Counter = {Counter}";
            return View();
        }
    }
}