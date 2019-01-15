
using CMS_Blog.Models;
using CMS_Blog.SQLite;
using System;
using System.Data;
using System.IO;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CMS_Blog.Controllers
{
    public class BackendController : Controller
    {
        public ActionResult Login() => View();

        [ActionName("Index")]
        public ActionResult Index()
        {
            if ((Object)Session["UserId"] != null)
                return View();

            Session["Session_Val"] = "Session abgelaufen";
            return this.RedirectToAction("Login", "Backend");
        }

        [ActionName("Logout")]
        public ActionResult Logout()
        {
            Session.Clear();

            return View("Login");
        }

        // POST: /Account/Login
        [HttpPost()]
        [ActionName("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(SessionModel info)
        {
            SqlDatabase database = new SqlDatabase();

            if (database.OpenConnection(Path.Combine(Server.MapPath("~"), @"SQLite\CMS_BLOG.db")))
            {
                if (info.UserName != null && info.User_Pwd != null)
                {
                    DataTable returnData =
                            database.readSql(
                                    "select * " +
                                    "  from User " +
                                    " where user_alias = \"" + info.UserName + "\"" +
                                    "    or upper(user_email) = \"" + info.User_Pwd + "\"");


                    if (returnData.Rows.Count == 0)
                    {
                        Session["Session_Val"] = "Login fehlgeschlagen";
                        return View("Login");
                    }
                    else
                    {
                        DataRow row = returnData.Rows[0];
                        string hashedPassword = row["user_password"].ToString();

                        if (Crypto.VerifyHashedPassword(hashedPassword, info.User_Pwd))
                        {
                            Session["UserId"] = info.UserName;
                            return this.RedirectToAction("Post", "Post", Blog.Get());
                        }
                        else
                        {
                            Session["Session_Val"] = "Login fehlgeschlagen";
                            return View("Login");
                        }
                    }
                }

                Session["Session_Val"] = "Login fehlgeschlagen";
                return View("Login");
            }
            else
            {
                Session["Session_Val"] = "Login fehlgeschlagen";
                return View("Login");
            }

        }

        
        [ActionName("Pictures")]
        public ActionResult Pictures()
        {
            if ((Object)Session["UserId"] != null)
                return View("Pictures");

            Session["Session_Val"] = "Session abgelaufen";
            return this.RedirectToAction("Login", "Backend");
        }
    }
}