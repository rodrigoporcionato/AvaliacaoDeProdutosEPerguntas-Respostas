namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Badge")]
    public partial class Badge
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Icone { get; set; }

        public int TipoBadge { get; set; }

        public int IdCliente { get; set; }

        public virtual Clientes Clientes { get; set; }
    }
}
