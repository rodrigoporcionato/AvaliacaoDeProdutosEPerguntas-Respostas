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

    [CustomAuthorize(PaginaAcessoEnum.Relatorio_RelatoriosDeProdutos)]
    public class Relatorio_RelatoriosDeProdutosController : Controller
    {
        private readonly PerfilAcessoLogado UserProfile = AppUserManager.Usuario;
        private readonly IProdutoRepository produtoRepo;
        private readonly IAvaliacaoRepository avaliacaoRepo;
        public Relatorio_RelatoriosDeProdutosController(IProdutoRepository produtoRepo, IAvaliacaoRepository avaliacaoRepo)
        {
            this.produtoRepo = produtoRepo;
            this.avaliacaoRepo = avaliacaoRepo;
        }

        // GET: Relatorio_RelatoriosDeProdutos
        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Index(int? page)
        {
            var pagination = new PaginationDTO();            
            pagination.ActualPageNumber = (page ?? 1);

            var model = new RelatoriosDeProdutosModel();
            model.Produtos = new List<ProdutoModel>();
            model.TopMaisAvaliado = new List<ProdutoModel>();
            model.TopMenosAvaliado = new List<ProdutoModel>();                       
            model  = produtoRepo.RelTopProdutos(1, pagination.ActualPageNumber);
            

            

            if (model!=null && model.Produtos.Any())
            {
                pagination.RowsPerPage = model.Produtos.FirstOrDefault().RowsPerPage;
                pagination.TotalRows = model.Produtos.FirstOrDefault().TotalRows;

                ViewBag.Produtos = new StaticPagedList<ProdutoModel>(model.Produtos.ToList(), pagination.ActualPageNumber, pagination.RowsPerPage, pagination.TotalRows);
            }
            else
            {
                model.Produtos = new List<ProdutoModel>();
                ViewBag.Produtos = new StaticPagedList<ProdutoModel>(model.Produtos.ToList(), 1, 1, pagination.TotalRows);
            }
                        
            return View(model);
        }
    }
}