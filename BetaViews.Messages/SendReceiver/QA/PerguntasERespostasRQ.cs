

using BetaViews.Messages.Dtos;

namespace BetaViews.Messages.SendReceiver.QA
{
    public class PerguntasERespostasRQ : TokenAuthorizationDTO
    {        
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string ClientePergunta { get; set; }
        public string ClienteLocalizacao { get; set; }


        public ProdutoDTO Produto{ get; set; }
        public LojaDTO Loja { get; set; }

    }
}
