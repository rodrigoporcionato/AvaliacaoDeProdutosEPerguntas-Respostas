using System;
using BetaViews.Core.Framework.Extension;
using BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Produto;
using BetaViews.Messages;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Produto;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Loja;

namespace BetaViews.Core.Services.Avaliacoes.Loja
{
    public static class AvaliacoesServiceValidation
    {

        public static ListarAvaliacoesProdutosRS ListarAvaliacoesProdutosValidar(this ListarAvaliacoesProdutosRQ request)
        {

            var response = new ListarAvaliacoesProdutosRS();

            response.Mensagens = new System.Collections.Generic.List<Mensagem>();
            response.ProtocoloRetorno = Guid.NewGuid();

            if (response.Mensagens.Count == 0 && (request != null))
            {

                if (string.IsNullOrEmpty(request.CodigoLoja))
                {
                    response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_ID_LOJA.CodigoErro, CodigoMensagem.NAO_INFORMADO_ID_LOJA.Descricao, TipoMensagem.ErroValidacao);
                }

                if (string.IsNullOrEmpty(request.PrdCodigo))
                {
                    response.AdicionarMensagemErro("PrdCodigo", "PrdCodigo não informado", TipoMensagem.ErroValidacao);
                }

                if (request.ActualPageNumber <= 0)
                {
                    response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_CAMPOS.CodigoErro, CodigoMensagem.NAO_INFORMADO_CAMPOS.SetFormat("PageNumber").Descricao, TipoMensagem.ErroValidacao);
                }
            }
            else
            {
                response.AdicionarMensagemErro(CodigoMensagem.REQUEST_INVALIDO.CodigoErro, CodigoMensagem.REQUEST_INVALIDO.Descricao, TipoMensagem.ErroValidacao);
            }

