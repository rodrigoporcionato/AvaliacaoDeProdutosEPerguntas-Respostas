//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BetaViews.Messages.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class QAProdutoModel
    {
        public int Id { get; set; }
        public int IdQA { get; set; }
        public int IdProduto { get; set; }
    
        public virtual ProdutoModel ProdutoModel { get; set; }
        public virtual QAModel QAModel { get; set; }
    }
}