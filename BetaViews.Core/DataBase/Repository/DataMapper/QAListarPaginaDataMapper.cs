using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Core.DataBase.Repository.DataMapper
{
    public class QAListarPaginaDataMapper
    {
        
        public int IdQA { get; set; }
        
        public int IdQAStatus { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteLocalizacao { get; set; }
        public string ClientePergunta { get; set; }
        public DateTime DtPergunta { get; set; }
        public string Resposta { get; set; }
        public DateTime? DtResposta { get; set; }
        public int QtdAjudou { get; set; }
        public int QtdNaoAjudou { get; set; }
        public string NomeModerador { get; set; }
        public string Badge { get; set; }

        public string CodigoLoja { get; set; }
        public string LojaNome { get; set; }
        public bool LojaMktPlace { get; set; }
        public string ProdCodigo { get; set; }
        public string PrdNome { get; set; }
        public string PrdLink { get; set; }

        public string RespTerceiroClienteNome { get; set; }
        public string RespTerceiroClienteEmail { get; set; }
        public string RespTerceiroClienteLocalizacao { get; set; }
        public int RespTerceiroStatus { get; set; }

        public int TotalRows { get; set; }
    }
}
