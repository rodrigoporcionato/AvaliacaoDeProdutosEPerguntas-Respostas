using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using BetaViews.Messages.Enums;
using BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Produto;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Produto;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Core.Services.Logs;
using BetaViews.Messages;

namespace BetaViews.Core.Services.Avaliacoes.Loja
{
    public class AvaliacoesService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepository avaliacaoService;
        private readonly ILogService logService;
        public AvaliacoesService(IAvaliacaoRepository avaliacaoService, ILogService logService)
        {
            this.avaliacaoService = avaliacaoService;
            this.logService = logService;
        }

        public async Task<EnviarAvaliacaoProdutoRS> AdicionarAvaliacaoProduto(EnviarAvaliacaoProdutoRQ request)
        {
            EnviarAvaliacaoProdutoRS response = request.AdicionarAvaliacaoProdutoValidar();

            try
            {
                if (response.Valido)
                {                    
                    if (response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio))
                    {
                        var addAvaliacao = await avaliacaoService.EnviarAvaliacaoProduto(request);

                        response.CodigoRetorno = addAvaliacao;
                    }
                }
            }
            catch (Exception ex)
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                logService.AdicionarLogErro("AdicionarAvaliacaoProduto", response.ProtocoloRetorno.ToString(), CodigoMensagem.ERRO_APLICAO.CodigoErro, CodigoMensagem.ERRO_APLICAO.SetFormat(string.Format("MENSAGEM={0}\nINNER_EXCEPTION={1}\nSTACK_TRACE={2}\n JSON REQUEST=={3}", ex.Message, (ex.InnerException != null ? ex.InnerException.ToString() : ""), ex.StackTrace.ToString()), jsonRequest).Descricao,TipoMensagem.ErroAplicacao);
                response.AdicionarMensagemErro(CodigoMensagem.ERRO_APLICAO_GENERICO.CodigoErro, CodigoMensagem.ERRO_APLICAO_GENERICO.Descricao, TipoMensagem.ErroAplicacao);
            }
            response.Valido = response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio);

            return response;
        }
        public async Task<EnviarAvaliacaoLojaRS> AdicionarAvaliacaoLoja(EnviarAvaliacaoLojaRQ request)
        {
            EnviarAvaliacaoLojaRS response = request.AdicionarAvaliacaoLojaValidar();

            try
            {
                if (response.Valido)
                {
                    if (response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio))
                    {
                        var addAvaliacao = await avaliacaoService.EnviarAvaliacaoLoja(request);

                        response.CodigoRetorno = addAvaliacao;
                    }
                }
            }
            catch (Exception ex)
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                logService.AdicionarLogErro("AdicionarAvaliacaoLoja", response.ProtocoloRetorno.ToString(), CodigoMensagem.ERRO_APLICAO.CodigoErro, CodigoMensagem.ERRO_APLICAO.SetFormat(string.Format("MENSAGEM={0}\nINNER_EXCEPTION={1}\nSTACK_TRACE={2}\n JSON REQUEST=={3}", ex.Message, (ex.InnerException != null ? ex.InnerException.ToString() : ""), ex.StackTrace.ToString()), jsonRequest).Descricao, TipoMensagem.ErroAplicacao);
                response.AdicionarMensagemErro(CodigoMensagem.ERRO_APLICAO_GENERICO.CodigoErro, CodigoMensagem.ERRO_APLICAO_GENERICO.Descricao, TipoMensagem.ErroAplicacao);
            }
            response.Valido = response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio);

            return response;
        }
        public async Task<ListarAvaliacoesLojasRS> ListarAvaliacoesLojas(ListarAvaliacoesLojasRQ request)
        {
            ListarAvaliacoesLojasRS response = request.ListarAvaliacoesLojasValidar();
            try
            {
                if (response.Valido)
                {
                    if (response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio))
                    {
                        response = await avaliacaoService.AvaliacoesLojasListar(request);

                        if (!response.Avaliacoes.Any())
                        {
                            response.Valido = false;
                            response.AdicionarMensagemErro("sem resultados", "não tem avaliações", TipoMensagem.NenhumErro);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                logService.AdicionarLogErro("ListarAvaliacoesLojas", response.ProtocoloRetorno.ToString(), CodigoMensagem.ERRO_APLICAO.CodigoErro, CodigoMensagem.ERRO_APLICAO.SetFormat(string.Format("MENSAGEM={0}\nINNER_EXCEPTION={1}\nSTACK_TRACE={2}\n JSON REQUEST=={3}", ex.Message, (ex.InnerException != null ? ex.InnerException.ToString() : ""), ex.StackTrace.ToString()), jsonRequest).Descricao, TipoMensagem.ErroAplicacao);
                response.AdicionarMensagemErro(CodigoMensagem.ERRO_APLICAO_GENERICO.CodigoErro, CodigoMensagem.ERRO_APLICAO_GENERICO.Descricao, TipoMensagem.ErroAplicacao);
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            response.Valido = response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }
        public async Task<ListarAvaliacoesProdutosRS> ListarAvaliacoesProdutos(ListarAvaliacoesProdutosRQ request)
        {
            ListarAvaliacoesProdutosRS response = request.ListarAvaliacoesProdutosValidar();
            try
            {
                if (response.Valido)
                {
                    if (response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio))
                    {
                        response = await avaliacaoService.AvaliacoesProdutosListar(request);


                        if (response.Avaliacoes.Any() && response.Avaliacoes.Any())
                        {
                            //int totalAvaliacoes = response.AvaliacaoGeral.TotalAvaliacoes;

                            //double mp = 0;
                            //mp = mp + (response.AvaliacaoGeral.Total1Estrela * 1);
                            //mp = mp + (response.AvaliacaoGeral.Total2Estrela * 2);
                            //mp = mp + (response.AvaliacaoGeral.Total3Estrela * 3);
                            //mp = mp + (response.AvaliacaoGeral.Total4Estrela * 4);
                            //mp = mp + (response.AvaliacaoGeral.Total5Estrela * 5);
                            
                            ////MP = TOTAL ESTRELA * PESO(ESTRELA)//MEDIA = MP/total distribuido /                             
                            //if (totalAvaliacoes > 0 && mp > 0)
                            //{
                            //    response.AvaliacaoGeral.TotalAvaliacoes = totalAvaliacoes;
                            //    double PontuacaoMedia = (mp / totalAvaliacoes);
                            //    response.AvaliacaoGeral.AvaliacaoGeral = Math.Round(PontuacaoMedia, 1);
                            //}                            
                            //if (response.AvaliacaoGeral.TotalRecomendaProduto > 0 && totalAvaliacoes > 0)
                            //{
                            //   response.AvaliacaoGeral.TotalRecomendaProduto = Math.Round(100 * response.AvaliacaoGeral.TotalRecomendaProduto / totalAvaliacoes);
                            //}
                        }
                        else
                        {
                            response.Valido = false;
                            response.AdicionarMensagemErro("sem resultados", "não tem avaliações", TipoMensagem.NenhumErro);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                logService.AdicionarLogErro("ListarAvaliacoesPagina", response.ProtocoloRetorno.ToString(), CodigoMensagem.ERRO_APLICAO.CodigoErro, CodigoMensagem.ERRO_APLICAO.SetFormat(string.Format("MENSAGEM={0}\nINNER_EXCEPTION={1}\nSTACK_TRACE={2}\n JSON REQUEST=={3}", ex.Message, (ex.InnerException != null ? ex.InnerException.ToString() : ""), ex.StackTrace.ToString()), jsonRequest).Descricao, TipoMensagem.ErroAplicacao);
                response.AdicionarMensagemErro(CodigoMensagem.ERRO_APLICAO_GENERICO.CodigoErro, CodigoMensagem.ERRO_APLICAO_GENERICO.Descricao, TipoMensagem.ErroAplicacao);
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            response.Valido = response.Mensagens.All(m => m.Tipo == TipoMensagem.Negocio);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }      

        public async Task AtualizaAvaliacaoQtdes(int idReview, TipoReportIntegracaoEnum tipoReport)
        {
            await avaliacaoService.AtualizaAvaliacaoQtdes(idReview,tipoReport);
        }
       
       
    }   
}
