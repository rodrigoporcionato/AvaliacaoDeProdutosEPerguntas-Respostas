namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClienteConfiguracao")]
    public partial class ClienteConfiguracao
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }

        public bool? ReviewClienteLogado { get; set; }

        public string PaginaRedirecionaLogin { get; set; }

        public bool? FlagUploadImagem { get; set; }

        public int? TotPageSize { get; set; }

        public bool? FaceBookShare { get; set; }

        public bool? TwitterShare { get; set; }

        public int? FlagTermoAceite { get; set; }

        public string TermoAceite { get; set; }

        public bool? FlagQA { get; set; }

        public virtual Clientes Clientes { get; set; }
    }
}
