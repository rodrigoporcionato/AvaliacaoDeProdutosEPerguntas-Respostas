using BetaViews.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaViews.Messages.Models;
using BetaViews.Core.DataBase.Repository.Interface;
using System.Threading.Tasks;
using BetaViews.Messages.SendReceiver.QA.ListarPergunta;
using BetaViews.Messages.Dtos;
using PagedList;
using BetaViews.Messages.SendReceiver.QA.Moderacao;
using BetaViews.Messages.SendReceiver.Clientes;
using System.Web.UI;

namespace BetaViews.Admin.Controllers.Moderacao
{

    [CustomAuthorize(PaginaAcessoEnum.Moderacao_PerguntasERespostas)]
    public class Moderacao_PerguntasERespostasController : Controller
    {
        private readonly IQARepository _QAService;
        private readonly IClienteRepository _clienteService;
        public Moderacao_PerguntasERespostasController(IQARepository _QAService, IClienteRepository _clienteService)
        {
            this._QAService = _QAService;
            this._clienteService = _clienteService;
        }

        [OutputCache(Duration = 360, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
        public async Task<ActionResult> Index(int? page, string busca)
        {
            await PopulaBadges();

            var response = new ListarPerguntaRS();
            response.Pagination = new PaginationDTO();
            response.Perguntas = new List<QAProdutoDTO>();
            response.Pagination.ActualPageNumber = page ?? 1;
            response.Pagination.RowsPerPage = 25;

            response = await _QAService.ListarPerguntaAdmin(new ListarPerguntaRQ
            {
                ActualPageNumber = response.Pagination.ActualPageNumber,
                IdCliente = AppUserManager.Usuario.IdCliente,
                Busca = busca
            });

            response.Pagination.ActualPageNumber = page ?? 1;

            var result = new StaticPagedList<QAProdutoDTO>(response.Perguntas.ToList(), response.Pagination.ActualPageNumber, 25, response.Pagination.TotalRows);
            ViewBag.TotalRows = response.Pagination.TotalRows;
            ViewBag.Avaliacoes = result;

            
            
            return View(result);
        }

        private async Task PopulaBadges() {

            var request = new ListarBadgeRQ();
            request.IdCliente = AppUserManager.Usuario.IdCliente;
            var response = new ListarBadgeRS();
            response.Badges = new List<BadgeDTO>();

            response = await _clienteService.ListarBadgeCliente(request);

            ViewBag.Badges = response.Badges;

        }

        public async Task<ActionResult> ResponderCliente(QAModeracaoResponderClienteRQ request)
        {

            if (request==null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            try
            {
                request.IdClienteAcesso = AppUserManager.Usuario.IdUsuario;

                await _QAService.ModeracaoResponderCliente(request);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        public async Task<ActionResult> AprovarRepostaTerceiro(int idQuestion) {

            try
            {
                if (idQuestion > 0)
                {
                    var resposta = await _QAService.GetByIdAsync(idQuestion);
                    resposta.IdQAStatus = 5;
                    resposta.IdClienteAcesso = 0;                    
                    resposta.NomeRespondente = resposta.RespTerceiroClienteNome;
                    await _QAService.EditAsync(resposta, idQuestion);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


            

        }

    }
}