namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClienteAcessoPerfil")]
    public partial class ClienteAcessoPerfil
    {
        public int Id { get; set; }

        public int IdClienteAcesso { get; set; }

        public int IdPaginaAcesso { get; set; }

        public virtual ClienteAcesso ClienteAcesso { get; set; }

        public virtual PaginaAcesso PaginaAcesso { get; set; }
    }
}
