using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog.DAL
{
    public class CategoryDAO
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static List<Category> CategoryList()
        {
            return ctx.Category.ToList();
        }

        public static List<Category> CategoryActiveList()
        {
            return ctx.Category.Where(x => x.Status == 0).ToList();
        }

        public static bool CreateCategory(Category category)
        {
            if (CheckName(category) == null)
            {
                ctx.Category.Add(category);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static Category CheckName(Category category)
        {
            return ctx.Category.FirstOrDefault(x => x.Name.Equals(category.Name));
        }

        public static Category FindId(int? id)
        {
            return ctx.Category.Find(id);
        }

        public static bool EditCategory(Category cat)
        {
            if (ctx.Category.FirstOrDefault(x => x.Name.Equals(cat.Name) && x.CategoryId != cat.CategoryId) == null)
            {
                ctx.Entry(cat).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
    }
}