namespace CMS_Blog.Controllers
{
    using CMS_Blog.SQLite;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Data;
    using System.Web.Helpers;

    public class FrontendController : Controller
    {
        public IActionResult Index() => View();


        //
        
    }
}