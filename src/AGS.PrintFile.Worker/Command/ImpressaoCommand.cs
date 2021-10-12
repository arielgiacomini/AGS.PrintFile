using AGS.PrintFile.Worker.Entities;
using ceTe.DynamicPDF.Printing;
using System;

namespace AGS.PrintFile.Worker.Command
{
    public static class ImpressaoCommand
    {
        public static bool Imprimir3ponto0(ControlePDF controlePDF)
        {
            bool result;

            try
            {
                var pathAndFile = $@"{controlePDF.Pasta}\{controlePDF.Arquivo}";

                PrintJob printJob = new PrintJob(Printer.Default, pathAndFile);

                printJob.Print();

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