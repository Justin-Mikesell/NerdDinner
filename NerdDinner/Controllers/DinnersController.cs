using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {

        Models.DinnerRepository dinnerRepository = new Models.DinnerRepository();

        // HTTP-GET: /Dinners/
        public ActionResult Index()
        {
            var dinners = dinnerRepository.FindUpcomingDinners().ToList();

            return View("Index", dinners);
        }

        // HTTP-GET /DinnersDetails/2
        public ActionResult Details(int id)
        {
            Models.Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
            {
                return View("NotFound");
            }
            else
            {
                return View("Details", dinner);
            }
        }
    }
}