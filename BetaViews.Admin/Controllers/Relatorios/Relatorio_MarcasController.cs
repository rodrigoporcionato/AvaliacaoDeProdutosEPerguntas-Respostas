using BetaViews.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaViews.Messages.Models;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Messages.Dtos;
using System.Web.UI;

namespace BetaViews.Admin.Controllers.Relatorios
{

    [CustomAuthorize(PaginaAcessoEnum.Relatorio_Marcas)]
    public class Relatorio_MarcasController : Controller
    {
        private readonly IRelatoriosRepository relRepo;
        public Relatorio_MarcasController(IRelatoriosRepository relRepo)
        {
            this.relRepo = relRepo;
        }

        // GET: RelatorioMarcas        
        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Index(int? page)
        {
            var pagination = new PaginationDTO();
            pagination.ActualPageNumber = (page ?? 1);

            var model = new RelMarcas();            
            model.TopMaisAvaliados = new List<MarcasModel>();
            model.TopMenosAvaliados = new List<MarcasModel>();

            model = relRepo.RelMarcas(1,1);

            model.TopMaisAvaliados.ForEach(x =>
            {
                x.Marca = string.IsNullOrEmpty(x.Marca) ? "não informado na integração" : x.Marca;
            });

            model.TopMenosAvaliados.ForEach(x =>
            {
                x.Marca = string.IsNullOrEmpty(x.Marca) ? "não informado na integração" : x.Marca;
            });

            return View(model);
        }
    }
}