using BetaViews.Core.Services.Avaliacoes.Loja;
using BetaViews.Messages;
using BetaViews.WinService.DI;
using Microsoft.Practices.Unity;
using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;

namespace BetaViews.WinService
{
    partial class ModeracaoService : ServiceBase
    {
        string sSource = "ModeracaoService";
        string sLog = "Aplicativo";
        Timer timer;


        private UnityContainer container = new UnityContainer();

        public ModeracaoService()
        {            
            InitializeComponent();
            container.AddExtension(new ModelContainerExtension());
        }

        protected override void OnStart(string[] args)
        {
            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

            try
            {
                timer.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["FrequenciaExecucaoEmMilesegundos"]);
            }
            catch
            {
                timer.Interval = 1800000d;
            }

            timer.Start();
            EventLog.WriteEntry(sSource, "Serviço iniciado com êxito.", EventLogEntryType.Information);
        }

        public void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            timer.Stop();
            Executar();
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
            EventLog.WriteEntry(sSource, "Serviço parado com êxito.", EventLogEntryType.Information);
        }

        public void Executar()
        {
            var logService = container.Resolve<Core.Services.Logs.LogService>();
            try
            {
                var moderacaoLojaService = container.Resolve<Core.Services.Avaliacoes.Loja.ModeracaoService>();

                var result = "";
                result += $"MODERAÇÃO AUTOMATICA:\n";
                result += $"QA: {Task.Run(() => moderacaoLojaService.ExecutaModeracaoQA()).Result} \n";
                result += $"AVALIACAO: {Task.Run(() => moderacaoLojaService.ExecutaModeracaoAvaliacoes()).Result}\n";

                logService.AdicionarInformacao(sSource, $"Execução de serviço {sSource}.\n{result}");

            }
            catch (Exception ex)
            {                
                logService.AdicionarLogErro(sSource, Guid.NewGuid().ToString(), "-", $"Ocorreu um erro no serviço {sSource}. Informações do Erro: {ex}\n", TipoMensagem.ErroAplicacao);
                EventLog.WriteEntry(sSource, $"Ocorreu um erro no serviço {sSource}. Trace do Erro:\n {ex}", EventLogEntryType.Error);
            }

        }
    }
}
