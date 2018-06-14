using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace FinalCapstone.Controllers
{
    public class ValuesController : ApiController
    {
        

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
        
        public JsonResult<List<Car>> GetAllCars()
        {
            CarEntities db = new CarEntities();
            List<Car> cars = db.Cars.ToList();
            return Json(cars);
        }

        public JsonResult<List<Car>> GetSearchAll(string id)
        {
            CarEntities db = new CarEntities();
            List<Car> cars = db.Cars.Where(
               c => c.Make.Contains(id)
               || c.Model.Contains(id)
               || c.Color.Contains(id)
               ).ToList();
            
            return Json(cars);
        }
    }
}
