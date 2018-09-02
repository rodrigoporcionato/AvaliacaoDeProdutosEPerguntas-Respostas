namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clientes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clientes()
        {
            Badge = new HashSet<Badge>();
            ClienteAcesso = new HashSet<ClienteAcesso>();
            ClienteConfiguracao = new HashSet<ClienteConfiguracao>();
            Loja = new HashSet<Loja>();
            PalavraRecusadaPadraoCliente = new HashSet<PalavraRecusadaPadraoCliente>();
        }

        public int Id { get; set; }

        [Required]
        public string CodigoCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20)]
        public string CPFCNPJ { get; set; }

        [Required]
        [StringLength(100)]
        public string Endereco { get; set; }

        [StringLength(100)]
        public string EnderecoNumero { get; set; }

        [StringLength(50)]
        public string Bairro { get; set; }

        [StringLength(50)]
        public string Cidade { get; set; }

        [StringLength(10)]
        public string CEP { get; set; }

        public bool FlagAtivo { get; set; }

        public DateTime DtCadastro { get; set; }

        [StringLength(200)]
        public string APIKey { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Badge> Badge { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClienteAcesso> ClienteAcesso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClienteConfiguracao> ClienteConfiguracao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Loja> Loja { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PalavraRecusadaPadraoCliente> PalavraRecusadaPadraoCliente { get; set; }
    }
}
