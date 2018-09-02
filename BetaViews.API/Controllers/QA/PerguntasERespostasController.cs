using BetaViews.API.Filters;
using BetaViews.Core.Services.QA;
using BetaViews.Messages.SendReceiver.QA;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace BetaViews.API.Controllers.QA
{    
    [RoutePrefix("PerguntasERespostas")]
    public class PerguntasERespostasController : ApiController
    {
        private IQAService _qaService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="qaService"></param>
        public PerguntasERespostasController(IQAService qaService)
        {
            _qaService = qaService;
        }

        /// <summary>
        /// EnviarPergunta sobre um determinado produto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        
        [HttpPost()]
        [Route("EnviarDuvida")]
        public async Task<PerguntasERespostasRS> EnviarPergunta(PerguntasERespostasRQ request)
        {

            try
            {
                if (request.Authorization == null)
                {
                    var token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                    request.Authorization = token;
                }

                return await _qaService.EnviarPergunta(request);
            }
            catch (Exception)
            {
                return new PerguntasERespostasRS
                {
                    CodigoRetorno = 400,
                    Mensagens = new List<Messages.Mensagem> {
                         new Messages.Mensagem { Descricao="Token invalido", Tipo= Messages.TipoMensagem.ErroAplicacao, CodigoErro="-1" }
                    },
                    Valido = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            

        }


        /// <summary>
        /// Obtem resposta de perguntas de terceiros
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Retorna 200 quando encontrado Perguntas e Respostas para o Produto</response>
        /// <response code="400">Se request não for informado</response>
        [HttpPost]
        [Route("RespostaTerceiro")]
        public async Task<IHttpActionResult> RespostaTerceiro(PerguntasERespostasRespTerceiroRQ request) {

            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _qaService.RespostaTerceiro(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);                
            }            
        }


        /// <summary>
        /// Obtem Perguntas e Respostas de um produto na sua loja.
        /// </summary>
        /// <remarks>Retorna lista de Perguntas e Respostas do produto na sua loja</remarks>       
        /// <returns>Retorna uma lista de Perguntas e Respostas de um produto</returns>
        /// <response code="200">Retorna 200 quando encontrado Perguntas e Respostas para o Produto</response>
        /// <response code="400">Se request não for informado</response>
        /// <param name="request">
        /// </param>
        /// <returns></returns>
        [HttpGet()]
        //[CacheOutput(ClientTimeSpan = 20, ServerTimeSpan = 20)]
        [Route("ObterDuvidas")]        
        public async Task<IHttpActionResult> ObterDuvidas([FromUri]PerguntasERespostasDisplayRQ request)
        {
            if (request== null)
            {
                return BadRequest();
            }

            try
            {                                
                var result = await _qaService.ObterDuvidas(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
