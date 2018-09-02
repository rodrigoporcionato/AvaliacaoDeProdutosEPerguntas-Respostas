using BetaViews.Messages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages.SendReceiver.Avaliacoes.Moderacao.Produto
{
    public class ModeracaoProdutoRS : BaseMessageResponse
    {
        public PaginationDTO Pagination { get; set; }
        public List<ModeracaoProdutoAvaliacaoDTO> Avaliacoes { get; set; }
    }
}
