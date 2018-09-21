using Blog.DAL;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {

        private const int PostsPerPage = 4;
        private const int PostsPerFeed = 25;

        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 0;

            IEnumerable<Post> posts = PostDAO.PostsOrderBy(pageNumber, PostsPerPage);

            ViewBag.IsPreviousLinkVisible = pageNumber > 0;
            ViewBag.IsNextLinkVisible = posts.Count() > PostsPerPage;
            ViewBag.PageNumber = pageNumber;

            ViewBag.Posts = PostDAO.PostList();
            ViewBag.Users = UserDAO.UserList();
            ViewBag.Categories = CategoryDAO.CategoryList();
            return View(posts.Take(PostsPerPage));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index2(int? page)
        {
            int pageNumber = page ?? 0;

            IEnumerable<Post> posts = PostDAO.PostsOrderBy(pageNumber, PostsPerPage);

            ViewBag.IsPreviousLinkVisible = pageNumber > 0;
            ViewBag.IsNextLinkVisible = posts.Count() > PostsPerPage;
            ViewBag.PageNumber = pageNumber;

            ViewBag.Posts = PostDAO.PostList();
            ViewBag.Users = UserDAO.UserList();
            ViewBag.Categories = CategoryDAO.CategoryList();
            return View(posts.Take(PostsPerPage));
            
        }
    }
}