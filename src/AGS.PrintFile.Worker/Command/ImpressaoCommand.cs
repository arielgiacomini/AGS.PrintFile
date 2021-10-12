using AGS.PrintFile.Worker.Entities;
using AGS.PrintFile.Worker.Infrastructure;
using ceTe.DynamicPDF.Printing;
using System;

namespace AGS.PrintFile.Worker.Command
{
    public static class ImpressaoCommand
    {
        private static readonly AGSPrintFileConfiguration _aGSPrintFileConfiguration = new AGSPrintFileConfiguration();

        public static bool Imprimir(ControlePDF controlePDF)
        {
            bool result;

            try
            {
                var pathAndFile = $@"{controlePDF.Pasta}\{controlePDF.Arquivo}";

                PrintJob printJob = new PrintJob(Printer.Default, pathAndFile);

                printJob.PrintOptions.Color = Convert.ToBoolean(_aGSPrintFileConfiguration.ImpressaoColorido);

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