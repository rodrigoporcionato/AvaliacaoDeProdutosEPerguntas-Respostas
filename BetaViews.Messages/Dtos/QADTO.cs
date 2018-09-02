using BetaViews.Messages.Enums;
using System;

namespace BetaViews.Messages.Dtos.QA
{
    public class QADTO
    {
        /// <summary>
        /// Id da Pergunta
        /// </summary>
        public int IDQuestion { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string ClientePergunta { get; set; }
        public string ClienteLocalizacao { get; set; }
        public DateTime DtPergunta { get; set; }

        public DateTime DTResposta { get; set; }
        
        public string Resposta { get; set; }
        public string RespostaNome { get; set; }
        public bool RespondidoPorOutroCliente { get; set; }
        public Nullable<int> QtdRespAjudou { get; set; }
        public Nullable<int> QtdRespNaoAjudou { get; set; }
        public string Badge { get; set; }
        public StatusQAEnum IdQAStatus { get; set; }

    }
}
