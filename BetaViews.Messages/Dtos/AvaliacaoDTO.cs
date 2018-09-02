
using BetaViews.Messages.Enums;
using System;
using System.Runtime.Serialization;

namespace BetaViews.Messages.Dtos
{
    public class AvaliacaoEnvioDTO
   {
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteLocalizacao { get; set; }
        public string ClienteTitulo { get; set; }
        public string ClienteComentario { get; set; }
        public int ClienteClassificacao { get; set; }
        public bool ClienteRecomenda { get; set; }
    }


    public class AvaliacaoDTO
    {        
        public int IdAvaliacao { get; set; }

        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteLocalizacao { get; set; }
        public string ClienteTitulo { get; set; }
        public string ClienteComentario { get; set; }
        public int ClienteClassificacao { get; set; }
        public bool ClienteRecomenda { get; set; }     
        public DateTime DataAvaliacao { get; set; }
        public bool ClienteVerificado { get; set; }
        public int QtdAjudou { get; set; }
        public int QtdNaoAjudou { get; set; }
        public StatusAvaliacaoEnum IdAvaluacaoStatus { get; set; }

    }
}
