using BetaViews.Messages.Dtos;
using BetaViews.Messages.SendReceiver.QA;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetaViews.Static.Controllers
{
    public class QAController : Controller
    {
        
        // GET: QA
        public ActionResult Index(PerguntasERespostasDisplayRQ rq)
        {
            var model = new PerguntasERespostasDisplayRS();
            model.Produto = new ProdutoDTO();
            model.QA = new List<Messages.Dtos.QA.QADTO>();            

            var client = new RestClient("http://api.betaviews.com.br/PerguntasERespostas/");

            var request = new RestRequest("ObterDuvidas/", Method.GET);
            request.AddParameter("prdCodigo", rq.PrdCodigo);
            request.AddParameter("CodigoLoja", rq.CodigoLoja);
            request.AddParameter("codCliente", rq.CodCliente);
            request.AddParameter("PageNumber", rq.PageNumber);
            request.AddParameter("Filter", rq.Filter);
            request.AddParameter("Busca", rq.Busca);

            request.AddHeader("Authorization", rq.Authorization);

            var response = client.Execute<PerguntasERespostasDisplayRS>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                model = response.Data;                
                ViewBag.ClientSerachFor = rq.Busca;
            }

            


            return View(model);
        }
    }
}