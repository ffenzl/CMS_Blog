
using CMS_Blog.Models;
using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CMS_Blog.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [ActionName("User")]
        public ActionResult Index()
        {
            if ((Object)Session["UserId"] != null)
                return View(CMS_Blog.Models.User.GetAll());

            Session["Session_Val"] = "Session abgelaufen";
            return this.RedirectToAction("Login", "Backend");
        }

        [ActionName("AddUser")]
        public ActionResult AddUser()
        {
            if ((Object)Session["UserId"] != null)
                return View("AddUser");

            Session["Session_Val"] = "Session abgelaufen";
            return this.RedirectToAction("Login", "Backend");
        }

        [ActionName("EditUser")]
        public ActionResult EditUser(CMS_Blog.Models.User user)
        {
            if ((Object)Session["UserId"] != null)
                return View("EditUser", user);

            Session["Session_Val"] = "Session abgelaufen";
            return this.RedirectToAction("Login", "Backend");
        }


        // POST: User/Create
        [HttpPost()]
        [ActionName("Create")]
        public ActionResult Create(
                string name,
                string surname,
                string alias,
                string email,
                int role,
                string password,
                string confirmedPassword)
        {
            
            if (string.Equals(password, confirmedPassword))
            {
                User user = new User();
                user.Name = name;
                user.Alias = alias;
                user.Surname = surname;
                user.Password = Crypto.HashPassword(password);
                user.Email = email;

                if (CMS_Blog.Models.User.Create(user))
                    return View("User", CMS_Blog.Models.User.GetAll());
              
                ViewData["Error"] = "Der Benutzer konnte nicht gespeichert werden!";
                return View("AddUser");
            }

            ViewData["Error"] = "Die eingegebenen Passwörter stimmen nicht überein!";
            return View("AddUser");
        }

        // POST: User/Create
        [HttpPost()]
        [ActionName("Update")]
        public ActionResult Update(
                User item
        )
        {
            if (CMS_Blog.Models.User.Update(item))
                return View("User", CMS_Blog.Models.User.GetAll());

            ViewData["Error"] = "Benutzer konnte nicht gespeichert werden!";
            return View("EditUser");
        }

    }
}