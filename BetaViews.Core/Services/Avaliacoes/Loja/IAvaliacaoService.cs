using BetaViews.Messages.Enums;
using BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Produto;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Produto;
using System.Threading.Tasks;

namespace BetaViews.Core.Services.Avaliacoes.Loja
{
    public interface IAvaliacaoService
    {

        Task<EnviarAvaliacaoProdutoRS> AdicionarAvaliacaoProduto(EnviarAvaliacaoProdutoRQ request);

        Task<EnviarAvaliacaoLojaRS> AdicionarAvaliacaoLoja(EnviarAvaliacaoLojaRQ request);

        Task AtualizaAvaliacaoQtdes(int idReview, TipoReportIntegracaoEnum tipoReport);

        //Task<AvaliacoesDisplayRS> ListarAvaliacoesPagina(AvaliacoesDisplayRQ request);

        Task<ListarAvaliacoesLojasRS> ListarAvaliacoesLojas(ListarAvaliacoesLojasRQ request);

        Task<ListarAvaliacoesProdutosRS> ListarAvaliacoesProdutos(ListarAvaliacoesProdutosRQ request);


    }
}