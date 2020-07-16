using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Play.Models;

namespace Play.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var employees = new List<Employee>();
            employees.Add(new Employee(1));
            employees.Add(new Employee(2));
            employees.Add(new Employee(3));

            return View(employees);
        }

        // return a 302 Found and adds a Location header corresponding to the new route
        // the browser then makes the request to the location header route (second get)
        public IActionResult Test()
        {
            return RedirectToAction("Index");
        }

        [Route("Home/context")]
        public IActionResult TestHttpContext()
        {
            //anything more and it throws memory exception
            var x = ControllerContext.HttpContext.Request.Headers;
            var y = JsonConvert.SerializeObject(x,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            //return Json(y);
            return Content(y);
        }
    }
}