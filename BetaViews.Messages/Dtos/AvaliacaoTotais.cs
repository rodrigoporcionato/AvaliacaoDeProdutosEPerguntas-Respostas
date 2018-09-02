using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages.Dtos
{
    public class AvaliacaoTotaisDTO
    {
        public int TotalAvaliacoes { get; set; }
        public double AvaliacaoGeral { get; set; }
        public double TotalRecomendaProduto { get; set; }
        public double PercentualRecomenda { get; set; }
        public int Total1Estrela { get; set; }
        public int Total2Estrela { get; set; }
        public int Total3Estrela { get; set; }
        public int Total4Estrela { get; set; }
        public int Total5Estrela { get; set; }

    }
}
