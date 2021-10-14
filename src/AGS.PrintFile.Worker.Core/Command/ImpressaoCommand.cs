using AGS.PrintFile.Worker.Core.Entities;
using AGS.PrintFile.Worker.Core.Infrastructure;
using ceTe.DynamicPDF.Printing;
using System;
using System.Linq;

namespace AGS.PrintFile.Worker.Core.Command
{
    public static class ImpressaoCommand
    {
        private static AGSPrintFileConfiguration _config { get; set; }

        public static bool Imprimir(ControlePDF controlePDF)
        {
            _config = AGSPrintFileConfiguration.LoadFile();

            bool result;

            try
            {
                var pathAndFile = $@"{controlePDF.Pasta}\{controlePDF.Arquivo}";

                var impressoras = Printer.GetLocalPrinters();

                var impressoraDefinida = impressoras.Where(x => x.Name == _config.NomeImpressora).FirstOrDefault();

                PrintJob printJob = new PrintJob(impressoraDefinida, pathAndFile);

                printJob.PrintOptions.Color = Convert.ToBoolean(_config.ImpressaoColorido);

                printJob.Print();

                printJob.Dispose();

                result = true;
            }
            catch (Exception ex)
            {
                AGSPrintFileLogger.Logger($"Erro no Método Imprimir(): {ex}");
                throw ex;
            }

            return result;
        }
    }
}