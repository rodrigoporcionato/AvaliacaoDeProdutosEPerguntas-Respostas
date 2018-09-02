using BetaViews.Core.Services.Avaliacoes.Loja;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BetaViews.API.Controllers.Moderacao.Loja
{
    [RoutePrefix("Moderacao/Loja")]
    public class ModeracaoAutomaticaController : ApiController
    {

        private IModeracaoService moderacaoLojaService;
        public ModeracaoAutomaticaController(IModeracaoService moderacaoLojaService)
        {
            this.moderacaoLojaService = moderacaoLojaService;
        }


        [HttpPost]
        [Route("ExecutarModeracaoLojaAuto")]
        public async Task<string> EnviarAvaliacaoLoja()
        {
            try
            {
                var result = "";
                result += "QA: "+ await moderacaoLojaService.ExecutaModeracaoQA();
                result += "AVALIACAO: " + await moderacaoLojaService.ExecutaModeracaoAvaliacoes();

                return result;
                
            }
            catch (Exception ex)
            {
                throw ex;                
            }
            return "falha";
        }



    }
}
