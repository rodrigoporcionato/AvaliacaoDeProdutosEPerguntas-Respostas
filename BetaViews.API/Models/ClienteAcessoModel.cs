//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BetaViews.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClienteAcessoModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    
        public virtual ClienteModel Cliente { get; set; }
    }
}