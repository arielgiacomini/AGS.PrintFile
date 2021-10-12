using AGS.PrintFile.Worker.Entities;
using System.Diagnostics;
using System.Drawing.Printing;

namespace AGS.PrintFile.Worker.Command
{
    public static class ImpressaoCommand
    {
        public static bool Imprimir(ControlePDF controlePDF)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    Verb = "open",
                    FileName = $@"{controlePDF.Pasta}\{controlePDF.Arquivo}",
                }
            };

            return process.Start();
        }

        public static bool Impressao2ponto0(ControlePDF controlePDF)
        {
            PrinterSettings printerSettings = new PrinterSettings()
            {
                PrinterName = "HP Officejet J3600 series",
                Copies = 1
            };

            PageSettings pageSettings = new PageSettings(printerSettings)
            {
                Margins = new Margins(0, 0, 0, 0)
            };

            foreach (PaperSize paperSize in printerSettings.PaperSizes)
            {
                if (paperSize.PaperName == "A4")
                {
                    pageSettings.PaperSize = paperSize;
                    break;
                }
            }

            using (PdfDocument pdfDocument = PdfDocument.Load(@"C:\Users\ariel\OneDrive\Documentos\AC-845000113535.pdf"))
            {
                using (PrintDocument printDocument = pdfDocument.CreatePrintDocument())
                {
                    printDocument.PrinterSettings = printerSettings;
                    printDocument.DefaultPageSettings = pageSettings;
                    printDocument.PrintController = (PrintController)new StandardPrintController();
                    printDocument.Print();
                }
            }

            return false;
        }
    }
}