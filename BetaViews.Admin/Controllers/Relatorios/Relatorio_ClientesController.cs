using BetaViews.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaViews.Messages.Models;

namespace BetaViews.Admin.Controllers.Relatorios
{

    [CustomAuthorize(PaginaAcessoEnum.Relatorio_Clientes)]
    public class Relatorio_ClientesController : Controller
    {
        // GET: RelatorioClientes
        public ActionResult Index()
        {
            return View();
        }
    }
}