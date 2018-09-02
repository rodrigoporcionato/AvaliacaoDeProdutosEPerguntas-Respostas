using BetaViews.Core.DataBase.Entitys;
using BetaViews.Core.Framework;
using BetaViews.Messages.Models;

namespace BetaViews.Core.DataBase.Repository.Interface
{
    public interface IProdutoRepository : IGenericRepository<Produto>
    {

        //ModeracaoProdutosListar ModeracaoProdutosListar(int IdCliente, int Page, string Search);

        RelatoriosDeProdutosModel RelTopProdutos(int? idCliente, int page);
    }
}