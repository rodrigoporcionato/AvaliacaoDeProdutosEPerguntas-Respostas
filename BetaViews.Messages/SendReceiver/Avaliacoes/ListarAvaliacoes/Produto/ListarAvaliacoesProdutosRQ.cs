using BetaViews.Messages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Produto
{
    public class ListarAvaliacoesProdutosRQ : TokenAuthorizationDTO
    {

        public int ActualPageNumber { get; set; }

        public string CodigoLoja { get; set; }

        public string PrdCodigo { get; set; }

        public string Filtro { get; set; }

        public string Busca { get; set; }

    }
}
