using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BetaViews.Messages.Models
{
    public class LoginModel
    {

        [Required]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string Senha { get; set; }

        [Display(Name = "Lembrar senha")]
        public bool LembrarSenha { get; set; }

    }
}