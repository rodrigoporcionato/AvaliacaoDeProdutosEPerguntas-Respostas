using System.Collections.Generic;

namespace BetaViews.Messages.Models
{
    /// <summary>
    /// Define os acessos no sistema
    /// </summary>
    public class PerfilAcessoLogado
    {
        public int IdUsuario { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }
        public bool UsuarioMaster { get; set; }        
        public int IdCliente { get; set; }

        /// <summary>
        /// se exister loja, então retringe as lojas que pode ver.
        /// </summary>
        public List<int> LojaRestricao { get; set; }
        /// <summary>
        /// verifica se tem acesso a pagina
        /// </summary>
        public List<int> PaginaAcesso { get; set; }

    }
}