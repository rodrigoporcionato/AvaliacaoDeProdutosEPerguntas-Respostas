﻿using BetaViews.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaViews.Messages.Models;
using BetaViews.Core.DataBase.Repository.Interface;
using System.Threading.Tasks;
using BetaViews.Messages.SendReceiver.Avaliacoes.Moderacao.Loja;
using BetaViews.Messages.Dtos;
using PagedList;
using System.Web.UI;

namespace BetaViews.Admin.Controllers.Moderacao
{

    [CustomAuthorize(PaginaAcessoEnum.Moderacao_ModeracaoLojasEMarketPlace)]    
    public class Moderacao_ModeracaoLojasEMarketPlaceController : Controller
    {

        // GET: Moderacao_ModeracaoLojasEMarketPlace        
        private readonly IAvaliacaoRepository _AvaliacaoService;
        public Moderacao_ModeracaoLojasEMarketPlaceController(IAvaliacaoRepository _AvaliacaoService)
        {
            this._AvaliacaoService = _AvaliacaoService;
        }

        [OutputCache(Duration = 360, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
        public async Task<ActionResult> Index(int? page, string busca)
        {
            var response = new ModeracaoLojaRS();
            response.Pagination = new PaginationDTO();
            response.Avaliacoes = new List<ModeracaoLojaAvaliacaoDTO>();
            response.Pagination.ActualPageNumber = page ?? 1;
            response.Pagination.RowsPerPage = 25;

            response = await _AvaliacaoService.ModeracaoLojasListar(new ModeracaoLojaRQ
            {
                ActualPageNumber = response.Pagination.ActualPageNumber,
                IdCliente = AppUserManager.Usuario.IdCliente,
                Busca = busca
            });
            response.Pagination.ActualPageNumber = page ?? 1;
            var aval = new StaticPagedList<ModeracaoLojaAvaliacaoDTO>(response.Avaliacoes.ToList(), response.Pagination.ActualPageNumber, 25, response.Pagination.TotalRows);
            ViewBag.TotalRows = response.Pagination.TotalRows;

            ViewBag.Avaliacoes = aval;

            return View(aval);
        }


        public async Task<ActionResult> AprovarAvaliacoes(List<int> avaliacoes, int idStatus)
        {
            try
            {
                if (avaliacoes.Any() && idStatus > 0)
                {
                    foreach (var x in avaliacoes)
                    {
                         await _AvaliacaoService.AlterarStatusAvaliacao(x, (Messages.Enums.StatusAvaliacaoEnum)idStatus, "alterado por usuário " + AppUserManager.Usuario.Nome);
                    }
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