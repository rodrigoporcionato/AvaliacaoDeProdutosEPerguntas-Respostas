using BetaViews.Admin.Filters;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Messages.Dtos;
using BetaViews.Messages.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace BetaViews.Admin.Controllers.Relatorios
{

    [CustomAuthorize(PaginaAcessoEnum.Relatorio_RelatoriosDeLojasEMarketPlace)]
    public class Relatorio_RelatoriosDeLojasEMarketPlaceController : Controller
    {
        
        private readonly PerfilAcessoLogado UserProfile = AppUserManager.Usuario;
        private readonly IRelatoriosRepository relRepo;
        private readonly IAvaliacaoRepository avaliacaoRepo;
        public Relatorio_RelatoriosDeLojasEMarketPlaceController(IRelatoriosRepository relRepo, IAvaliacaoRepository avaliacaoRepo)
        {
            this.relRepo = relRepo;
            this.avaliacaoRepo = avaliacaoRepo;
        }

        //get Relatorio_RelatoriosDeLojasEMarketPlace
        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Index(int? page)
        {
            var pagination = new PaginationDTO();
            pagination.ActualPageNumber = (page ?? 1);

            var model = new RelatoriosDeLojas();
            model.Lojas = new List<LojaModel>();
            model.TopMaisAvaliado = new List<LojaModel>();
            model.TopMenosAvaliado = new List<LojaModel>();
            model = relRepo.RelTopLojas(AppUserManager.Usuario.IdCliente,1);

            if (model.Lojas != null && model.Lojas.Any())
            {
                pagination.RowsPerPage = model.Lojas.FirstOrDefault().RowsPerPage;
                pagination.TotalRows = model.Lojas.FirstOrDefault().TotalRows;

                var lojas = new StaticPagedList<LojaModel>(model.Lojas.ToList(), pagination.ActualPageNumber, pagination.RowsPerPage, pagination.TotalRows);
                ViewBag.Lojas = lojas;
            }
            else
            {
                ViewBag.Lojas = new List<LojaModel>(); ;
                ViewBag.Lojas = new StaticPagedList<LojaModel>(model.Lojas.ToList(), 1, 1, pagination.TotalRows);
            }

            return View(model);
        }
    }
}