            response.Valido = response.Mensagens.Count == 0;
            return response;
        }

        public static ListarAvaliacoesLojasRS ListarAvaliacoesLojasValidar(this ListarAvaliacoesLojasRQ request)
        {
            var response = new ListarAvaliacoesLojasRS();

            response.Mensagens = new System.Collections.Generic.List<Mensagem>();
            response.ProtocoloRetorno = Guid.NewGuid();

            if (response.Mensagens.Count == 0 && (request != null))
            {

                if (string.IsNullOrEmpty(request.CodigoLoja))
                {
                    response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_ID_LOJA.CodigoErro, CodigoMensagem.NAO_INFORMADO_ID_LOJA.Descricao, TipoMensagem.ErroValidacao);
                }

                if (request.ActualPageNumber <= 0)
                {
                    response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_CAMPOS.CodigoErro, CodigoMensagem.NAO_INFORMADO_CAMPOS.SetFormat("PageNumber").Descricao, TipoMensagem.ErroValidacao);
                }
            }
            else
            {
                response.AdicionarMensagemErro(CodigoMensagem.REQUEST_INVALIDO.CodigoErro, CodigoMensagem.REQUEST_INVALIDO.Descricao, TipoMensagem.ErroValidacao);
            }

            response.Valido = response.Mensagens.Count == 0;
            return response;
        }

        public static EnviarAvaliacaoProdutoRS AdicionarAvaliacaoProdutoValidar(this EnviarAvaliacaoProdutoRQ request)
        {             
            var response = new EnviarAvaliacaoProdutoRS();
                        
            response.Mensagens = new System.Collections.Generic.List<Mensagem>();
            response.ProtocoloRetorno = Guid.NewGuid();

            if (request == null && request.Avaliacao == null && request.Loja == null && request.Produto == null)
            {
                response.AdicionarMensagemErro(CodigoMensagem.REQUEST_INVALIDO.CodigoErro, CodigoMensagem.REQUEST_INVALIDO.Descricao, TipoMensagem.ErroValidacao);
                response.Valido = false;
                return response;
            }

            if (string.IsNullOrEmpty(request.Loja.LojaCodigo))
            {
                response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_ID_LOJA.CodigoErro, CodigoMensagem.NAO_INFORMADO_ID_LOJA.Descricao, TipoMensagem.ErroValidacao);
            }

            if (!string.IsNullOrEmpty(request.Loja.LojaCodigo) && request.Loja.LojaCodigo.ToLower().Equals("string"))
            {
                response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_ID_LOJA.CodigoErro, CodigoMensagem.NAO_INFORMADO_ID_LOJA.Descricao, TipoMensagem.ErroValidacao);
            }

            if (string.IsNullOrEmpty(request.Loja.LojaNome))
            {
                response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_NOME_LOJA.CodigoErro, CodigoMensagem.NAO_INFORMADO_NOME_LOJA.Descricao, TipoMensagem.ErroValidacao);
            }

            if (!string.IsNullOrEmpty(request.Loja.LojaNome) && request.Loja.LojaNome.ToLower().Equals("string"))
            {
                response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_NOME_LOJA.CodigoErro, CodigoMensagem.NAO_INFORMADO_NOME_LOJA.Descricao, TipoMensagem.ErroValidacao);
            }

            if (request.Produto.PrdCodigo.IsEmptyOrWhiteSpace())
            {
                response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_CAMPOS.CodigoErro, CodigoMensagem.NAO_INFORMADO_CAMPOS.SetFormat("PrdCodigo").Descricao, TipoMensagem.ErroValidacao);
            }

            if (request.Avaliacao.ClienteClassificacao <= 0 || request.Avaliacao.ClienteClassificacao > 5)
            {
                response.AdicionarMensagemErro(CodigoMensagem.AVALIACAO_GERAL_INVALIDO.CodigoErro, CodigoMensagem.AVALIACAO_GERAL_INVALIDO.Descricao, TipoMensagem.ErroValidacao);
            }

            if (string.IsNullOrWhiteSpace(request.Avaliacao.ClienteEmail.IfEmptyOrWhiteSpace(""))
                 || string.IsNullOrWhiteSpace(request.Avaliacao.ClienteNome.IfEmptyOrWhiteSpace(""))
                 || string.IsNullOrWhiteSpace(request.Avaliacao.ClienteComentario.IfEmptyOrWhiteSpace("")))
            {

                response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_CAMPOS.CodigoErro, CodigoMensagem.NAO_INFORMADO_CAMPOS.SetFormat("Nome, email ou comentários").Descricao, TipoMensagem.ErroValidacao);
            }



            response.Valido = response.Mensagens.Count == 0;
            return response;
        }



        public static EnviarAvaliacaoLojaRS AdicionarAvaliacaoLojaValidar(this EnviarAvaliacaoLojaRQ request)
        {
            var response = new EnviarAvaliacaoLojaRS();

            response.Mensagens = new System.Collections.Generic.List<Mensagem>();
            response.ProtocoloRetorno = Guid.NewGuid();

            if (request == null && request.Avaliacao == null && request.Loja == null)
            {
                response.AdicionarMensagemErro(CodigoMensagem.REQUEST_INVALIDO.CodigoErro, CodigoMensagem.REQUEST_INVALIDO.Descricao, TipoMensagem.ErroValidacao);
                response.Valido = false;
                return response;
            }

            if (string.IsNullOrEmpty(request.Loja.LojaCodigo))
            {
                response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_ID_LOJA.CodigoErro, CodigoMensagem.NAO_INFORMADO_ID_LOJA.Descricao, TipoMensagem.ErroValidacao);
            }

            if (!string.IsNullOrEmpty(request.Loja.LojaCodigo) && request.Loja.LojaCodigo.ToLower().Equals("string"))
            {
                response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_ID_LOJA.CodigoErro, CodigoMensagem.NAO_INFORMADO_ID_LOJA.Descricao, TipoMensagem.ErroValidacao);
            }

            if (string.IsNullOrEmpty(request.Loja.LojaNome))
            {
                response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_NOME_LOJA.CodigoErro, CodigoMensagem.NAO_INFORMADO_NOME_LOJA.Descricao, TipoMensagem.ErroValidacao);
            }

            if (!string.IsNullOrEmpty(request.Loja.LojaNome) && request.Loja.LojaNome.ToLower().Equals("string"))
            {
                response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_NOME_LOJA.CodigoErro, CodigoMensagem.NAO_INFORMADO_NOME_LOJA.Descricao, TipoMensagem.ErroValidacao);
            }

            if (request.Avaliacao.ClienteClassificacao <= 0 || request.Avaliacao.ClienteClassificacao > 5)
            {
                response.AdicionarMensagemErro(CodigoMensagem.AVALIACAO_GERAL_INVALIDO.CodigoErro, CodigoMensagem.AVALIACAO_GERAL_INVALIDO.Descricao, TipoMensagem.ErroValidacao);
            }

            if (string.IsNullOrWhiteSpace(request.Avaliacao.ClienteEmail.IfEmptyOrWhiteSpace(""))
                 || string.IsNullOrWhiteSpace(request.Avaliacao.ClienteNome.IfEmptyOrWhiteSpace(""))
                 || string.IsNullOrWhiteSpace(request.Avaliacao.ClienteComentario.IfEmptyOrWhiteSpace("")))
            {

                response.AdicionarMensagemErro(CodigoMensagem.NAO_INFORMADO_CAMPOS.CodigoErro, CodigoMensagem.NAO_INFORMADO_CAMPOS.SetFormat("Nome, email ou comentários").Descricao, TipoMensagem.ErroValidacao);
            }



            response.Valido = response.Mensagens.Count == 0;
            return response;
        }


    }
}
