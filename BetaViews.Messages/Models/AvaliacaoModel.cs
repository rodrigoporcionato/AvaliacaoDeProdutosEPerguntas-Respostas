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
    
    public partial class AvaliacaoModel
    {
    	
        public AvaliacaoModel()
        {
            this.AvaliacaoProdutoModel = new HashSet<AvaliacaoProdutoModel>();
        }
    
        public int Id { get; set; }
        public int IdLoja { get; set; }
        public int IdTipoAvaliacao { get; set; }
        public int IdAvaliacaoStatus { get; set; }
        public System.DateTime DtAvaliacao { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteLocalizacao { get; set; }
        public string ClienteTitulo { get; set; }
        public string ClienteComentario { get; set; }
        public int ClienteClassificacao { get; set; }
        public bool ClienteRecomenda { get; set; }
        public Nullable<bool> ClienteVerificado { get; set; }
        public Nullable<int> QtdAjudou { get; set; }
        public Nullable<int> QtdNaoAjudou { get; set; }
        public string ModeracaoOBS { get; set; }
        public Nullable<int> IdBadge { get; set; }
        public Nullable<int> QtdReportAbuse { get; set; }
    
        public virtual LojaModel LojaModel { get; set; }
        public virtual AvaliacaoStatusModel AvaliacaoStatusModel { get; set; }
        public virtual ICollection<AvaliacaoProdutoModel> AvaliacaoProdutoModel { get; set; }
    }
}
