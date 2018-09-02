namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Produto")]
    public partial class Produto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produto()
        {
            this.AvaliacaoProduto = new HashSet<AvaliacaoProduto>();
            this.QA = new HashSet<QA>();
        }

        public int Id { get; set; }
        public int IdLoja { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string Departamento { get; set; }
        public string Categoria { get; set; }
        public string SubCategoria { get; set; }
        public string Marca { get; set; }
        public string Link { get; set; }
        public string ImagemProduto { get; set; }
        public System.DateTime DtCadastro { get; set; }
        public System.DateTime DtUltimaAtualizacao { get; set; }
        public Nullable<int> TotalRecomendaProduto { get; set; }
        public Nullable<int> TotalAvaliacoes { get; set; }
        public Nullable<double> AvaliacaoGeral { get; set; }
        public Nullable<int> Total1Estrela { get; set; }
        public Nullable<int> Total2Estrela { get; set; }
        public Nullable<int> Total3Estrela { get; set; }
        public Nullable<int> Total4Estrela { get; set; }
        public Nullable<int> Total5Estrela { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AvaliacaoProduto> AvaliacaoProduto { get; set; }
        public virtual Loja Loja { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QA> QA { get; set; }
    }
}
