using AGS.PrintFile.Worker.Core.Infrastructure;
using System.IO;

namespace AGS.PrintFile.Worker.Core.Command
{
    public static class ArquivoCommand
    {
        private static AGSPrintFileConfiguration _config { get; set; }

        public static void MoverArquivoParaDiretorioJaImpressos(Entities.ControlePDF arquivoParaMover)
        {
            _config = AGSPrintFileConfiguration.LoadFile();

            var origem = $@"{arquivoParaMover.Pasta}\{arquivoParaMover.Arquivo}";
            var destino = $@"{_config.DiretorioPrincipalAplicacao}{_config.DiretorioFilesDepoisImpressao}{arquivoParaMover.Arquivo}";

            if (File.Exists(destino))
            {
                File.Delete(destino);
            }

            File.Move(origem, destino);
        }
    }
}