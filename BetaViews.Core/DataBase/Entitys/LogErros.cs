namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LogErros
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Interface { get; set; }

        [Required]
        [StringLength(150)]
        public string ProtocoloRetorno { get; set; }

        [Required]
        [StringLength(50)]
        public string CodigoErro { get; set; }

        [Required]
        public string Descricao { get; set; }

        public int TipoMensagem { get; set; }

        public DateTime Data { get; set; }
    }
}
