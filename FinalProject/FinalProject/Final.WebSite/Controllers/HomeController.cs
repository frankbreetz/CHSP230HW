using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final.Business;
using Final.WebSite.Models;
using Microsoft.AspNet.Identity.Owin;
using Final.ProductDatabase;

namespace Final.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryManager categoryManager;
        private readonly IProductManager productManager;
        private readonly IUserManager userManager;

        public HomeController(ICategoryManager categoryManager,
                              IProductManager productManager,
                              IUserManager userManager)
        {
            this.categoryManager = categoryManager;
            this.productManager = productManager;
            this.userManager = userManager;
        }

        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterForClass(int id)
        {
            var database = new Entities();

            var sessionUser = (User)Session["User"];

            // Get the user object from the database
            var user = database.Users.First(t => t.UserId == sessionUser.UserId);

            // Get the class object from the database
            // var newClass = database.Classes.First(t => t.ClassId == model.ClassId);
            Class newClass = null;
            foreach (var t in database.Classes)
            {
                if (t.ClassId == id)
                {
                    newClass = t;
                    break;
                }
            }

            // Add the class to the user object
            user.Classes.Add(newClass);

            // Save the changes to the database

            database.SaveChanges();
            return View();
        }
[HttpPost]
public ActionResult Register(RegisterModel registerModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Register(registerModel.Email, registerModel.Password);
                if (user==null)
                {
                    ModelState.AddModelError("","Passwords don't Match");
                }
                else
                {
                    Session["User"] = new Final.WebSite.Models.RegisterModel { Id = user.Id, Email = user.Name };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(registerModel.Email, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }
            return View(registerModel);
        }
        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new Final.WebSite.Models.UserModel { Id = user.Id, Name = user.Name };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        public ActionResult Category(int id)
        {
            var category = categoryManager.Category(id);
            var products = productManager
                                .ForCategory(id)
                                .Select(t =>
                                    new Final.WebSite.Models.ProductModel
                                    {
                                        Id = t.Id,
                                        Email = t.Email,
                                        IsAdmin = t.IsAdmin,
                                        Password = t.Password
                                    }).ToArray();

            var model = new CategoryViewModel
            {
                Category = new Final.WebSite.Models.CategoryModel(category.Id, category.Name),
                Products = products
            };

            return View(model);
        }

        public ActionResult Index()
        {
            var categories = categoryManager.Categories
                                            .Select(t => new Final.WebSite.Models.CategoryModel(t.Id, t.Name))
                                            .ToArray();
            var model = new IndexModel { Categories = categories };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}