using AGS.PrintFile.Worker.Infrastructure;
using System.IO;

namespace AGS.PrintFile.Worker.Command
{
    public static class ArquivoCommand
    {
        public static AGSPrintFileConfiguration _aGSPrintFileConfiguration = new AGSPrintFileConfiguration();

        public static void MoverParaJaImpressos(Entities.ControlePDF arquivoParaMover)
        {
            File.Move($@"{arquivoParaMover.Pasta}\{arquivoParaMover.Arquivo}", $@"{_aGSPrintFileConfiguration.DiretorioPrincipalAplicacao}\Ja_Impresso\");
        }
    }
}