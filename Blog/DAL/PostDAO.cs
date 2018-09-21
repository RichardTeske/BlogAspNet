using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog.DAL
{
    public class PostDAO
    {

        private static Context ctx = SingletonContext.GetInstance();

        public static List<Post> PostList()
        {
            return ctx.Post.ToList();
        }

        public static Post FindId(int id)
        {
            return ctx.Post.Find(id);
        }

        public static bool CreatePost(Post post)
        {
            if (CheckTitle(post) == null)
            {
                ctx.Post.Add(post);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static Post CheckTitle(Post post)
        {
            return ctx.Post.FirstOrDefault(x => x.Title.Equals(post.Title));
        }

        public static bool EditPost(Post post)
        {
            if (ctx.Post.FirstOrDefault(x => x.Title == post.Title && x.PostId != post.PostId) == null)
            {
                ctx.Entry(post).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static IEnumerable<Post> PostsOrderBy(int pageNumber, int PostsPerPage)
        {
            IEnumerable<Post> posts = (from Post in ctx.Post
                                       where Post.Date < DateTime.Now
                                       orderby Post.Date descending
                                       select Post).Skip(pageNumber * PostsPerPage).Take(PostsPerPage + 1);
            return posts;

        }

        public static List<Post> MyPosts(int id)
        {
            return ctx.Post.Where(x => x.User.UserId == id).ToList();
        }
    }
}