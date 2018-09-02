using BetaViews.Messages.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages.Dtos
{
    public class QAProdutoDTO
    {
        /// <summary>
        /// Id da Pergunta
        /// </summary>
        public int IDQuestion { get; set; }
        public StatusQAEnum IdQAStatus { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string ClientePergunta { get; set; }
        public string ClienteLocalizacao { get; set; }
        public DateTime DtPergunta { get; set; }

        public DateTime DTResposta { get; set; }
        public string Resposta { get; set; }
        public string RespostaNome { get; set; }

        public bool RespondidoPorOutroCliente { get; set; }
        public string RespTerceiroClienteNome { get; set; }
        public string RespTerceiroClienteEmail { get; set; }
        public string RespTerceiroClienteLocalizacao { get; set; }


        public Nullable<int> QtdRespAjudou { get; set; }
        public Nullable<int> QtdRespNaoAjudou { get; set; }
        public string Badge { get; set; }

        public LojaDTO Loja { get; set; }
        public ProdutoDTO Produto { get; set; }
    }
}
