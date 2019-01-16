
using CMS_Blog.Models;
using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CMS_Blog.Controllers
{
    public class CommentController : Controller
    {
        // GET: User
        [ActionName("Comment")]
        public ActionResult Comments()
        {
            if ((Object)Session["UserId"] != null)
                return View(CMS_Blog.Models.Comment.GetAll(Comment.STATE_OPEN));

            Session["Session_Val"] = "Session abgelaufen";
            return this.RedirectToAction("Login", "Backend");
        }

        


        // POST: User/Create
        [HttpPost()]
        [ActionName("Create")]
        public ActionResult Create(string name, string email, string text, int id)
        {
            Comment comment = new Comment();
            comment.Date = DateTime.Now;
            comment.State = Comment.STATE_OPEN;
            comment.Text = text;
            comment.UserEmail = email;
            comment.UserName = name;
            comment.Post_id = id;

            if (CMS_Blog.Models.Comment.Create(comment))
                return this.RedirectToAction("Frontend", "Frontend");

            return this.RedirectToAction("Frontend", "Frontend");
        }

        // POST: Comment/Update
        public ActionResult Accept(int id)
        {
            if (CMS_Blog.Models.Comment.Accept(id))
                return View("Comment", CMS_Blog.Models.Comment.GetAll(Comment.STATE_OPEN));

            ViewData["Error"] = "Kommentar konnte nicht gespeichert werden!";
            return View("Comment", CMS_Blog.Models.Comment.GetAll(Comment.STATE_OPEN));
        }

        public ActionResult Reject(int id)
        {
            if (CMS_Blog.Models.Comment.Reject(id))
                return View("Comment", CMS_Blog.Models.Comment.GetAll(Comment.STATE_OPEN));

            ViewData["Error"] = "Kommentar konnte nicht gespeichert werden!";
            return View("Comment", CMS_Blog.Models.Comment.GetAll(Comment.STATE_OPEN));
        }
    }
}