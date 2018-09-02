namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Loja")]
    public partial class Loja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Loja()
        {
            Avaliacao = new HashSet<Avaliacao>();
            ClienteAcessoLoja = new HashSet<ClienteAcessoLoja>();
            LojaConfiguracao = new HashSet<LojaConfiguracao>();
            NPSLoja = new HashSet<NPSLoja>();
            Produto = new HashSet<Produto>();
        }

        public int Id { get; set; }

        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string CodigoLoja { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public DateTime DtCadastro { get; set; }

        [StringLength(20)]
        public string CPFCNPJ { get; set; }

        [StringLength(100)]
        public string Endereco { get; set; }

        [StringLength(100)]
        public string EnderecoNumero { get; set; }

        [StringLength(50)]
        public string Bairro { get; set; }

        [StringLength(50)]
        public string Cidade { get; set; }

        [StringLength(10)]
        public string CEP { get; set; }

        [StringLength(200)]
        public string Url { get; set; }

        [StringLength(200)]
        public string Logo { get; set; }

        public bool MarketPlace { get; set; }

        public bool FlagAtivo { get; set; }

        public string TermoAceite { get; set; }

        public int? TotalRecomendaLoja { get; set; }
        public int? TotalAvaliacoes { get; set; }
        public double? AvaliacaoGeral { get; set; }
        public int? Total1Estrela { get; set; }
        public int? Total2Estrela { get; set; }
        public int? Total3Estrela { get; set; }
        public int? Total4Estrela { get; set; }
        public int? Total5Estrela { get; set; }




        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Avaliacao> Avaliacao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClienteAcessoLoja> ClienteAcessoLoja { get; set; }

        public virtual Clientes Clientes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LojaConfiguracao> LojaConfiguracao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NPSLoja> NPSLoja { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produto> Produto { get; set; }
    }
}
