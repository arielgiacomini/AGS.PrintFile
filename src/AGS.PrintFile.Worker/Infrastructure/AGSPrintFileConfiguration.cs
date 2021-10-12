using System.Configuration;

namespace AGS.PrintFile.Worker.Infrastructure
{
    public class AGSPrintFileConfiguration
    {
        public string ServiceName => ConfigurationManager.AppSettings["service.name"];
        public string DiretorioPrincipalAplicacao => ConfigurationManager.AppSettings["diretorio.principal"];
        public string DiretorioFilesParaImpressao => ConfigurationManager.AppSettings["diretorio.para.impressao"];
        public string DiretorioFilesDepoisImpressao => ConfigurationManager.AppSettings["diretorio.depois.impresso"];
        public string ArquivoBancoDados => ConfigurationManager.AppSettings["arquivo.banco.dados"];
        public string TempoEsperarParaMoverArquivo => ConfigurationManager.AppSettings["tempo.pos.impressao.para.mover.arquivo"];
        public string ImpressaoColorido => ConfigurationManager.AppSettings["impimir.colorido"];

    }
}