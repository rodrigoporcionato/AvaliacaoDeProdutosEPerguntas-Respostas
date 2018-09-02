using BetaViews.Messages.Dtos;
using System.Runtime.Serialization;

namespace BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Loja
{
    public class ListarAvaliacoesLojasRQ: TokenAuthorizationDTO
    {        
        public string CodigoLoja { get; set; }

        public int ActualPageNumber { get; set; }

        public string Filtro { get; set; }

        public string Busca { get; set; }
    }
}
