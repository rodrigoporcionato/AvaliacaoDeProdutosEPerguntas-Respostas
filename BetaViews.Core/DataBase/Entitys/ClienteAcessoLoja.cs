namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClienteAcessoLoja")]
    public partial class ClienteAcessoLoja
    {
        public int Id { get; set; }

        public int IdClienteAcesso { get; set; }

        public int IdLoja { get; set; }

        public virtual ClienteAcesso ClienteAcesso { get; set; }

        public virtual Loja Loja { get; set; }
    }
}
