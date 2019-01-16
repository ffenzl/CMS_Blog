using CMS_Blog.Models;
using CMS_Blog.SQLite;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CMS_Blog.Controllers
{
    public class PostController : Controller
    {
        private List<Post> _posts = new List<Post>();

        [ActionName("EditPost")]
        public ActionResult EditBlog(Post post)
        {
            return View("EditPost", post);
        }

        [HttpPost("Save")]
        [ActionName("Save")]
        public ActionResult Save(string content, Post item)
        {
            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection("SQLite\\CMS_BLOG.db"))
            {
                string statement = 
                        "update Post " +
                            "set post_text = '" + content + "' " +
                            "where post_id = " + item.Id;

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
        [HttpGet("Post")]
        [ActionName("Post")]
        public ActionResult Index(Blog blog)
        {
            blog.Posts = Post.GetAll(blog.Id);

            return View("Post", blog);
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Post/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}