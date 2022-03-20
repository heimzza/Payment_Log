using Accounting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Accounting.Helpers;

namespace Accounting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string SaveToFile()
        {
            try
            {
                string salary = Request.Form["Salary"], car = Request.Form["Car"],
                    clothing  = Request.Form["Clothing"], food  = Request.Form["Food"],
                    leisure  = Request.Form["Leisure"], living = Request.Form["Living"];

                //salary = salary.Replace(".", ",");
                //car = car.Replace(".", ",");
                //clothing = clothing.Replace(".", ",");
                //food = food.Replace(".", ",");
                //leisure = leisure.Replace(".", ",");
                //living = living.Replace(".", ",");

                PayList payList = new PayList
                {
                    Name = "Payment List",
                    Salary = double.TryParse(salary, out _) ? double.Parse(salary, CultureInfo.InvariantCulture) : 0,
                    Car = double.TryParse(car, out _) ? double.Parse(car, CultureInfo.InvariantCulture) : 0,
                    Clothing = double.TryParse(clothing, out _) ? double.Parse(clothing, CultureInfo.InvariantCulture) : 0,
                    Food = double.TryParse(food, out _) ? double.Parse(food, CultureInfo.InvariantCulture) : 0,
                    Leisure = double.TryParse(leisure, out _) ? double.Parse(leisure, CultureInfo.InvariantCulture) : 0,
                    Living = double.TryParse(living, out _) ? double.Parse(living, CultureInfo.InvariantCulture) : 0,
                };

                bool result = DataHelper.WriteFile(payList);

                if (result)
                {
                    return "success";
                }
                else
                {
                    return "error";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return "error";
            }
        }
        
        public JsonResult LoadFromFile()
        {
            try
            {
                var jsonString = DataHelper.ReadFile();
                PayList payList = JsonSerializer.Deserialize<PayList>(jsonString);
                return Json(new { error = false, payList });
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return Json(new { error = true });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
