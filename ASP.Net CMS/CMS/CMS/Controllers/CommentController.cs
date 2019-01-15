
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
        public ActionResult Create(string name, string email, string text, Post item)
        {
            Comment comment = new Comment();
            comment.Date = DateTime.Now;
            comment.State = Comment.STATE_OPEN;
            comment.Text = text;
            comment.UserEmail = email;
            comment.UserName = name;
            comment.Post_id = item.Id;

            if (CMS_Blog.Models.Comment.Create(comment))
                return View("Frontend");

            return View("Frontend");
        }

        /*// POST: Comment/Update
        [HttpPost()]
        [ActionName("Update")]
        public ActionResult Update(
                Comment item
        )
        {
            if (CMS_Blog.Models.Comment.Update(item))
                return View("Comment", CMS_Blog.Models.Comment.GetAll());

            ViewData["Error"] = "Kommentar konnte nicht gespeichert werden!";
            return View("Comment");
        }*/


    }
}