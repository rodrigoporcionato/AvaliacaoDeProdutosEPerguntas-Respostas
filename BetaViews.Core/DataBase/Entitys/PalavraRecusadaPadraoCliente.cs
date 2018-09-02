namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PalavraRecusadaPadraoCliente")]
    public partial class PalavraRecusadaPadraoCliente
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public virtual Clientes Clientes { get; set; }
    }
}
