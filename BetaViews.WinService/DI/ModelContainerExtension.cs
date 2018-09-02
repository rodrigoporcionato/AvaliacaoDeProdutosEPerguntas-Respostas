using BetaViews.Core.Services.Logs;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.WinService.DI
{
    public class ModelContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {            
            Container.RegisterType<Core.DataBase.Repository.Interface.ILogErrosRepository, Core.DataBase.Repository.LogErrosRepository>();
            Container.RegisterType<ILogService, LogService>();
            Container.RegisterType<Core.Services.Avaliacoes.Loja.IAvaliacaoService, Core.Services.Avaliacoes.Loja.AvaliacoesService>();
            Container.RegisterType<Core.DataBase.Repository.Interface.IAvaliacaoRepository, Core.DataBase.Repository.AvaliacaoRepository>();
            Container.RegisterType<Core.DataBase.Repository.Interface.ILojaRepository, Core.DataBase.Repository.LojaRepository>();            
            Container.RegisterType<Core.DataBase.Repository.Interface.IClienteRepository, Core.DataBase.Repository.ClienteRepository>();
            Container.RegisterType<Core.Services.Avaliacoes.Loja.IModeracaoService, Core.Services.Avaliacoes.Loja.ModeracaoService>();
            Container.RegisterType<Core.DataBase.Repository.Interface.IPalavraRecusadaPadraoRepository, Core.DataBase.Repository.PalavraRecusadaPadraoRepository>();   
            Container.RegisterType<Core.Services.QA.IQAService, Core.Services.QA.QAService>();
            Container.RegisterType<Core.DataBase.Repository.Interface.IQARepository, Core.DataBase.Repository.QARepository>();
        }
    }
}
