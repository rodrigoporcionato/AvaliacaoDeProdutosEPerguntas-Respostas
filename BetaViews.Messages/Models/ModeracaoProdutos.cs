using BetaViews.Messages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages.Models
{
    public class ModeracaoProdutos: PaginationDTO
    {
        List<AvaliacaoProdutoModel> Avaliacoes { get; set; }
    }
}
