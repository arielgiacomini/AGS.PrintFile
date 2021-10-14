using AGS.PrintFile.Worker.Core.Application;
using AGS.PrintFile.Worker.Core.Infrastructure;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace AGS.PrintFile.Worker.Core
{
    public class Worker : BackgroundService
    {
        private readonly ImpressaoAutomaticaApplication _impressaoAutomaticaApplication;

        public Worker(ImpressaoAutomaticaApplication impressaoAutomaticaApplication)
        {
            _impressaoAutomaticaApplication = impressaoAutomaticaApplication;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                AGSPrintFileLogger.Logger("Servi�o de Envio de Impress�o Autom�tica sendo iniciando...");

                _impressaoAutomaticaApplication.Execute();

                AGSPrintFileLogger.Logger("_impressaoAutomaticaApplication.Execute() executado.");

                await Task.Delay(1000, stoppingToken);
            }

            AGSPrintFileLogger.Logger("Finalizado o servi�o.");
        }
    }
}