using Blog.Models;
using Blog.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.DAL;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        public ActionResult PostPanel()
        {
            ViewBag.Categories = CategoryDAO.CategoryList();
            ViewBag.Users = UserDAO.UserList();
            ViewBag.Posts = PostDAO.PostList();
            return View(PostDAO.PostList());
        }

        // GET: Post
        public ActionResult Create()
        {
            ViewBag.Category = CategoryDAO.CategoryActiveList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post post, int? Category)
        {
            ViewBag.Category = new SelectList(CategoryDAO.CategoryList(), "CategoryId", "Name");
            post.Category = CategoryDAO.FindId(Category);
            post.Status = 0;
            post.Date = DateTime.Now;
            post.User = UserDAO.FindId(Sessao.user.UserId);
           
                if (Category != null)
                {
                    
                    if (PostDAO.CreatePost(post))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Já existe um post com o titulo " + post.Title);
                        return View(post);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Por favor, selecione uma categoria!");
                    return View(post);
                }
            
        }

        public ActionResult DisablePost(int id)
        {
            Post post = PostDAO.FindId(id);
            post.Status = 1;

            PostDAO.EditPost(post);
            if (Sessao.user.Admin == 1)
            {
                return RedirectToAction("PostPanel", "Post");
            } else
            {
                return RedirectToAction("UserPanel", "User");
            }
            
        }

        public ActionResult EnablePost(int id)
        {
            Post post = PostDAO.FindId(id);
            post.Status = 0;

            PostDAO.EditPost(post);

            if (Sessao.user.Admin == 1)
            {
                return RedirectToAction("PostPanel", "Post");
            }
            else
            {
                return RedirectToAction("UserPanel", "User");
            }
            
        }
        
        public ActionResult Detail(int id)
        {
            Post post = PostDAO.FindId(id);
            List<Comment> comments = CommentDAO.GetComments(id);
            ViewBag.Post = post;
            ViewBag.Comments = comments;
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(PostDAO.FindId(id));
        }

        [HttpPost]
        public ActionResult Edit(Post postEdited)
        {
            Post postOrig = PostDAO.FindId(postEdited.PostId);
            postOrig.Body = postEdited.Body;
            postOrig.Title = postEdited.Title;
            postOrig.Date = DateTime.Now;
            
            if (PostDAO.EditPost(postOrig))
            {
                if (postOrig.User.Admin == 1)
                {
                    return RedirectToAction("AdminPanel", "User");
                }
                else
                {
                    return RedirectToAction("UserPanel", "User");
                }

            }
            else
            {
                ModelState.AddModelError("", "Já existe um post com esse Nome!");
                return View(postOrig);
            }

        }


    }
}