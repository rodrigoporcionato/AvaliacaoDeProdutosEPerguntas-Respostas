using System.Collections.Generic;
using System.Web.Mvc;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Produto;
using RestSharp;

namespace BetaViews.Static.Controllers
{
    public class AvaliacaoProdutoController : Controller
    {
        // GET: AvaliacaoProduto
        public ActionResult Index(ListarAvaliacoesProdutosRQ rq)
        {
            if (rq.ActualPageNumber == 0) rq.ActualPageNumber = 1;

            var model = new ListarAvaliacoesProdutosRS();
            model.Avaliacoes = new List<Messages.Dtos.AvaliacaoDTO>();
            model.AvaliacaoGeral = new Messages.Dtos.AvaliacaoTotaisDTO();
            model.AvaliacaoNegativaMaisUtil = new Messages.Dtos.AvaliacaoDTO();            

            var client = new RestClient("http://api.betaviews.com.br/Avaliacoes/");

            var request = new RestRequest("obter/produto/", Method.GET);
            request.AddParameter("prdCodigo", rq.PrdCodigo);
            request.AddParameter("codigoLoja", rq.CodigoLoja);
            request.AddParameter("actualPageNumber", rq.ActualPageNumber);
            request.AddParameter("Filtro", rq.Filtro);
            request.AddParameter("busca", rq.Busca);

            request.AddHeader("Authorization", rq.Authorization);
            var response = client.Execute<ListarAvaliacoesProdutosRS>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                model = response.Data;
                //model.AvaliacaoGeral.AvaliacaoGeral = StringExtensions.RoundScore(model.AvaliacaoGeral.AvaliacaoGeral);
            }
            return View(model);
        }
    }
}