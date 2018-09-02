using BetaViews.API.Filters;
using BetaViews.Core.DataBase.Repository;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Core.Services.Avaliacoes.Loja;
using BetaViews.Core.Services.Logs;
using BetaViews.Core.Services.QA;
using Microsoft.Practices.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Unity.WebApi;

namespace BetaViews.API
{
    

    public static class UnityConfig
    {
        

        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            
            
            container.RegisterType<ILogErrosRepository,LogErrosRepository>();
            container.RegisterType<ILogService, LogService>();

            container.RegisterType<IAvaliacaoService, AvaliacoesService>();
            container.RegisterType<IAvaliacaoRepository, AvaliacaoRepository>();
            container.RegisterType<ILojaRepository, LojaRepository>();            
            container.RegisterType<IClienteRepository, ClienteRepository>();
            container.RegisterType<IModeracaoService, ModeracaoService>();   
            container.RegisterType<IPalavraRecusadaPadraoRepository, PalavraRecusadaPadraoRepository>();   
            container.RegisterType<IQAService, QAService>();
            container.RegisterType<IQARepository, QARepository>();

            
         



            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);



        }
    }


    

}