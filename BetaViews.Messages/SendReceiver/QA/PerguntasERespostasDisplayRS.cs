

using BetaViews.Messages.Dtos;
using BetaViews.Messages.Dtos.QA;
using System.Collections.Generic;

namespace BetaViews.Messages.SendReceiver.QA
{

    
    public class PerguntasERespostasDisplayRS : BaseMessageResponse
    {
        

        public LojaDTO Loja { get; set; }

        public ProdutoDTO Produto { get; set; }

        /// <summary>
        /// Perguntas e Respostas
        /// </summary>
        public List<QADTO> QA { get; set; }

        public PaginationDTO Pagination { get; set; }

    }
        
}
