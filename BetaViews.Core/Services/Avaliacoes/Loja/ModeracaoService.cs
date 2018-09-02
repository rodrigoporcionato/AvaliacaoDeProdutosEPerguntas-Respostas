using BetaViews.Core.DataBase.Repository.Interface;
using System.Linq;
using System.Threading.Tasks;
using BetaViews.Core.Framework.Extension;
using System.Collections.Generic;
using System;
using BetaViews.Core.Services.Logs;
using BetaViews.Messages.Enums;
using BetaViews.Messages.SendReceiver.ModeracaoAuto;

namespace BetaViews.Core.Services.Avaliacoes.Loja
{
    public class ModeracaoService : IModeracaoService
    {

        private readonly IAvaliacaoRepository avaliacaoService;
        private readonly IQARepository qaRepository;
        private readonly IClienteRepository cliService;
        private readonly ILogService logService;
        private readonly IPalavraRecusadaPadraoRepository palavrasRecusadaService;
        
        public ModeracaoService(ILogService logService, IPalavraRecusadaPadraoRepository palavrasRecusadaService, IAvaliacaoRepository avaliacaoService, IClienteRepository cliService, IQARepository qaRepository)
        {
            this.logService = logService;
            this.palavrasRecusadaService = palavrasRecusadaService;
            this.avaliacaoService = avaliacaoService;
            this.cliService = cliService;
            this.qaRepository = qaRepository;
        }

        

        public async Task<string> ExecutaModeracaoAvaliacoes()
        {
            int totalGeral = 0;
            int totalComErro = 0;
            int totalSucesso = 0;
            var request = new ModeracaoAutoRQ();
            var response = new ModeracaoAutoRS();
            response.Avaliacoes = new List<Messages.Dtos.AvaliacaoDTO>();

            var clientes = await cliService.FindAllAsync(x => x.FlagAtivo);

            if (clientes!=null)
            {
                foreach (var c in clientes)
                {
                    request.Token = c.APIKey;
                    response = await avaliacaoService.ListarAvaliacoesPendentes(request);


                    if (response != null && response.Avaliacoes != null && response.Avaliacoes.Any())
                    {
                        var palavraRecusada = await ObterPalavrasRecusadas(c);

                        foreach (var a in response.Avaliacoes)
                        {                            

                            string clienteTitulo = a.ClienteTitulo.RemoveDiacriticsLower();
                            string clienteNome = a.ClienteNome.RemoveDiacriticsLower();
                            string clienteComentario = a.ClienteComentario.RemoveDiacriticsLower();
                            string clienteEmail = a.ClienteEmail.RemoveDiacriticsLower();

                            var palavraTituloOuComentario= new List<string>();
                            palavraTituloOuComentario.AddRange(ContainBadWords(palavraRecusada, clienteTitulo));
                            palavraTituloOuComentario.AddRange(ContainBadWords(palavraRecusada, clienteComentario));

                            var palavraEmailOuNome = new List<string>();
                            palavraEmailOuNome.AddRange(ContainBadWords(palavraRecusada, clienteEmail));
                            palavraEmailOuNome.AddRange(ContainBadWords(palavraRecusada, clienteNome));

                            if (palavraTituloOuComentario.Any())
                            {
                                a.IdAvaluacaoStatus = StatusAvaliacaoEnum.RejeitadoRoboAprovacao;
                                await avaliacaoService.AlterarStatusAvaliacao(a.IdAvaliacao, StatusAvaliacaoEnum.RejeitadoRoboAprovacao, 
                                    $"Robô Aprovação - CANCELADO AUTOMATICAMENTE. COMENTÁRIO PARECE TER ALGUMA PALAVRA NEGATIVA/PROFANA OU INVALIDA. PALAVRA(S) ECONTRADA(S): {string.Join(",", palavraTituloOuComentario.Select(x => x))}" );
                                totalComErro++;
                            }
                            else if (a.ClienteComentario.Length <= 4)
                            {                                
                                a.IdAvaluacaoStatus = StatusAvaliacaoEnum.RejeitadoRoboAprovacao;
                                await avaliacaoService.AlterarStatusAvaliacao(a.IdAvaliacao, StatusAvaliacaoEnum.RejeitadoRoboAprovacao,
                                    $"Robô Aprovação - CANCELADO AUTOMATICAMENTE. POUCOS CARACTERES NO COMENTÁRIO. CONTEÚDO MENOR QUE 4 É CANCELADO AUTOMATICAMENTE.");
                                totalComErro++;
                            }
                            else
                            {
                                a.IdAvaluacaoStatus = StatusAvaliacaoEnum.PendenteRevisao;
                                await avaliacaoService.AlterarStatusAvaliacao(a.IdAvaliacao, StatusAvaliacaoEnum.PendenteRevisao, "Revisado automaticamente, aguardando revisão manual de usuário.");
                                totalSucesso++;
                            }
                            totalGeral++;
                        }
                    }
                }
            }

            string result = totalGeral > 0 ? string.Format("Moderação automatica executada em {3}. Total Geral:{0}, total com erro:{1}, total aguardando revisão manual:{2} ", totalGeral, totalComErro, totalSucesso, DateTime.Now.ToString()) : "Não há registros para processar.";
            return result;

        }



