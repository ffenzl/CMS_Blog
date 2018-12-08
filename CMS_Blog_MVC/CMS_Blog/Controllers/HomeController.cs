namespace CMS_Blog.Controllers
{
    using CMS_Blog.SQLite;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Data;
    using System.Web.Helpers;

    public class HomeController : Controller
    {
        public IActionResult Index() => View();


        [HttpGet("GetBackend")]
        public IActionResult GetBackend()
        {
            return View("backend");
        }

        //
        // POST: /Account/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Name, string Password)
        {
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
                        return View("backend");
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
    }
}