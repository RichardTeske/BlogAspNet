using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog.DAL
{
    public class UserDAO
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static List<User> UserList()
        {
            return ctx.User.ToList();
        }

        public static User FindId(int id)
        {
            return ctx.User.Find(id);
        }
        

        public static bool EditUser(User user)
        {
            if (ctx.User.FirstOrDefault(x => x.Email.Equals(user.Email) && x.UserId != user.UserId) == null)
            {
                ctx.Entry(user).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static User CheckEmail (User user)
        {
            return ctx.User.FirstOrDefault(x => x.Email.Equals(user.Email));
        }

        public static bool CreateUser (User user)
        {
            if (CheckEmail(user) == null)
            {
                ctx.User.Add(user);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
        public static User FindEmailPassword(User user)
        {
            return ctx.User.
                FirstOrDefault(x => x.Email.Equals(user.Email) &&
                x.Password.Equals(user.Password));
        }
    }
}