        public async Task<string> ExecutaModeracaoQA()
        {
            int totalGeral = 0;
            int totalComErro = 0;
            int totalSucesso = 0;
            var request = new ModeracaoAutoRQ();
            var response = new ModeracaoAutoRS();
            response.Perguntas = new List<Messages.Dtos.QAProdutoDTO>();

            var clientes = await cliService.FindAllAsync(x => x.FlagAtivo);

            if (clientes != null)
            {
                foreach (var c in clientes)
                {
                    request.Token = c.APIKey;
                    response = await qaRepository.ListarPerguntasPendentes(request);


                    if (response != null && response.Perguntas != null && response.Perguntas.Any())
                    {
                        var palavraRecusada = await ObterPalavrasRecusadas(c);

                        foreach (var p in response.Perguntas)
                        {                            
                            var perguntaDuplicada = await qaRepository.FindAllAsync(x => x.IdQAStatus == 5 && x.Produto!=null && x.Produto.Codigo.Equals(p.Produto.PrdCodigo) && x.Pergunta.Equals(p.ClientePergunta));
                            string clienteNome = p.ClienteNome.RemoveDiacriticsLower();
                            string clienteComentario = p.ClientePergunta.RemoveDiacriticsLower();
                            string clienteEmail = p.ClienteEmail.RemoveDiacriticsLower();

                            var palavraTituloOuComentario = new List<string>();
                            palavraTituloOuComentario.AddRange(ContainBadWords(palavraRecusada, clienteComentario));

                            var palavraEmailOuNome = new List<string>();
                            palavraEmailOuNome.AddRange(ContainBadWords(palavraRecusada, clienteEmail));
                            palavraEmailOuNome.AddRange(ContainBadWords(palavraRecusada, clienteNome));
                            

                            if (palavraTituloOuComentario.Any())
                            {
                                p.IdQAStatus = StatusQAEnum.RejeitadoRoboAprovacao;
                                await qaRepository.AlterarStatusQA(p.IDQuestion, StatusQAEnum.RejeitadoRoboAprovacao,
                                    $"Robô Aprovação - CANCELADO AUTOMATICAMENTE. COMENTÁRIO PARECE TER ALGUMA PALAVRA NEGATIVA/PROFANA OU INVALIDA. PALAVRA(S) ECONTRADA(S): {string.Join(",", palavraTituloOuComentario.Select(x => x))}");
                                totalComErro++;
                            }
                            else if (p.ClientePergunta.Length <= 2)
                            {
                                p.IdQAStatus = StatusQAEnum.RejeitadoRoboAprovacao;
                                await qaRepository.AlterarStatusQA(p.IDQuestion, StatusQAEnum.RejeitadoRoboAprovacao,
                                    $"Robô Aprovação - CANCELADO AUTOMATICAMENTE. POUCOS CARACTERES NO COMENTÁRIO. CONTEÚDO MENOR QUE 4 É CANCELADO AUTOMATICAMENTE.");
                                totalComErro++;
                            }
                            else if (perguntaDuplicada!=null && perguntaDuplicada.Any()) {
                                p.IdQAStatus = StatusQAEnum.RejeitadoRoboAprovacao;
                                await qaRepository.AlterarStatusQA(p.IDQuestion, StatusQAEnum.RejeitadoRoboAprovacao,
                                    $"Robô Aprovação - CANCELADO AUTOMATICAMENTE. PERGUNTA DUPLICADA PARA UM PRODUTO. A pergunta recusada foi {p.ClientePergunta}");
                                totalComErro++;
                            }
                            else
                            {
                                p.IdQAStatus = StatusQAEnum.PendenteRevisao;
                                await qaRepository.AlterarStatusQA(p.IDQuestion, StatusQAEnum.PendenteRevisao, "Revisado automaticamente, aguardando revisão manual de usuário.");
                                totalSucesso++;
                            }
                            totalGeral++;
                        }
                    }
                }
            }

            string result = totalGeral > 0 ? string.Format("Moderação automatica executada em {3}. Total Geral:{0}, total com erro:{1}, total aguardando revisão manual:{2} ", totalGeral, totalComErro, totalSucesso, DateTime.Now.ToString()) : "Não há registros para processar.";
            return result;
        }



        private async Task<List<string>> ObterPalavrasRecusadas(DataBase.Entitys.Clientes c)
        {
            var palavras = await palavrasRecusadaService.GetAllAsync();

            //faz um merge de palavras caso o cliente tenha adicionado algo.
            var palavrasClientes = await cliService.PalavraRecusadaCliente(c.Id);

            if (palavrasClientes != null && palavrasClientes.Any())
            {
                palavrasClientes.ForEach(a => {
                    palavras.Add(new DataBase.Entitys.PalavraRecusadaPadrao { Nome = a.Nome });
                });
            }

            palavras.ToList().ForEach(x =>
            {
                x.Nome = x.Nome.RemoveDiacriticsLower();
            });

            var result = palavras.Distinct().Select(x => x.Nome).ToList();

            return result;
        }

        

        private List<string> ContainBadWords(List<string> badwords, string content)
        {
            var words = content.GetWords();

            var palavras = new List<string>();

            foreach (var b in badwords)
            {

                int total = words.Count(w => w.Equals(b.RemoveDiacritics(), StringComparison.OrdinalIgnoreCase));
                if (total > 0)
                {
                    palavras.Add(b);
                }
            }

            return palavras;
        }

       
    }
}
