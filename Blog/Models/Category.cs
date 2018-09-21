using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(30,ErrorMessage = "Campo deve conter no máximo 30 caracteres")]
        [Display(Name = "Nome da Categoria")]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int Status { get; set; }

    }
}