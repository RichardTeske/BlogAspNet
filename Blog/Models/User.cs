using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Nome do Usuário")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Descrição do Perfil")]
        public string Biography { get; set; }

        public int Admin { get; set; }

        public DateTime Date { get; set; }

        public int Status { get; set; }
    }
}





