using BetaViews.Core.DataBase.Entitys;
using BetaViews.Core.Framework;
using BetaViews.Messages.Enums;
using BetaViews.Messages.SendReceiver.ModeracaoAuto;
using BetaViews.Messages.SendReceiver.QA;
using BetaViews.Messages.SendReceiver.QA.ListarPergunta;
using BetaViews.Messages.SendReceiver.QA.Moderacao;
using System.Threading.Tasks;

namespace BetaViews.Core.DataBase.Repository.Interface
{
    public interface IQARepository : IGenericRepository<QA>
    {
        Task AlterarStatusQA(int IdQA, StatusQAEnum IdQAStatus, string motivo);
        Task<ModeracaoAutoRS> ListarPerguntasPendentes(ModeracaoAutoRQ request);
        Task<int> AddQAAsync(PerguntasERespostasRQ request);
        Task<int> AddQARespostaTerceiroAsync(PerguntasERespostasRespTerceiroRQ request);
        Task<PerguntasERespostasDisplayRS> ObterDuvidasAsync(PerguntasERespostasDisplayRQ request);
        Task ModeracaoResponderCliente(QAModeracaoResponderClienteRQ request);
        /// <summary>
        /// Usado no adm, recupera perguntas para responder.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ListarPerguntaRS> ListarPerguntaAdmin(ListarPerguntaRQ request);        

    }
}
