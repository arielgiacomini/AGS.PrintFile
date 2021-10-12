using AGS.PrintFile.Worker.Infrastructure;
using System.IO;

namespace AGS.PrintFile.Worker.Command
{
    public static class ArquivoCommand
    {
        public static AGSPrintFileConfiguration _aGSPrintFileConfiguration = new AGSPrintFileConfiguration();

        public static void MoverArquivoParaDiretorioJaImpressos(Entities.ControlePDF arquivoParaMover)
        {
            var origem = $@"{arquivoParaMover.Pasta}\{arquivoParaMover.Arquivo}";
            var destino = $@"{_aGSPrintFileConfiguration.DiretorioPrincipalAplicacao}{_aGSPrintFileConfiguration.DiretorioFilesDepoisImpressao}{arquivoParaMover.Arquivo}";

            if (File.Exists(destino))
            {
                File.Delete(destino);
            }

            File.Move(origem, destino);
        }
    }
}