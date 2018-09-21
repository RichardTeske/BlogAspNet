using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    [Table("SubCategory")]
    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }

        [Display(Name = "Categoria")]
        public Category Category { get; set; }
        
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Nome da Sub-Categoria")]
        public string Name { get; set; }
    }
}