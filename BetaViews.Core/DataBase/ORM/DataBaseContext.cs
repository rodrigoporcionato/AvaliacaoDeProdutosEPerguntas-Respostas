using BetaViews.Core.DataBase.Entitys;
using BetaViews.Core.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace BetaViews.Core.DataBase.ORM
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext() : base("name=BetaViewsContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }


        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
                //Write to file
                var configEFDebug = !string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["EnableEFDebug"]) ?
                    System.Configuration.ConfigurationManager.AppSettings["EnableEFDebug"] : "";
                var EnableEFDebug = configEFDebug.Equals("true") ? true : false;

                if (EnableEFDebug) System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                throw;

                // Showing it on screen
                throw new Exception(string.Join(",", outputLines.ToArray()));

            }
            catch (DbUpdateException E)
            {
                var sqlException = E.InnerException.InnerException as SqlException;
                if (sqlException != null && sqlException.Number == 2601)
                    throw new RepeatedKeyException(E);
                throw;
            }
        }

        public override async Task<int> SaveChangesAsync()
        {
            var outputLines = new List<string>();

            try
            {
                return await base.SaveChangesAsync();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
                //Write to file
                var configEFDebug = !string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["EnableEFDebug"]) ?
                    System.Configuration.ConfigurationManager.AppSettings["EnableEFDebug"] : "";

                var EnableEFDebug = configEFDebug.Equals("true") ? true : false;

                if (EnableEFDebug) System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);


                throw;

                // Showing it on screen
                throw new Exception(string.Join(",", outputLines.ToArray()));

            }
            catch (DbUpdateException E)
            {
                var sqlException = E.InnerException.InnerException as SqlException;
                if (sqlException != null && sqlException.Number == 2601)
                    throw new RepeatedKeyException(E);
                throw;
            }
        }


        public virtual DbSet<Avaliacao> Avaliacao { get; set; }
        public virtual DbSet<AvaliacaoProduto> AvaliacaoProduto { get; set; }
        public virtual DbSet<AvaliacaoStatus> AvaliacaoStatus { get; set; }
        public virtual DbSet<Badge> Badge { get; set; }
        public virtual DbSet<ClienteAcesso> ClienteAcesso { get; set; }
        public virtual DbSet<ClienteAcessoLoja> ClienteAcessoLoja { get; set; }
        public virtual DbSet<ClienteAcessoPerfil> ClienteAcessoPerfil { get; set; }
        public virtual DbSet<ClienteConfiguracao> ClienteConfiguracao { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<DadosConfiguracao> DadosConfiguracao { get; set; }
        public virtual DbSet<LogErros> LogErros { get; set; }
        public virtual DbSet<Loja> Loja { get; set; }
        public virtual DbSet<LojaConfiguracao> LojaConfiguracao { get; set; }
        public virtual DbSet<NPSLoja> NPSLoja { get; set; }
        public virtual DbSet<PaginaAcesso> PaginaAcesso { get; set; }
        public virtual DbSet<PalavraRecusadaPadrao> PalavraRecusadaPadrao { get; set; }
        public virtual DbSet<PalavraRecusadaPadraoCliente> PalavraRecusadaPadraoCliente { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<QA> QA { get; set; }
        public virtual DbSet<QAStatus> QAStatus { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            modelBuilder.Entity<Avaliacao>()
                .Property(e => e.ClienteNome)
                .IsUnicode(false);

            modelBuilder.Entity<Avaliacao>()
                .Property(e => e.ClienteEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Avaliacao>()
                .Property(e => e.ClienteComentario)
                .IsUnicode(false);

            modelBuilder.Entity<Avaliacao>()
                .HasMany(e => e.AvaliacaoProduto)
                .WithRequired(e => e.Avaliacao)
                .HasForeignKey(e => e.IdAvaliacao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AvaliacaoStatus>()
                .HasMany(e => e.Avaliacao)
                .WithRequired(e => e.AvaliacaoStatus)
                .HasForeignKey(e => e.IdAvaliacaoStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClienteAcesso>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<ClienteAcesso>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ClienteAcesso>()
                .Property(e => e.Senha)
                .IsUnicode(false);

            modelBuilder.Entity<ClienteAcesso>()
                .HasMany(e => e.ClienteAcessoLoja)
                .WithRequired(e => e.ClienteAcesso)
                .HasForeignKey(e => e.IdClienteAcesso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClienteAcesso>()
                .HasMany(e => e.ClienteAcessoPerfil)
                .WithRequired(e => e.ClienteAcesso)
                .HasForeignKey(e => e.IdClienteAcesso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.CPFCNPJ)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Endereco)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.EnderecoNumero)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Bairro)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Cidade)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.CEP)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.APIKey)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .HasMany(e => e.Badge)
                .WithRequired(e => e.Clientes)
                .HasForeignKey(e => e.IdCliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clientes>()
                .HasMany(e => e.ClienteAcesso)
                .WithRequired(e => e.Clientes)
                .HasForeignKey(e => e.IdCliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clientes>()
                .HasMany(e => e.ClienteConfiguracao)
                .WithRequired(e => e.Clientes)
                .HasForeignKey(e => e.IdCliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clientes>()
                .HasMany(e => e.Loja)
                .WithRequired(e => e.Clientes)
                .HasForeignKey(e => e.IdCliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clientes>()
                .HasMany(e => e.PalavraRecusadaPadraoCliente)
                .WithRequired(e => e.Clientes)
                .HasForeignKey(e => e.IdCliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LogErros>()
                .Property(e => e.Interface)
                .IsUnicode(false);

            modelBuilder.Entity<LogErros>()
                .Property(e => e.ProtocoloRetorno)
                .IsUnicode(false);

            modelBuilder.Entity<LogErros>()
                .Property(e => e.CodigoErro)
                .IsUnicode(false);

            modelBuilder.Entity<LogErros>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .Property(e => e.CPFCNPJ)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .Property(e => e.Endereco)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .Property(e => e.EnderecoNumero)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .Property(e => e.Bairro)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .Property(e => e.Cidade)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .Property(e => e.CEP)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .Property(e => e.Logo)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .HasMany(e => e.Avaliacao)
                .WithRequired(e => e.Loja)
                .HasForeignKey(e => e.IdLoja)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Loja>()
                .HasMany(e => e.ClienteAcessoLoja)
                .WithRequired(e => e.Loja)
                .HasForeignKey(e => e.IdLoja)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Loja>()
                .HasMany(e => e.LojaConfiguracao)
                .WithRequired(e => e.Loja)
                .HasForeignKey(e => e.IdLoja)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Loja>()
                .HasMany(e => e.NPSLoja)
                .WithRequired(e => e.Loja)
                .HasForeignKey(e => e.IdLoja)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Loja>()
                .HasMany(e => e.Produto)
                .WithRequired(e => e.Loja)
                .HasForeignKey(e => e.IdLoja)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PaginaAcesso>()
                .Property(e => e.Modulo)
                .IsUnicode(false);

            modelBuilder.Entity<PaginaAcesso>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<PaginaAcesso>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<PaginaAcesso>()
                .HasMany(e => e.ClienteAcessoPerfil)
                .WithRequired(e => e.PaginaAcesso)
                .HasForeignKey(e => e.IdPaginaAcesso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PalavraRecusadaPadrao>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<PalavraRecusadaPadrao>()
                .Property(e => e.CodigoPais)
                .IsUnicode(false);

            modelBuilder.Entity<PalavraRecusadaPadraoCliente>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Produto>()
                .HasMany(e => e.AvaliacaoProduto)
                .WithRequired(e => e.Produto)
                .HasForeignKey(e => e.IdProduto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Produto>()
                .HasMany(e => e.QA)
                .WithRequired(e => e.Produto)
                .HasForeignKey(e => e.IdProduto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QA>()
                .Property(e => e.ClienteNome)
                .IsUnicode(false);

            modelBuilder.Entity<QA>()
                .Property(e => e.ClienteEmail)
                .IsUnicode(false);

            modelBuilder.Entity<QA>()
                .Property(e => e.RespTerceiroClienteNome)
                .IsUnicode(false);

            modelBuilder.Entity<QA>()
                .Property(e => e.RespTerceiroClienteEmail)
                .IsUnicode(false);

            modelBuilder.Entity<QA>()
                .Property(e => e.RespTerceiroClienteLocalizacao)
                .IsUnicode(false);

            modelBuilder.Entity<QAStatus>()
                .HasMany(e => e.QA)
                .WithRequired(e => e.QAStatus)
                .HasForeignKey(e => e.IdQAStatus)
                .WillCascadeOnDelete(false);
        }





    }
}
