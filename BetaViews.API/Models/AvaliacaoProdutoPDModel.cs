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
    
    public partial class AvaliacaoProdutoPDModel
    {
        public int Id { get; set; }
        public int IdAvaliacaoProduto { get; set; }
        public int IdProdutoPD { get; set; }
        public string RespostaEscolhida { get; set; }
    
        public virtual AvaliacaoProdutoModel AvaliacaoProduto { get; set; }
        public virtual ProdutoPDModel ProdutoPD { get; set; }
    }
}