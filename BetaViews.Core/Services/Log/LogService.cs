using System;
using BetaViews.Messages;
using BetaViews.Core.DataBase.Entitys;
using BetaViews.Core.DataBase.Repository.Interface;

namespace BetaViews.Core.Services.Logs
{
    public class LogService : ILogService
    {

        private readonly ILogErrosRepository _logService;

        public LogService(ILogErrosRepository logService)
        {
           this._logService = logService;
        }

        public void AdicionarInformacao(string interfaceService, string descricao)
        {
            _logService.Add(new LogErros
            {
                Data = DateTime.Now,
                CodigoErro="-",
                Descricao = descricao,
                Interface = interfaceService,
                ProtocoloRetorno = Guid.NewGuid().ToString(),
                TipoMensagem = (int)TipoMensagem.NenhumErro
            }
            );
        }

        /// <summary>
        /// adiciona log de erro da interface
        /// </summary>
        /// <param name="interfaceService"></param>
        /// <param name="protocoloRetorno"></param>
        /// <param name="codigoErro"></param>
        /// <param name="descricao"></param>
        /// <param name="tipoMensagem"></param>
        public void AdicionarLogErro(string interfaceService, string protocoloRetorno, string codigoErro, string descricao, TipoMensagem tipoMensagem)
        {
            _logService.Add(new LogErros { CodigoErro= codigoErro, Data=DateTime.Now, Descricao=descricao, Interface= interfaceService, ProtocoloRetorno= protocoloRetorno, TipoMensagem = (int)tipoMensagem  });
        }
    }
}
