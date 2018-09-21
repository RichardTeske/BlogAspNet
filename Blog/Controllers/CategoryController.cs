using Blog.DAL;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult CategoryPanel()
        {
            ViewBag.Data = DateTime.Now;
            return View(CategoryDAO.CategoryList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Status = 0;
                category.Date = DateTime.Now;
                if (CategoryDAO.CreateCategory(category))
                {
                    return RedirectToAction("CategoryPanel", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Não é possivel criar uma categoria com o mesmo nome");
                    return View(category);
                }
            }
            else
            {
                return View(category);
            }
        }
        
        public ActionResult TurnOffCategory(int id)
        {
            Category catOriginal = CategoryDAO.FindId(id);
            catOriginal.Status = 1;
            catOriginal.Date = DateTime.Now;
            CategoryDAO.EditCategory(catOriginal);
            return RedirectToAction("CategoryPanel","Category");
            
        }
        
        
        public ActionResult TurnOnCategory(int id)
        {
            Category catOriginal = CategoryDAO.FindId(id);
            catOriginal.Status = 0;
            CategoryDAO.EditCategory(catOriginal);
            catOriginal.Date = DateTime.Now;
            return RedirectToAction("CategoryPanel","Category");
        }

        public ActionResult Edit(int id)
        {
            return View(CategoryDAO.FindId(id));
        }

        [HttpPost]
        public ActionResult Edit(Category catEdited)
        {
            Category catOriginal = CategoryDAO.FindId(catEdited.CategoryId);

            catOriginal.Name = catEdited.Name;
            catOriginal.Date = DateTime.Now;
            catOriginal.Status = catEdited.Status;

            if (ModelState.IsValid)
            {
                if (CategoryDAO.EditCategory(catOriginal))
                {
                    return RedirectToAction("CategoryPanel", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Não é possivel editar uma categoria com um nome já existente");
                    return View(catOriginal);
                }
            }
            else
            {
                return View(catOriginal);
            }
        }
    }
}