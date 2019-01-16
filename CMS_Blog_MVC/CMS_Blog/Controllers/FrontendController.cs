namespace CMS_Blog.Controllers
{
    using CMS_Blog.Models;
    using Microsoft.AspNetCore.Mvc;

    public class FrontendController : Controller
    {
        [ActionName("Frontend")]
        public IActionResult Index()
        {
            Blog blog = Blog.Get();

            return View(blog);
        }


        [ActionName("Post")]
        public IActionResult Post() => View();

        //

    }
}