using BetaViews.Admin.Filters;

using System.Web.Mvc;
using BetaViews.Messages.Models;

namespace BetaViews.Admin.Controllers.Configuracoes
{

    [CustomAuthorize(PaginaAcessoEnum.Configuracoes_ConfiguracaoDeLoja)]
    public class Configuracoes_ConfiguracaoDeLojaController : Controller
    {
        // GET: Configuracoes_ConfiguracaoDeLoja
        public ActionResult Index()
        {
            return View();
        }
    }
}