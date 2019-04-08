using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProiectIP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowRecommendations()
        {
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\vladi\Anaconda3\python.exe";

            // 2) Provide script and arguments
            var script = @"C:\Users\vladi\Desktop\work\An3\TakeMePlaces\TakeMePlaces\ProiectIP\AI\ScriptPython2.py";

            psi.Arguments = $"\"{script}\"";

            // 3) Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // 4) Execute process and get output
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            // 5) Display output
            Console.WriteLine("ERRORS:");
            Console.WriteLine(errors);
            Console.WriteLine();
            Console.WriteLine("Results:");
            Console.WriteLine(results);
            return View();
        }

        public ActionResult Restaurants()
        {
            return View();
        }

        public ActionResult Museums()
        {
            return View();
        }

        public ActionResult Cafes()
        {
            return View();
        }

        public ActionResult MovieTheaters()
        {
            return View();
        }

        public ActionResult ShoppingMalls()
        {
            return View();
        }

        public ActionResult Bars()
        {
            return View();
        }

        public ActionResult Parks()
        {
            return View();
        }
    }
}