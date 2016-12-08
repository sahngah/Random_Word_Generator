using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace randomWordGenerator.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.random = HttpContext.Session.GetString("random");
            ViewBag.attempt = HttpContext.Session.GetInt32("attempt");
            Console.WriteLine("viewbag counter", ViewBag.attempt);
            return View();
        }

        [HttpGet]
        [Route("generate")]
        public IActionResult Generate()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string random_word = new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());
            HttpContext.Session.SetString("random", random_word);
            if (HttpContext.Session.GetInt32("attempt") == null)
            {
                HttpContext.Session.SetInt32("attempt", 1);
            }
            else
            {
                int current = (int) HttpContext.Session.GetInt32("attempt");
                current++;
                HttpContext.Session.SetInt32("attempt", current);
            }
            return RedirectToAction("index");
        }
    }
}
