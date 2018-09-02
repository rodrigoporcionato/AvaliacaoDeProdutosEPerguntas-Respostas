using BetaViews.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaViews.Messages.Models;
using BetaViews.Core.DataBase.Repository.Interface;
using System.Web.UI;
using BetaViews.Messages.Dtos;

namespace BetaViews.Admin.Controllers.Relatorios
{

    [CustomAuthorize(PaginaAcessoEnum.Relatorio_Categorias)]
    public class Relatorio_CategoriasController : Controller
    {
        private readonly IRelatoriosRepository relRepo;
        public Relatorio_CategoriasController(IRelatoriosRepository relRepo)
        {
            this.relRepo = relRepo;
        }

        // GET: RelatorioCategorias                
        [OutputCache(Duration = 180, Location = OutputCacheLocation.Server)]
        public ActionResult Index(int? page)
        {
            var pagination = new PaginationDTO();
            pagination.ActualPageNumber = (page ?? 1);

            var model = new RelDepartamentos();
            model.TopMaisAvaliados = new List<DepartamentosModel>();
            model.TopMenosAvaliados = new List<DepartamentosModel>();

            model = relRepo.RelDepartamentos(1, 1);

            model.TopMaisAvaliados.ForEach(x =>
            {
                x.Departamento = string.IsNullOrEmpty(x.Departamento) ? "não informado na integração" : x.Departamento;
            });

            model.TopMenosAvaliados.ForEach(x =>
            {
                x.Departamento = string.IsNullOrEmpty(x.Departamento) ? "não informado na integração" : x.Departamento;
            });

            return View(model);
        }


    }
}