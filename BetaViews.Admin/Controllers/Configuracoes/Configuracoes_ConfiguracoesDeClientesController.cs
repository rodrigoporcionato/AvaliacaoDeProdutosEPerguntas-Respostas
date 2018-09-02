using BetaViews.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaViews.Messages.Models;

namespace BetaViews.Admin.Controllers.Configuracoes
{

    [CustomAuthorize(PaginaAcessoEnum.Configuracoes_ConfiguracoesDeClientes)]
    public class Configuracoes_ConfiguracoesDeClientesController : Controller
    {
        // GET: Configuracoes_ConfiguracoesDeClientes
        public ActionResult Index()
        {
            return View();
        }
    }
}