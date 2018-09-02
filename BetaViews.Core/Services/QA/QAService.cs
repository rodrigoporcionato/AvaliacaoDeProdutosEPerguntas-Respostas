using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetaViews.Messages.SendReceiver.QA;
using BetaViews.Messages;
using Newtonsoft.Json;
using BetaViews.Core.Services.Logs;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Core.Framework.Extension;

namespace BetaViews.Core.Services.QA
{
    public class QAService : IQAService
    {
        private readonly ILogService logService;
        private readonly IClienteRepository clienteService;
        private readonly IQARepository qaRepository;
        public QAService(IQARepository qaRepository, IClienteRepository clienteService, ILogService logService)
        {
            this.qaRepository = qaRepository;
            this.clienteService = clienteService;
            this.logService = logService;
        }

        public async Task<PerguntasERespostasRS> EnviarPergunta(PerguntasERespostasRQ request)
        {
            PerguntasERespostasRS response = request.ValidarEnviarPergunta(clienteService);

            try
            {
                if (response.Valido)
                {
                    if (response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio))
                    {
                        var idRetorno = await qaRepository.AddQAAsync(request);
                        response.CodigoRetorno = idRetorno;
                    }
                }

            }
            catch (Exception ex)
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                logService.AdicionarLogErro("EnviarPergunta", response.ProtocoloRetorno.ToString(), CodigoMensagem.ERRO_APLICAO.CodigoErro, CodigoMensagem.ERRO_APLICAO.SetFormat(string.Format("MENSAGEM={0}\nINNER_EXCEPTION={1}\nSTACK_TRACE={2}\n JSON REQUEST=={3}", ex.Message, (ex.InnerException != null ? ex.InnerException.ToString() : ""), ex.StackTrace.ToString()), jsonRequest).Descricao, TipoMensagem.ErroAplicacao);
                response.AdicionarMensagemErro(CodigoMensagem.ERRO_APLICAO_GENERICO.CodigoErro, CodigoMensagem.ERRO_APLICAO_GENERICO.Descricao, TipoMensagem.ErroAplicacao);
            }

