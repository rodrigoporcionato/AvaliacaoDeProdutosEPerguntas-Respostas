using BetaViews.Messages.Dtos;


namespace BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Loja
{
    public class EnviarAvaliacaoLojaRQ : TokenAuthorizationDTO
    {
        public AvaliacaoEnvioDTO Avaliacao { get; set; }

        public LojaDTO Loja { get; set; }
    }
}
