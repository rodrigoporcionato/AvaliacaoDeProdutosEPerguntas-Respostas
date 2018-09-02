using BetaViews.Messages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Produto
{
    public class ListarAvaliacoesProdutosRS : BaseMessageResponse
    {
        public PaginationDTO Pagination { get; set; }
        public LojaDTO Loja { get; set; }
        public ProdutoDTO Produto { get; set; }
        public AvaliacaoTotaisDTO AvaliacaoGeral { get; set; }
        public List<AvaliacaoDTO> Avaliacoes { get; set; }
        public AvaliacaoDTO AvaliacaoPositivaMaisUtil { get; set; }
        public AvaliacaoDTO AvaliacaoNegativaMaisUtil { get; set; }
        

    }
}