            response.Valido = response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio);

            return response;

        }


        public async Task<PerguntasERespostasRespTerceiroRS> RespostaTerceiro(PerguntasERespostasRespTerceiroRQ request)
        {
            PerguntasERespostasRespTerceiroRS response = new PerguntasERespostasRespTerceiroRS();
            response.Mensagens = new List<Mensagem>();
            response.ProtocoloRetorno = Guid.NewGuid();
            response.Valido = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            if (request.IdQuestion<0)
            {
                response.Valido = false;
                response.AdicionarMensagemErro("-1", "idquestion não pode ser 0", TipoMensagem.ErroValidacao);    
            }
            if (request.Resposta.IsEmptyOrWhiteSpace() || request.ClienteEmail.IsEmptyOrWhiteSpace() || request.ClienteEmail.IsEmptyOrWhiteSpace())
            {
                response.Valido = false;
                response.AdicionarMensagemErro("-1", "resposta, e-mail ou nome não poder nulo", TipoMensagem.ErroValidacao);
            }
            try
            {
                if (response.Valido)
                {
                    if (response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio))
                    {
                        var idRetorno = await qaRepository.AddQARespostaTerceiroAsync(request);
                        response.CodigoRetorno = idRetorno;
                    }
                }

            }
            catch (Exception ex)
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                logService.AdicionarLogErro("RespostaTerceiro", response.ProtocoloRetorno.ToString(), CodigoMensagem.ERRO_APLICAO.CodigoErro, CodigoMensagem.ERRO_APLICAO.SetFormat(string.Format("MENSAGEM={0}\nINNER_EXCEPTION={1}\nSTACK_TRACE={2}\n JSON REQUEST=={3}", ex.Message, (ex.InnerException != null ? ex.InnerException.ToString() : ""), ex.StackTrace.ToString()), jsonRequest).Descricao, TipoMensagem.ErroAplicacao);
                response.AdicionarMensagemErro(CodigoMensagem.ERRO_APLICAO_GENERICO.CodigoErro, CodigoMensagem.ERRO_APLICAO_GENERICO.Descricao, TipoMensagem.ErroAplicacao);
            }

            response.Valido = response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio);

            return response;
        }


        public async Task<PerguntasERespostasDisplayRS> ObterDuvidas(PerguntasERespostasDisplayRQ request)
        {
            PerguntasERespostasDisplayRS response = request.ValidarPerguntasERespostasDisplayRQ(clienteService);

            try
            {
                if (response.Valido)
                {
                    if (response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio))
                    {

                        if (request.PageNumber == 0 || !request.Busca.IsEmptyOrWhiteSpace())
                        {
                            request.PageNumber = 1;
                        }                        
                        response = await qaRepository.ObterDuvidasAsync(request);
                    }
                }
            }
            catch (Exception ex)
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                logService.AdicionarLogErro("EnviarPergunta", response.ProtocoloRetorno.ToString(), CodigoMensagem.ERRO_APLICAO.CodigoErro, CodigoMensagem.ERRO_APLICAO.SetFormat(string.Format("MENSAGEM={0}\nINNER_EXCEPTION={1}\nSTACK_TRACE={2}\n JSON REQUEST=={3}", ex.Message, (ex.InnerException != null ? ex.InnerException.ToString() : ""), ex.StackTrace.ToString()), jsonRequest).Descricao, TipoMensagem.ErroAplicacao);
                response.AdicionarMensagemErro(CodigoMensagem.ERRO_APLICAO_GENERICO.CodigoErro, CodigoMensagem.ERRO_APLICAO_GENERICO.Descricao, TipoMensagem.ErroAplicacao);
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            response.Valido = response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;            
        }

       
    }














    /// <summary>
    /// Validação de request de EnviarPergunta
    /// </summary>
    public static class QAServiceValidation
    {
        public static PerguntasERespostasRS ValidarEnviarPergunta(this PerguntasERespostasRQ request, IClienteRepository clienteService) {

            var response = new PerguntasERespostasRS();

            response.Mensagens = new List<Mensagem>();
            response.ProtocoloRetorno = Guid.NewGuid();

            if (response.Mensagens.Count == 0 && (request != null && request.Produto != null && request.Loja != null))
            {

                if (string.IsNullOrEmpty(request.Authorization))
                {
                    response.AdicionarMensagemErro(CodigoMensagem.TOKEN_VAZIO.CodigoErro, CodigoMensagem.TOKEN_VAZIO.Descricao, TipoMensagem.ErroValidacao);
                }

                if (string.IsNullOrEmpty(request.Loja.LojaCodigo))
                {
                    response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_ID_LOJA.CodigoErro, CodigoMensagem.NAO_INFORMADO_ID_LOJA.Descricao, TipoMensagem.ErroValidacao);
                }
                if (string.IsNullOrEmpty(request.ClienteEmail) || string.IsNullOrEmpty(request.ClienteNome) || string.IsNullOrEmpty(request.ClientePergunta))
                {
                    response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_CAMPOS.CodigoErro, CodigoMensagem.NAO_INFORMADO_CAMPOS.SetFormat("Nome, email ou pergunta").Descricao, TipoMensagem.ErroValidacao);
                }
            }
            else
            {
                response.AdicionarMensagemErro(CodigoMensagem.REQUEST_INVALIDO.CodigoErro, CodigoMensagem.REQUEST_INVALIDO.Descricao, TipoMensagem.ErroValidacao);
            }

            response.Valido = response.Mensagens.Count == 0;
            return response;
        }

        public static PerguntasERespostasDisplayRS ValidarPerguntasERespostasDisplayRQ(this PerguntasERespostasDisplayRQ request, IClienteRepository clienteService)
        {
            PerguntasERespostasDisplayRS response = new PerguntasERespostasDisplayRS();
            response.Mensagens = new List<Mensagem>();
            response.ProtocoloRetorno = Guid.NewGuid();

            if (response.Mensagens.Count == 0 && (request != null))
            {

                if (request.CodCliente.IsEmptyOrWhiteSpace())
                {
                    response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_ID_CLIENTE.CodigoErro, CodigoMensagem.NAO_INFORMADO_ID_CLIENTE.Descricao, TipoMensagem.ErroValidacao);
                }
                else
                {
                    var clienteExist = clienteService.Find(x => x.CodigoCliente == request.CodCliente);
                    if (clienteExist == null)
                    {
                        response.AdicionarMensagemErro(CodigoMensagem.ID_CLIENTE_NAO_EXISTE.CodigoErro, CodigoMensagem.ID_CLIENTE_NAO_EXISTE.Descricao, TipoMensagem.ErroValidacao);
                    }
                }                
                if (string.IsNullOrEmpty(request.CodigoLoja))
                {
                    response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_ID_LOJA.CodigoErro, CodigoMensagem.NAO_INFORMADO_ID_LOJA.Descricao, TipoMensagem.ErroValidacao);
                }                
            }
            else
            {
                response.AdicionarMensagemErro(CodigoMensagem.REQUEST_INVALIDO.CodigoErro, CodigoMensagem.REQUEST_INVALIDO.Descricao, TipoMensagem.ErroValidacao);
            }

            response.Valido = response.Mensagens.Count == 0;
            return response;

        }
    }

}
