using BetaViews.Messages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages.SendReceiver.Avaliacoes.Moderacao.Loja
{
    public class ModeracaoLojaRS : BaseMessageResponse
    {
        public PaginationDTO Pagination { get; set; }
        public List<ModeracaoLojaAvaliacaoDTO> Avaliacoes { get; set; }
    }   
}
