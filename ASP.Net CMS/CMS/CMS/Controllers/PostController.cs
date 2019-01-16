using CMS_Blog.Models;
using CMS_Blog.SQLite;
using MVCIntegrationExample;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMS_Blog.Controllers
{
    public class PostController : Controller
    {
        private List<Post> _posts = new List<Post>();

        [ValidateInput(false)]
        public ActionResult EditPost(int id)
        {
            if ((Object)Session["UserId"] != null)
                return View("EditPost", Post.Get(id));

            Session["Session_Val"] = "Session abgelaufen";
            return this.RedirectToAction("Login", "Backend");
        }

        [ActionName("AddPost")]
        public ActionResult AddPost(Blog blog)
        {
            if ((Object)Session["UserId"] != null)
                return View("AddPost", blog);

            Session["Session_Val"] = "Session abgelaufen";
            return this.RedirectToAction("Login", "Backend");
        }

        [HttpPost()]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Save(int id, string content, string title, string titleImage)
        {
            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Server.MapPath("~"), @"SQLite\CMS_BLOG.db")))
            {
                string statement = 
                        "update Post " +
                            "set post_text = '" + content + "', " +
                                "post_title = '" + title + "', " +
                                "post_titleImage = '" + titleImage + "' " +
                            "where post_id = " + id;

                database.BeginTransaction();
                bool result = database.executeSql(statement, false);

                if (result)
                {
                    database.TransCommit();
                    return this.RedirectToAction("Post", Blog.Get());
                }
            }

            return this.RedirectToAction("Post", Blog.Get());

        }

        // GET: Post
        [HttpGet()]
        [ActionName("Post")]
        [ValidateInput(false)]
        public ActionResult Index(Blog blog)
        {
            if ((Object)Session["UserId"] != null)
            {
                blog.Posts = Post.GetAll(blog.Id);

                return View("Post", blog);
            }

            Session["Session_Val"] = "Session abgelaufen";
            return this.RedirectToAction("Login", "Backend");
        }


        // GET: Post/Create
        [HttpPost()]
        [ActionName("Create")]
        [ValidateInput(false)]
        public ActionResult Create(string title, string content, string titleImage)
        {
            Blog blog = Blog.Get();
            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
            {
                string statement = "insert into Post ( " +
                        "post_id, " +
                        "post_text, " +
                        "post_date, " + 
                        "user_id, " +
                        "post_title, " +
                        "blog_id, " +
                        "post_titleImage) " +
                    "values( " +
                        Post.GetNextRef() + ", " +
                        "'" + content + "', " +
                        "'" + DateTime.Now.ToString(Post.GlobalDateFormat) + "', " +
                        CMS_Blog.Models.User.Get(Session["UserId"].ToString()).Id + ", " +
                        "'" + title + "', " +
                        blog.Id + ", " +
                        "'" + titleImage + "' " +
                    ");";

                database.BeginTransaction();
                bool result = database.executeSql(statement);
                database.TransCommit();

            }

            return this.RedirectToAction("Post", Blog.Get());
        }

        [ValidateInput(false)]
        public ActionResult Delete(int id)
        {
            Post.Delete(id);

            return this.RedirectToAction("Post", Blog.Get());
        }

    }
}