using BetaViews.Messages.Models;
using System.Linq;

namespace BetaViews.Admin
{
    public static class AppUserManager
    {
        public static PerfilAcessoLogado Usuario { get; set; }


        public static bool VerificaAcessoPagina(PaginaAcessoEnum pagina)
        {
            if (Usuario.PaginaAcesso.Where(x=> x == (int)pagina).Any())
            {
                return true;
            }

            return false;            
        }

    }
}