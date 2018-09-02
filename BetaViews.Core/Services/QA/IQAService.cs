using BetaViews.Messages.SendReceiver.QA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Core.Services.QA
{
    public interface IQAService
    {
        Task<PerguntasERespostasRS> EnviarPergunta(PerguntasERespostasRQ request);


        Task<PerguntasERespostasDisplayRS> ObterDuvidas(PerguntasERespostasDisplayRQ request);

        Task<PerguntasERespostasRespTerceiroRS> RespostaTerceiro(PerguntasERespostasRespTerceiroRQ request);
    }
}
