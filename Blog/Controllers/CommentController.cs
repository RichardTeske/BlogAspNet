using Blog.DAL;
using Blog.Models;
using Blog.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string body, int postId)
        {
            Comment comment = new Comment();
            comment.Body = body;
            comment.Post = PostDAO.FindId(postId);
            comment.User = Sessao.user;
            comment.Status = 0;
            comment.Date = DateTime.Now;

            CommentDAO.CreateComment(comment);

            return RedirectToAction("Detail","Post", new { id = postId });
        }

        public ActionResult DisableFromPost(int commentId, int postId)
        {
            Comment comment = CommentDAO.FindId(commentId);
            comment.Status = 1;
            CommentDAO.EditComment(comment);
            return RedirectToAction("Detail", "Post", new { id = postId });
            
        }

        public ActionResult UnableFromPost(int commentId, int postId)
        {
            Comment comment = CommentDAO.FindId(commentId);
            comment.Status = 0;
            CommentDAO.EditComment(comment);
            return RedirectToAction("Detail", "Post", new { id = postId });
            

        }

        public ActionResult EnableFromPanel(int commentId)
        {
            Comment comment = CommentDAO.FindId(commentId);
            comment.Status = 0;
            CommentDAO.EditComment(comment);
            
            return RedirectToAction("UserPanel", "User");
            

        }
        public ActionResult DisableFromPanel(int commentId)
        {
            Comment comment = CommentDAO.FindId(commentId);
            comment.Status = 1;
            CommentDAO.EditComment(comment);

            return RedirectToAction("UserPanel", "User");


        }
    }
}