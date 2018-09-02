namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClienteAcesso")]
    public partial class ClienteAcesso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClienteAcesso()
        {
            ClienteAcessoLoja = new HashSet<ClienteAcessoLoja>();
            ClienteAcessoPerfil = new HashSet<ClienteAcessoPerfil>();
        }

        public int Id { get; set; }

        public int IdCliente { get; set; }
        

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Senha { get; set; }

        [StringLength(100)]
        public string Departamento { get; set; }

        public DateTime? DtUltimoAcesso { get; set; }

        public bool FlagStatus { get; set; }

        public bool PrimeiroAcesso { get; set; }

        public bool UsuarioMaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClienteAcessoLoja> ClienteAcessoLoja { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClienteAcessoPerfil> ClienteAcessoPerfil { get; set; }

        public virtual Clientes Clientes { get; set; }
    }
}
