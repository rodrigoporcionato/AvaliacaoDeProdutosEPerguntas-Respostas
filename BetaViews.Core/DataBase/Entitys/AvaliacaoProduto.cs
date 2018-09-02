namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AvaliacaoProduto")]
    public partial class AvaliacaoProduto
    {
        public int Id { get; set; }

        public int IdAvaliacao { get; set; }

        public int IdProduto { get; set; }

        public virtual Avaliacao Avaliacao { get; set; }

        public virtual Produto Produto { get; set; }
    }
}
