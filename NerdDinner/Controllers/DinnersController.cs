using System;
using System.Collections.Generic;
using System.Data;
using NerdDinner.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {

        DinnerRepository dinnerRepository = new DinnerRepository();
        private NerdDinnerDataContext db = new NerdDinnerDataContext();
        const int pageSize = 10;

        // HTTP-GET: /Dinners/
        public ActionResult Index(int? page)
        {
            int pageIndex = page ?? 1;

            var dinners = dinnerRepository.FindUpcomingDinners();

            return View(dinners.ToPagedList(pageIndex, pageSize).ToList());

        }

        // HTTP-GET /DinnersDetails/2

        public ActionResult Details(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(dinner);
            }
        }

        //get: Dinners/edit/{id}

        public ActionResult Edit(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);



            return View(dinner);
        }


        // saves edited changes to a dinner
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            try
            {
                UpdateModel(dinner);

                dinnerRepository.Save();

                return RedirectToAction("Details", new { id = dinner.DinnerID });
            }

            catch
            {
                foreach (var issue in dinner.GetRuleViolations())
                {
                    ModelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
                }
            }

            return View(dinner);
        }
        // creates a new form for a dinner
        public ActionResult Create()
        {
            Dinner dinner = new Dinner() { EventDate = DateTime.Now.AddDays(7) };

            return View(dinner);
        }

        //Saves and adds dinner to db
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dinner.Host = "SomeUser";

                    dinnerRepository.Add(dinner);
                    dinnerRepository.Save();

                    return RedirectToAction("Details", new { id = dinner.DinnerID });
                }
                catch
                {
                    foreach (var issue in dinner.GetRuleViolations())
                    {
                        ModelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
                    }
                }
            }

            return View(dinner);
        }

        //Delete a dinner
        public ActionResult Delete(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(dinner);
            }
        }

        //Saves and removes the dinner from db
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id, string confirmButton)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
            {
                return View("NotFound");
            }

            dinnerRepository.Delete(dinner);
            dinnerRepository.Save();

            return View("Deleted");
        }
    }
}