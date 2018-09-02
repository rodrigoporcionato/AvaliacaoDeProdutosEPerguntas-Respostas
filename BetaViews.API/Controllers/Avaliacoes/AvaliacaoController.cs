
using BetaViews.API.Filters;
using BetaViews.Core.DataBase.Repository;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Core.Services.Avaliacoes.Loja;
using BetaViews.Messages.Enums;
using BetaViews.Messages.SendReceiver.Avaliacoes;
using BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Produto;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Produto;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace BetaViews.API.Controllers.Avaliacoes
{
 
    [RoutePrefix("avaliacoes")]
    public class AvaliacoesController : ApiController
    {
        private IAvaliacaoService _avaliacaoService;        
        public AvaliacoesController(IAvaliacaoService enviarAvaliacaoLojaService)
        {
            _avaliacaoService = enviarAvaliacaoLojaService;
        }

        
        //[APIAuthentication]
        [HttpPost]
        [Route("enviar/produto")]
        public async Task<EnviarAvaliacaoProdutoRS> EnviarAvaliacaoProduto(EnviarAvaliacaoProdutoRQ request)
        {
            try
            {
                if (request.Authorization == null)
                {
                    var token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                    request.Authorization = token;
                }

                var container = new UnityContainer();
                container.RegisterType<IClienteRepository, ClienteRepository>();
                var clienteService = container.Resolve<ClienteRepository>();
                if (!clienteService.CheckTokenAuthorization(request.Authorization))
                {
                    return new EnviarAvaliacaoProdutoRS
                    {
                        CodigoRetorno = 401,
                        Mensagens = new List<Messages.Mensagem> { new Messages.Mensagem { Descricao = "Token invalido", Tipo = Messages.TipoMensagem.ErroAplicacao, CodigoErro = "-1" } },
                        Valido = false,
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    };
                }

                return await _avaliacaoService.AdicionarAvaliacaoProduto(request);                
                
            }
            catch (Exception ex)
            {
                return new EnviarAvaliacaoProdutoRS
                {
                    CodigoRetorno = 400,
                    Mensagens = new List<Messages.Mensagem> {
                         new Messages.Mensagem { Descricao= ex.Message, Tipo= Messages.TipoMensagem.ErroAplicacao, CodigoErro="-1" }
                    },
                    Valido = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }


        }


        [APIAuthentication]
        [HttpPost]
        [Route("enviar/loja")]
        public async Task<EnviarAvaliacaoLojaRS> EnviarAvaliacaoLoja(EnviarAvaliacaoLojaRQ request)
        {
            var token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            request.Authorization = token;

            return await _avaliacaoService.AdicionarAvaliacaoLoja(request);
        }






        [APIAuthentication]
        [HttpGet()]
        //[CacheOutput(ClientTimeSpan = 20, ServerTimeSpan = 20)]
        [Route("obter/loja")]
        public async Task<IHttpActionResult> ListarAvaliacoesLojas([FromUri]ListarAvaliacoesLojasRQ request)
        {
            if (request == null) return BadRequest();

            try
            {
                Uri referrer = Request.Headers.Referrer;
                if (referrer != null)
                {
                    string domain = referrer.GetLeftPart(UriPartial.Authority);
                }

                var token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                request.Authorization = token;

                var avaliacoes = await _avaliacaoService.ListarAvaliacoesLojas(request);

                return Ok(avaliacoes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [APIAuthentication]
        [HttpGet()]
        //[CacheOutput(ClientTimeSpan = 20, ServerTimeSpan = 20)]
        [Route("obter/produto")]
        public async Task<IHttpActionResult> ListarAvaliacoesProdutos([FromUri]ListarAvaliacoesProdutosRQ request)
        {
            if (request == null) return BadRequest();

            try
            {
                Uri referrer = Request.Headers.Referrer;
                if (referrer != null)
                {
                    string domain = referrer.GetLeftPart(UriPartial.Authority);
                }
                var token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                request.Authorization = token;
                var avaliacoes = await _avaliacaoService.ListarAvaliacoesProdutos(request);

                return Ok(avaliacoes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    
        [Route("enviar/PostarComentarioPositivo")]
        [HttpPost]
        public async Task<IHttpActionResult> PostarComentarioPositivo(int idReview)
        {
            if (idReview==0)
            {
                return BadRequest("Não pode ser 0");
            }
            try
            {
                await _avaliacaoService.AtualizaAvaliacaoQtdes(idReview, TipoReportIntegracaoEnum.Gostei);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// post negative
        /// </summary>
        /// <param name="idReview"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("enviar/PostarComentarioNegativo")]
        public async Task<IHttpActionResult> PostarComentarioNegativo(int idReview)
        {
            if (idReview == 0)
            {
                return BadRequest("Não pode ser 0");
            }

            try
            {
                await _avaliacaoService.AtualizaAvaliacaoQtdes(idReview, TipoReportIntegracaoEnum.NaoGostei);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// report abuse
        /// </summary>
        /// <param name="idReview"></param>
        /// <returns></returns>
        [Route("enviar/ReportarAbuso")]
        [HttpPost]
        public async Task<IHttpActionResult> ReportarAbuso(int idReview)
        {
            if (idReview == 0)
            {
                return BadRequest("Não pode ser 0");
            }

            try
            {
                await _avaliacaoService.AtualizaAvaliacaoQtdes(idReview, TipoReportIntegracaoEnum.Abuso);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
