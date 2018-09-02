
using BetaViews.Core.DataBase.Entitys;
using BetaViews.Core.Framework;
using BetaViews.Messages.Enums;
using BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Produto;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Produto;
using BetaViews.Messages.SendReceiver.Avaliacoes.Moderacao.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.Moderacao.Produto;
using BetaViews.Messages.SendReceiver.ModeracaoAuto;
using System.Threading.Tasks;

namespace BetaViews.Core.DataBase.Repository.Interface
{
    public interface IAvaliacaoRepository: IGenericRepository<Avaliacao>
    {

        //Task AtualizaTotaisProduto(string codProduto, int idCliente);

        Task<ModeracaoAutoRS> ListarAvaliacoesPendentes(ModeracaoAutoRQ request);

        Task<int> EnviarAvaliacaoLoja(EnviarAvaliacaoLojaRQ entity);
        Task<int> EnviarAvaliacaoProduto(EnviarAvaliacaoProdutoRQ entity);

        Task AtualizaAvaliacaoQtdes(int idReview, TipoReportIntegracaoEnum tipoReport);        
        Task AlterarStatusAvaliacao(int idAvaliacao, StatusAvaliacaoEnum idStatusAvaliacao, string motivo);

        Task<ModeracaoProdutoRS> ModeracaoProdutosListar(ModeracaoProdutoRQ request);
        Task<ModeracaoLojaRS> ModeracaoLojasListar(ModeracaoLojaRQ request);

        Task<ListarAvaliacoesProdutosRS> AvaliacoesProdutosListar(ListarAvaliacoesProdutosRQ request);
        Task<ListarAvaliacoesLojasRS> AvaliacoesLojasListar(ListarAvaliacoesLojasRQ request);

        Task<ModeracaoProdutoRS> AvaliacoesAdminProdutosListar(ModeracaoProdutoRQ request);
        Task<ModeracaoLojaRS> AvaliacoesAdminLojasListar(ModeracaoLojaRQ request);



    }
}



