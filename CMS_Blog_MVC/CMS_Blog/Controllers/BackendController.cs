using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using CMS_Blog.SQLite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return View("Index");
            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection("CMS_BLOG.db"))
            {
                DataTable returnData =
                        database.readSql(
                                "select * " +
                                "  from User " +
                                " where user_name = \"" + Name + "\"" +
                                "    or user_email = \"" + Name + "\"");


                if (returnData.Rows.Count == 0)
                {
                    ViewData["Error"] = "Login fehlgeschlagen";
                    return View("Index");
                }
                else
                {
                    DataRow row = returnData.Rows[0];
                    string hashedPassword = row["user_password"].ToString();

                    if (Crypto.VerifyHashedPassword(hashedPassword, Password))
                        return View("~/Backend/Index");
                    else
                    {
                        ViewData["Error"] = "Login fehlgeschlagen";
                        return View("Index");
                    }
                }
            }
            else
            {
                ViewData["Error"] = "Login fehlgeschlagen";
                return View("Index");
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