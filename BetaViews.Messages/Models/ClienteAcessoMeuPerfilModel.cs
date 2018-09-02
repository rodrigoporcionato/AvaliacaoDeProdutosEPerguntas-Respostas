using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BetaViews.Messages.Models
{
    public class ClienteAcessoMeuPerfilModel
    {
        [Required]        
        public int IdUsuario { get; set; }


        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "E-mail (login de acesso)")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail não é valido")]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Departamento")]
        public string Departamento { get; set; }


        
        [Display(Name = "Alterar senha?")]
        public bool AlterPassword { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Antiga")]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessage = "A {0} precisa ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação de senha não conferem.")]
        public string ConfirmPassword { get; set; }



    }

    
}