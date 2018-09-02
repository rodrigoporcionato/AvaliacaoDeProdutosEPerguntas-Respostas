namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PalavraRecusadaPadrao")]
    public partial class PalavraRecusadaPadrao
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(10)]
        public string CodigoPais { get; set; }
    }
}
