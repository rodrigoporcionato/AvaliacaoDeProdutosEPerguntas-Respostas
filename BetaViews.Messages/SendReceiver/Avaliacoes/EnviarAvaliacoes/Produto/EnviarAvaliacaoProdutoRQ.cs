
using BetaViews.Messages.Dtos;

namespace BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Produto
{

    
    public class EnviarAvaliacaoProdutoRQ : TokenAuthorizationDTO
    {
        public AvaliacaoEnvioDTO Avaliacao { get; set; }
        public LojaDTO Loja { get; set; }
        public ProdutoDTO Produto { get; set; }
    }

}
