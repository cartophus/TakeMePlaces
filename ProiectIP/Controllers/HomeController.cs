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

    public struct PlaceSortObj
    {
        public int placePoz;
        public double placeRating;
        public Place place;

        public PlaceSortObj(int p1, double p2, Place p3)
        {
            placePoz = p1;
            placeRating = p2;
            place = p3;
        }
    }

    public class PlaceJson
    {
        public String placeId;

        public PlaceJson(String placeId)
        {
            this.placeId = placeId;
        }
    }

    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public static int Comparison(PlaceSortObj POS1, PlaceSortObj POS2)
        {
            if (POS1.placeRating > POS2.placeRating) return -1;
            else if (POS1.placeRating < POS2.placeRating) return 1;
            return 0;
        }

        public ActionResult Index()
        {


            return View();
        }

       public JsonResult GetPlaceDetails()
        {
            var places = (from place in db.Places select place).ToList();

            List<PlaceJson> placeJson = new List<PlaceJson>();

            foreach (Place place in places)
            {
                placeJson.Add(new PlaceJson(place.PlaceId));
            }

            return Json(placeJson, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Rate(String category, String id)
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
                if (!db.Places.Any(o => o.PlaceId == id)) db.Places.Add(place);


                //add new rating to our database
                if (!db.RatingModels.Any(o => o.PlaceId == id && o.UserId == userId))
                {
                    RatingModel ratingModel = new RatingModel();
                    ratingModel.PlaceId = id;
                    ratingModel.Rating = rating;
                    ratingModel.UserId = userId;
                    ratingModel.UnixTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    db.RatingModels.Add(ratingModel);

                    string text = ratingModel.UserId + "\t" + ratingModel.PlaceId + "\t" + ratingModel.Rating + "\t" + ratingModel.UnixTimestamp;
                    using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(@"C:\Users\vladi\Desktop\work\An3\TakeMePlaces\TakeMePlaces\ProiectIP\AI\user.data", true))
                    {
                        file.WriteLine(text);
                    }
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


        public ActionResult ShowRecommendations()
        {
            String userId = User.Identity.GetUserId();

            var users = from user in db.Users select user;

            int userPos = 0;
            foreach (ApplicationUser user in users)
            {
                if (user.Id == userId) break;
                userPos++;
            }

            var places1 = from place in db.Places select place;
            var placeSize = places1.Count();
            var places = places1.ToList();
            int[] placePoz = new int[placeSize];
            for(int j = 0;j<placeSize;j++)
            {
                placePoz[j] = j;
            }


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

            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(@"C:\Users\vladi\Desktop\work\An3\TakeMePlaces\TakeMePlaces\ProiectIP\AI\conka.txt", true))
            {
                file.WriteLine("Errors:");
                file.WriteLine(errors);
                file.WriteLine();
                file.WriteLine("Results:");
                file.WriteLine(results);

                //String pattern = @"\s-\s?[+*]?\s?-\s";

                string[] userRec = results.Split(new char[2] { ']', '[' });
                                    
                string[] userRecPerPlace = userRec[userPos].Split(new char[2] { '\n', ' ' });
                
                double[] recPerPlace = new double[placeSize+10];
                int i = 0;
                foreach (string rec in userRecPerPlace)
                {
                    file.WriteLine(rec);
                    double number;
                    if (Double.TryParse(rec.Trim(), out number))
                        recPerPlace[i] = number;
                    else recPerPlace[i] = 0;

                    i++;
                }

                PlaceSortObj[] PSO = new PlaceSortObj[placeSize];
                for(int k = 0; k < placeSize ;k++)
                {
                    PSO[k] = new PlaceSortObj(placePoz[k], recPerPlace[k], places[k]);
                }

                Array.Sort(PSO.ToArray(), Comparison);

                List<Place> placesView = new List<Place>();
                for(int t = 0; t < 5;t++)
                {
                    placesView.Add(PSO[t].place);
                }

                ViewBag.PlacesView = placesView;

            }

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