using BetaViews.Messages.Dtos;
using BetaViews.Messages.Dtos.QA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages.SendReceiver.ModeracaoAuto
{
    public class ModeracaoAutoRS
    {
        public List<AvaliacaoDTO> Avaliacoes { get; set; }

        public List<QAProdutoDTO> Perguntas { get; set; }

    }
}
