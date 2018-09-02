//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class QA
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int IdQAStatus { get; set; }

        [Required]
        [StringLength(100)]
        public string ClienteNome { get; set; }

        [StringLength(100)]
        public string ClienteEmail { get; set; }

        [StringLength(100)]
        public string ClienteLocalizacao { get; set; }

        [Required]
        [StringLength(1000)]
        public string Pergunta { get; set; }

        public DateTime DtPergunta { get; set; }
                
        public string Resposta { get; set; }

        public string NomeRespondente { get; set; }

        public DateTime? DtResposta { get; set; }

        [StringLength(100)]
        public string RespTerceiroClienteNome { get; set; }

        [StringLength(100)]
        public string RespTerceiroClienteEmail { get; set; }

        [StringLength(100)]
        public string RespTerceiroClienteLocalizacao { get; set; }

        public Nullable<int> RespTerceiroStatus { get; set; }
        public Nullable<int> IdClienteAcesso { get; set; }
        public Nullable<int> IdBadge { get; set; }
        public Nullable<int> QtdAjudou { get; set; }
        public Nullable<int> QtdNaoAjudou { get; set; }

        

        [StringLength(500)]
        public string ModeracaoOBS { get; set; }
    
        public virtual QAStatus QAStatus { get; set; }
        public virtual Produto Produto { get; set; }
    }
}