using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages.Dtos
{
    public class ModeracaoProdutoAvaliacaoDTO
    {
        public LojaDTO Loja { get; set; }
        public ProdutoDTO Produto { get; set; }
        public AvaliacaoDTO Avaliacao { get; set; }

    }
}
