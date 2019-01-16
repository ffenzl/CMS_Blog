using CMS_Blog.Models;
using CMS_Blog.SQLite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Web.Helpers;

namespace CMS_Blog.Controllers
{
    public class BackendController : Controller
    {
        public IActionResult Login() => View();

        [ActionName("Index")]
        public IActionResult Index() => View();

        // POST: /Account/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Name, string Password)
        {
            return View("Index", Blog.Get());
            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection("SQLite\\CMS_BLOG.db"))
            {
                DataTable returnData =
                        database.readSql(
                                "select * " +
                                "  from User " +
                                " where user_alias = \"" + Name + "\"" +
                                "    or upper(user_email) = \"" + Name.ToUpper() + "\"");


                if (returnData.Rows.Count == 0)
                {
                    ViewData["Error"] = "Login fehlgeschlagen";
                    return View("Login");
                }
                else
                {
                    DataRow row = returnData.Rows[0];
                    string hashedPassword = row["user_password"].ToString();

                    if (Crypto.VerifyHashedPassword(hashedPassword, Password))
                    {
                        return View("Index", Blog.Get());
                    }
                    else
                    {
                        ViewData["Error"] = "Login fehlgeschlagen";
                        return View("Login");
                    }
                }
            }
            else
            {
                ViewData["Error"] = "Login fehlgeschlagen";
                return View("Login");
            }

        }


        [HttpGet("GetBackend")]
        [ActionName("BackendIndex")]
        public IActionResult GetBackend()
        {
            return View("Index");
        }

        [ActionName("Pictures")]
        public IActionResult Pictures()
        {
            return View("Pictures");
        }
    }
}