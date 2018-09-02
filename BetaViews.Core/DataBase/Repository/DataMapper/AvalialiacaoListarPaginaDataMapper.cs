using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Core.DataBase.Repository.DataMapper
{
    public class AvalialiacaoListarPaginaDataMapper
    {
        public int totalRows { get; set; }
        public double AvaliacaoGeral { get; set; }
        public int TotalRecomenda { get; set; }
        public double PercentualRecomenda { get; set; }
        public int IdAvaliacao { get; set; }        
        public int CodigoCliente { get; set; }
        public string LojaCodigo { get; set; }
        public string LojaNome { get; set; }
        public string PrdCodigo { get; set; }
        public string PrdLink { get; set; }

        public string PrdNome { get; set; }
        public bool LojaMarketPlace { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteLocalizacao { get; set; }
        public bool ClienteVerificado { get; set; }
        public int ClienteClassificacao { get; set; }
        public string ClienteTitulo { get; set; }
        public string ClienteComentario { get; set; }
        public DateTime ClienteDataAvaliacao { get; set; }
        public bool ClienteRecomenda { get; set; }
        public int QtdAjudou { get; set; }
        public int QtdNaoAjudou { get; set; }
        public int S1 { get; set; }
        public int S2 { get; set; }
        public int S3 { get; set; }
        public int S4 { get; set; }
        public int S5 { get; set; }
        public int AP_IDREVIEW { get; set; }
        public int AP_CLASS { get; set; }
        public string AP_NOME { get; set; }
        public string AP_COMENT { get; set; }
        public int ANP_IDREVIEW { get; set; }
        public int ANP_CLASS { get; set; }
        public string ANP_NOME { get; set; }
        public string ANP_COMENT { get; set; }


    }

   
        
}
