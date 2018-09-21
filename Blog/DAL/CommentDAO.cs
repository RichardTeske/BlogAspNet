using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog.DAL
{
    public class CommentDAO
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static List<Comment> GetComments(int postId)
        {
            return ctx.Comment.Where(x => x.Post.PostId == postId).ToList();
        }

        public static void CreateComment(Comment comment)
        {
            ctx.Comment.Add(comment);
            ctx.SaveChanges();
        }

        public static Comment FindId(int id)
        {
            return ctx.Comment.Find(id);
        }

        public static void EditComment(Comment comment)
        {

            ctx.Entry(comment).State = EntityState.Modified;
            ctx.SaveChanges();

        }

        public static List<Comment> MyComments(int id)
        {
            return ctx.Comment.Where(x => x.User.UserId == id).ToList();
        }

    }
}