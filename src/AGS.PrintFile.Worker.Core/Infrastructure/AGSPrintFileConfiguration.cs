using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace AGS.PrintFile.Worker.Core.Infrastructure
{
    public class AGSPrintFileConfiguration
    {
        public string ServiceName { get; set; }
        public string DiretorioPrincipalAplicacao { get; set; }
        public string DiretorioFilesParaImpressao { get; set; }
        public string DiretorioFilesDepoisImpressao { get; set; }
        public string ArquivoBancoDados { get; set; }
        public string TempoEsperarParaMoverArquivo { get; set; }
        public string ImpressaoColorido { get; set; }
        public string PathLogDefault { get; set; }

        public static AGSPrintFileConfiguration LoadFile()
        {
            try
            {
                string fileDirectory = $@"{AppContext.BaseDirectory}appsettings.json";
                var json = File.ReadAllText(fileDirectory, Encoding.UTF8);

                return JsonConvert.DeserializeObject<AGSPrintFileConfiguration>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}