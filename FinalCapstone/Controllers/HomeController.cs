using FinalCapstone.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalCapstone.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

       
        public ActionResult ViewAll()
        {
            HttpWebRequest WR = WebRequest.CreateHttp("http://localhost:54267/api/Values/GetAllCars");
           


            HttpWebResponse Response;
            try
            {
                Response = (HttpWebResponse)WR.GetResponse();
            }
            catch (WebException e)
            {

                ViewBag.Error = "Exception";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            if (Response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = Response.StatusCode;
                ViewBag.ErrorDescription = Response.StatusDescription;
                return View();
            }
            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string CarData = reader.ReadToEnd();

            try
            {
                JArray JsonData = JArray.Parse(CarData);
                ViewBag.Cars = JsonData;
             
            }
            catch (Exception e)
            {
                ViewBag.Error = "JSON Issue";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            return View();

            
        }

        public ActionResult Search()
        {
            return View();
        }

        
        public ActionResult SearchAPI(string id)
        {
            JArray JsonData;
            HttpWebRequest WR = WebRequest.CreateHttp($"http://localhost:54267/api/Values/GetSearchAll/{id}");
            
            HttpWebResponse Response;
            try
            {
                Response = (HttpWebResponse)WR.GetResponse();
            }
            catch (WebException e)
            {

                ViewBag.Error = "Exception";
                ViewBag.ErrorDescription = e.Message;
                return View("Error");
            }

            if (Response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = Response.StatusCode;
                ViewBag.ErrorDescription = Response.StatusDescription;
                return View("Error");
            }
            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string CarData = reader.ReadToEnd();
            
            try
            {
                 JsonData = JArray.Parse(CarData);
                ViewBag.Cars = JsonData;

            }
            catch (Exception e)
            {
                ViewBag.Error = "JSON Issue";
                ViewBag.ErrorDescription = e.Message;
                return View("Error");
            }

            return Json(JsonData);


        }

        [HttpPost]
        public ActionResult SearchInventory(string input)
        {
            CarEntities db = new CarEntities();
            
            List<Car> list = db.Cars.Where(
               c => c.Make.Contains(input)
               || c.Model.Contains(input)
               || c.Color.Contains(input)
               ).ToList();

            return Json(list);            
        }

        
    }
}
