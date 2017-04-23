using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW1BirthdayCard.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BirthdayMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BirthdayMessage(Models.BirthdayWish birthdayWish)
        {
            if (ModelState.IsValid)
            {
                return View("Thanks",birthdayWish );
            }
            else
            {
                return View();
            }
            //return View($"Thanks", );
        }
    }
}