using BetaViews.Admin.Filters;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Messages.Dtos;
using BetaViews.Messages.Models;
using BetaViews.Messages.SendReceiver.Avaliacoes.Moderacao.Produto;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace BetaViews.Admin.Controllers.Avaliacao
{

    [CustomAuthorize(PaginaAcessoEnum.Avaliacao_AvaliacaoDeProdutos)]
    public class Avaliacao_AvaliacaoDeProdutosController : Controller
    {
        private readonly IAvaliacaoRepository _AvaliacaoService;
        public Avaliacao_AvaliacaoDeProdutosController(IAvaliacaoRepository _AvaliacaoService)
        {
            this._AvaliacaoService = _AvaliacaoService;
        }

        [OutputCache(Duration = 360, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]        
        public async Task<ActionResult> Index(int? page, string busca)
        {
            var response = new ModeracaoProdutoRS();
            response.Pagination = new PaginationDTO();
            response.Avaliacoes = new List<ModeracaoProdutoAvaliacaoDTO>();
            response.Pagination.ActualPageNumber = page ?? 1;
            response.Pagination.RowsPerPage = 25;

            response = await _AvaliacaoService.AvaliacoesAdminProdutosListar(new ModeracaoProdutoRQ
            {
                ActualPageNumber = response.Pagination.ActualPageNumber,
                IdCliente = AppUserManager.Usuario.IdCliente,
                Busca = busca
            });
            response.Pagination.ActualPageNumber = page ?? 1;
            var aval = new StaticPagedList<ModeracaoProdutoAvaliacaoDTO>(response.Avaliacoes.ToList(), response.Pagination.ActualPageNumber, 25, response.Pagination.TotalRows);
            ViewBag.TotalRows = response.Pagination.TotalRows;
            ViewBag.Avaliacoes = aval;

            return View(aval);
        }


        public async Task<ActionResult> RemoverAvaliacao(int IdAvaliacao, int idStatus)
        {
            try
            {
                if (IdAvaliacao>0 && idStatus > 0)
                {
                    await _AvaliacaoService.AlterarStatusAvaliacao(IdAvaliacao, (Messages.Enums.StatusAvaliacaoEnum)idStatus, "removido por usuário " + AppUserManager.Usuario.Nome);                    
                }
            }
            catch (Exception ex)
            {
                //logService.AdicionarLogErro("ModeracaoAprovarAvaliacoes", Guid.NewGuid().ToString(), CodigoMensagem.CPM_ERRO_APLICAO.CodigoErro,
                //CodigoMensagem.ERRO_APLICAO.SetFormat(string.Format("MENSAGEM={0}\nINNER_EXCEPTION={1}\nSTACK_TRACE={2}", ex.Message, (ex.InnerException != null ? ex.InnerException.ToString() : ""), ex.StackTrace.ToString())).Descricao, TipoMensagem.ErroAplicacao);

                return Json(new { erro = true });
            }
            return Json(new { erro = false });
        }



    }
}