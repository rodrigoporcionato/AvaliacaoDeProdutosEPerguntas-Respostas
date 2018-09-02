using BetaViews.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaViews.Messages.Models;

namespace BetaViews.Admin.Controllers.Configuracoes
{
    [CustomAuthorize(PaginaAcessoEnum.Configuracoes_ConfiguracoesGerais)]
    public class Configuracoes_ConfiguracoesGeraisController : Controller
    {
        // GET: Configuracoes_ConfiguracoesGerais
        public ActionResult Index()
        {
            return View();
        }
    }
}