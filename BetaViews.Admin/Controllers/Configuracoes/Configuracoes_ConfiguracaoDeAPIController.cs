using BetaViews.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaViews.Messages.Models;

namespace BetaViews.Admin.Controllers.Configuracoes
{

    [CustomAuthorize(PaginaAcessoEnum.Configuracoes_ConfiguracaoDeAPI)]
    public class Configuracoes_ConfiguracaoDeAPIController : Controller
    {
        // GET: Configuracoes_ConfiguracaoDeAPI
        public ActionResult Index()
        {
            return View();
        }
    }
}