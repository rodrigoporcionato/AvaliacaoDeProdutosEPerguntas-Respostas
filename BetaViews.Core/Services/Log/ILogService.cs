using BetaViews.Messages;

namespace BetaViews.Core.Services.Logs
{
    public interface ILogService
    {
        void AdicionarLogErro(string interfaceService, string protocoloRetorno, string codigoErro, string descricao, TipoMensagem tipoMensagem);

        void AdicionarInformacao(string interfaceService, string descricao);


    }
}
