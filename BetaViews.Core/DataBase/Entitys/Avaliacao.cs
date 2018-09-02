namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Avaliacao")]
    public partial class Avaliacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Avaliacao()
        {
            AvaliacaoProduto = new HashSet<AvaliacaoProduto>();
        }

        public int Id { get; set; }

        public int IdLoja { get; set; }

        public int IdTipoAvaliacao { get; set; }

        public int IdAvaliacaoStatus { get; set; }

        public DateTime DtAvaliacao { get; set; }

        [Required]
        [StringLength(50)]
        public string ClienteNome { get; set; }

        [Required]
        [StringLength(50)]
        public string ClienteEmail { get; set; }

        [Required]
        [StringLength(50)]
        public string ClienteLocalizacao { get; set; }

        [Required]
        [StringLength(300)]
        public string ClienteTitulo { get; set; }

        [Required]
        [StringLength(2000)]
        public string ClienteComentario { get; set; }

        public int ClienteClassificacao { get; set; }

        public bool ClienteRecomenda { get; set; }

        public bool? ClienteVerificado { get; set; }

        public int? QtdAjudou { get; set; }

        public int? QtdNaoAjudou { get; set; }

        public int? QtdReportAbuse { get; set; }

        public string ModeracaoOBS { get; set; }

        public int? IdBadge { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AvaliacaoProduto> AvaliacaoProduto { get; set; }

        public virtual AvaliacaoStatus AvaliacaoStatus { get; set; }

        public virtual Loja Loja { get; set; }
    }
}
