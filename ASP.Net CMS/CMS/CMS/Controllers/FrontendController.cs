namespace CMS_Blog.Controllers
{
    using CMS_Blog.Models;
    using System;
    using System.Web.Mvc;

    public class FrontendController : Controller
    {
        [ActionName("Frontend")]
        public ActionResult Index()
        {
            if ((Object)Session["UserId"] != null)
            {
                Blog blog = Blog.Get();

                return View(blog);
            }

            Session["Session_Val"] = "Session abgelaufen";
            return this.RedirectToAction("Login", "Backend");
        }


        [ActionName("Post")]
        public ActionResult Post()
        {
            if ((Object)Session["UserId"] != null)
                return View();

            Session["Session_Val"] = "Session abgelaufen";
            return this.RedirectToAction("Login", "Backend");
        }

        //

    }
}