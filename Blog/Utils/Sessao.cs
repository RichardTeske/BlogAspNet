using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Utils
{
    public class Sessao
    {

        private static string NOME_SESSAO = "UserId";
        public static User user { get; set; }

        public static void Logged(User userL)
        {
            user = userL;
        }

        public static int UserId
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            }
            set { HttpContext.Current.Session["UserId"] = value.ToString(); }
        }
        
        public static string UserIdProf()
        {
            if (HttpContext.Current.Session[NOME_SESSAO] == null)
            {
                Guid guid = Guid.NewGuid();
                HttpContext.Current.Session[NOME_SESSAO] = guid.ToString();
            }
            return HttpContext.Current.Session[NOME_SESSAO].ToString();
        }
        

        public static void DisableSession()
        {
            HttpContext.Current.Session["UserId"] = null;
            user = null;
        }

    }
}