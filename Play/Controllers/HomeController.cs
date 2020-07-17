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

        [Route("Home/mb")]
        public IActionResult TestModelBinding([FromHeader(Name = "Accept-Language")] string language) 
        {
            //en-GB,en-US;q=0.9,en;q=0.8,it;q=0.7
            return Content(language);
        }

        [Route("Home/base64string")]
        [HttpPost]
        //post from postman a base64 endoded string
        //dGhpcyBpcyB0aGUgY29udGVudCBvZiBteSB0ZXh0IGZpbGUNCmFuZCBhIG5ldyBsaW5lDQp0aGUgZW5kIQ==
        public IActionResult Base64String([FromForm] byte[] file, string filename)
        {
            System.IO.File.WriteAllBytes(filename, file);
            var json = JsonConvert.SerializeObject(ControllerContext.HttpContext.Request.Headers,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            return Ok(json);
        }
    }
}