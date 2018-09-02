namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PaginaAcesso")]
    public partial class PaginaAcesso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PaginaAcesso()
        {
            ClienteAcessoPerfil = new HashSet<ClienteAcessoPerfil>();
        }

        public int Id { get; set; }

        [Required]
        public string Modulo { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClienteAcessoPerfil> ClienteAcessoPerfil { get; set; }
    }
}
