using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.WinService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive && Debugger.IsAttached)
            {
                string chave = ConfigurationManager.AppSettings["InstanceFactory"];

                switch (chave)
                {
                    case "ModeracaoService":
                        new ModeracaoService().Executar();
                        return;
                    default:
                        break;
                }
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    Program.ConcreteFactory()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
        public static ServiceBase ConcreteFactory()
        {


            string chave = ConfigurationManager.AppSettings["InstanceFactory"];
            switch (chave)
            {
                case "ModeracaoService":
                    return new ModeracaoService();
                default:
                    throw new NotImplementedException();
            }

        }
    }
}
