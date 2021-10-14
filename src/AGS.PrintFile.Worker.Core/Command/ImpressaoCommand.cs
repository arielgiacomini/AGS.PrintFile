using AGS.PrintFile.Worker.Core.Entities;
using AGS.PrintFile.Worker.Core.Infrastructure;
using ceTe.DynamicPDF.Printing;
using System;

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

                PrintJob printJob = new PrintJob(Printer.Default, pathAndFile);

                printJob.PrintOptions.Color = Convert.ToBoolean(_config.ImpressaoColorido);

                printJob.Print();

                printJob.Dispose();

                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}