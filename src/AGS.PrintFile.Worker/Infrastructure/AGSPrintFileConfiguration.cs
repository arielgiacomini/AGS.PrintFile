using System.Configuration;

namespace AGS.PrintFile.Worker.Infrastructure
{
    public class AGSPrintFileConfiguration
    {
        public string ServiceName => ConfigurationManager.AppSettings["service.name"];
        public string DiretorioPrincipalAplicacao => ConfigurationManager.AppSettings["diretorio.principal"];
    }
}