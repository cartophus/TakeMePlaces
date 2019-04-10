using ProiectIP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ProiectIP.Controllers
{
    
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User,Administrator")]
        public ActionResult Rate(String category,String id)
        {
            int rating;
            if (!string.IsNullOrEmpty(Request.Form["rating"]))
            {
                rating = int.Parse(Request.Form["rating"]);

                String userId = User.Identity.GetUserId();

                //add location to our database
                Place place = new Place();
                place.PlaceId = id;
                Place check = db.Places.Find(id);
                if(!db.Places.Any(o => o.PlaceId == id)) db.Places.Add(place);

                
                //add new rating to our database
                if (!db.RatingModels.Any(o => o.PlaceId == id && o.UserId == userId))
                {
                    RatingModel ratingModel = new RatingModel();
                    ratingModel.PlaceId = id;
                    ratingModel.Rating = rating;
                    ratingModel.UserId = userId;
                    ratingModel.UnixTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    db.RatingModels.Add(ratingModel);
                }
                else
                {
                    RatingModel rm = db.RatingModels.Where(o => o.PlaceId == id && o.UserId == userId).FirstOrDefault();
                    rm.Rating = rating;
                }
                db.SaveChanges();

                
            }
            switch (category)
            {
                case "restaurant":
                    return RedirectToAction("Restaurants");
                    
                case "bar":
                    return RedirectToAction("Bars");
                    
                case "museum":
                    return RedirectToAction("Museums");
                    
                case "shopping_mall":
                    return RedirectToAction("ShoppingMalls");
                   
                case "movie_theater":
                    return RedirectToAction("MovieTheaters");
                   
                case "park":
                    return RedirectToAction("Parks");
                  
                case "cafe":
                    return RedirectToAction("Cafes");
                   
                default:
                    return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "User,Administrator")]
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

        [Authorize(Roles = "User,Administrator")]
        public ActionResult Restaurants()
        {
            return View();
        }

        [Authorize(Roles = "User,Administrator")]
        public ActionResult Museums()
        {
            return View();
        }

        [Authorize(Roles = "User,Administrator")]
        public ActionResult Cafes()
        {
            return View();
        }

        [Authorize(Roles = "User,Administrator")]
        public ActionResult MovieTheaters()
        {
            return View();
        }

        [Authorize(Roles = "User,Administrator")]
        public ActionResult ShoppingMalls()
        {
            return View();
        }

        [Authorize(Roles = "User,Administrator")]
        public ActionResult Bars()
        {
            return View();
        }

        [Authorize(Roles = "User,Administrator")]
        public ActionResult Parks()
        {
            return View();
        }
    }
}