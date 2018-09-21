using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        public Category Category { get; set; }

        public User User { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(50, ErrorMessage = "Campo deve conter no máximo 50 caracteres")]
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Descrição")]
        public string Body { get; set; }

        public int Status { get; set; }
        public DateTime Date { get; set; }

    }
}