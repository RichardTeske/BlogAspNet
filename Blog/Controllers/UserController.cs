using Blog.DAL;
using Blog.Models;
using Blog.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Blog.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult AdminPanel()
        {
            ViewBag.Data = DateTime.Now;
            return View(UserDAO.UserList());
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Name == "admin" && user.Password == "admin")
                {
                    user.Admin = 1;
                }
                else
                {
                    user.Admin = 0;
                }
                user.Date = DateTime.Now;
                user.Status = 0;
                if (UserDAO.CreateUser(user))
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Esse email já existe");
                return View(user);
            }
            return View(user);
        }

        public ActionResult Edit(int id)
        {
            return View(UserDAO.FindId(id));
        }

        [HttpPost]
        public ActionResult Edit(User userEdited)
        {
            User userOrig = UserDAO.FindId(userEdited.UserId);

            userOrig.Name = userEdited.Name;
            userOrig.Email = userEdited.Email;
            userOrig.Biography = userEdited.Biography;
            userOrig.Date = DateTime.Now;
            userOrig.Admin = userEdited.Admin;
            userOrig.Status = userEdited.Status;
            userOrig.Password = userOrig.Password;

            if (UserDAO.EditUser(userOrig))
            {
                if (userOrig.Admin == 1)
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
                ModelState.AddModelError("", "Já existe um usuario com esse email");
                return View(userOrig);
            }

        }

        public ActionResult DisableUser(int id)
        {

            User user = UserDAO.FindId(id);
            user.Status = 1;
            user.Date = DateTime.Now;
            UserDAO.EditUser(user);
            return RedirectToAction("AdminPanel", "User");
        }

        public ActionResult EnableUser(int id)
        {

            User user = UserDAO.FindId(id);
            user.Status = 0;
            user.Date = DateTime.Now;
            UserDAO.EditUser(user);
            return RedirectToAction("AdminPanel", "User");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,Password")]User user)
        {
            user = UserDAO.FindEmailPassword(user);
            if (user != null)
            {
                Sessao.Logged(user);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "O e-mail ou senha não coincidem!");
            return View(user);
        }


        public ActionResult Logout()
        {
            Sessao.DisableSession();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult TurnAdmin(int id)
        {

            User user = UserDAO.FindId(id);
            user.Admin = 1;
            UserDAO.EditUser(user);
            return RedirectToAction("AdminPanel", "User");
        }

        public ActionResult TurnUser(int id)
        {

            User user = UserDAO.FindId(id);
            user.Admin = 0;
            UserDAO.EditUser(user);
            return RedirectToAction("AdminPanel", "User");
        }

        public ActionResult UserPanel()
        {
            ViewBag.MyPosts = PostDAO.MyPosts(Sessao.user.UserId);
            ViewBag.MyComments = CommentDAO.MyComments(Sessao.user.UserId);
            return View();
        }
    }
}