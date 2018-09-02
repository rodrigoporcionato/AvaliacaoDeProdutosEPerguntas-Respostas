using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages
{
    public enum TipoMensagem
    {
        [EnumMember(Value = "NenhumErro")]
        NenhumErro = 0,
        [EnumMember(Value = "ErroAplicacao")]
        ErroAplicacao = 1,
        [EnumMember(Value = "ErroNegocio")]
        ErroNegocio = 2,
        [EnumMember(Value = "ErroValidacao")]
        ErroValidacao = 3,
        [EnumMember(Value = "Aplicacao")]
        Aplicacao = 4,
        [EnumMember(Value = "Negocio")]
        Negocio = 5,
        [EnumMember(Value = "Validacao")]
        Validacao = 6
    }    
    public abstract class BaseMessageResponse
    {

        
        public HttpStatusCode StatusCode { get; set; }

        
        public Guid ProtocoloRetorno { get; set; }

        
        public bool Valido { get; set; }

        
        public int? CodigoRetorno { get; set; }
        
        
        public List<Mensagem> Mensagens { get; set; }

        public void AdicionarMensagemErro(List<Mensagem> mensagens) {
            if (Mensagens == null)
            {
                Mensagens = new List<Mensagem>();
            }          
            Mensagens.AddRange(mensagens);
        }
        public void AdicionarMensagemErro(Mensagem mensagem) {
            if (Mensagens == null)
            {
                Mensagens = new List<Mensagem>();
            }
            Mensagens.Add(mensagem);
        }
        public void AdicionarMensagemErro(string codigo, string conteudo, TipoMensagem tipoMensagem) {

            if (Mensagens == null)
            {
                Mensagens = new List<Mensagem>();
            }

            Mensagens.Add(new Mensagem { CodigoErro = codigo, Descricao = conteudo, Tipo = tipoMensagem });
        }

    }    
    public class Mensagem
    {
        public Mensagem(string idMensagem, string conteudo, string[] param)
        {
            CodigoErro = idMensagem;
            Descricao = string.Format(conteudo, param);
        }

        public Mensagem(string idMensagem, string conteudo)
        {
            CodigoErro = idMensagem;
            Descricao = conteudo;
        }

        public Mensagem(string idMensagem)
        {
            CodigoErro = idMensagem;
        }

        public Mensagem()
        {
        }

        public Mensagem SetFormat(params string[] format)
        {
            return new Mensagem(CodigoErro, Descricao, format);
        }



        
        public string CodigoErro { get; set; }

        
        public string Descricao { get; set; }

        
        public TipoMensagem Tipo { get; set; }

    }

    public abstract class CodigoMensagem
    {
        public static readonly Mensagem ERRO_APLICAO = new Mensagem("000", "{0}");
        public static readonly Mensagem RETORNO_NULO = new Mensagem("000-1", "não existe resultados.");
        public static readonly Mensagem ERRO_APLICAO_GENERICO = new Mensagem("000-1", "Ocorreu um erro na requisição. Por favor contacte o fornecedor.");
        public static readonly Mensagem REQUEST_INVALIDO = new Mensagem("000-2", "O envio de informações da API não é valido.");
        public static readonly Mensagem NAO_INFORMADO_ID = new Mensagem("000-3", "Não foi informado o ID {0}.");
        public static readonly Mensagem TOKEN_VAZIO = new Mensagem("000-4", "Token de sergurança não informado.");
        public static readonly Mensagem NAO_INFORMADO_ID_LOJA = new Mensagem("000-6", "Não foi informado o CodigoLoja.");
        public static readonly Mensagem TOKEN_INVALIDO = new Mensagem("000-5", "Token de segurança não é valido ou está expirado.");
        public static readonly Mensagem NAO_INFORMADO_NOME_LOJA = new Mensagem("000-6-1", "Não foi informado o nome da Loja.");
        public static readonly Mensagem COD_LOJA_OU_COD_CLI_INVALIADO= new Mensagem("000-6-2", "CodigoLoja ou IdCliente não existe ou não estão associados corretamente. Verifique se ambos são da loja e cliente. código informado: {0}.");
        public static readonly Mensagem NAO_INFORMADO_ID_LOJA_INEXISTENTE = new Mensagem("000-6-1", "CodigoLoja {0} não existe no sistema. Solicite cadastro no fornecedor.");
        public static readonly Mensagem NAO_INFORMADO_CAMPOS = new Mensagem("000-7", "Um ou mais campos não foram informados e são necessários. {0}.");
        public static readonly Mensagem NAO_INFORMADO_ID_CLIENTE = new Mensagem("000-8", "Não foi informado o ID CLIENTE{0}.");
        public static readonly Mensagem ID_CLIENTE_NAO_EXISTE = new Mensagem("000-8", "CLIENTE ID CLIENTE {0} NÃO EXISTE NO SISTEMA.");
        public static readonly Mensagem NAO_INFORMADO_NOME_CLIENTE = new Mensagem("000-8-1", "Não foi informado o NOME DO CLIENTE.");
        public static readonly Mensagem NAO_INFORMADO_ID_CLIENTE_INVALIDO = new Mensagem("000-8", "ID CLIENTE não está cadastrado. Solicite ao fornecedor idCliente {0}.");
        public static readonly Mensagem AVALIACAO_GERAL_INVALIDO = new Mensagem("000-9", "Campo AvaliacaoGeral deve ser entre 1 e 5.");
        public static readonly Mensagem PERGUNTA_DINAMICA_VAZIO = new Mensagem("000-10", "Não existe perguntas para essa loja.");

    }
}
