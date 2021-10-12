using AGS.PrintFile.Worker.Infrastructure;
using System.Collections.Generic;
using System.IO;

namespace AGS.PrintFile.Worker.Query
{
    public static class ArquivoQuery
    {
        public static AGSPrintFileConfiguration _aGSPrintFileConfiguration = new AGSPrintFileConfiguration();

        public static IList<Entities.ControlePDF> ArquivosParaImprimir()
        {
            IList<Entities.ControlePDF> listPrintFile = new List<Entities.ControlePDF>();

            string[] arquivos = Directory.GetFiles($@"{_aGSPrintFileConfiguration.DiretorioPrincipalAplicacao}{_aGSPrintFileConfiguration.DiretorioFilesParaImpressao}");

            foreach (string arq in arquivos)
            {
                FileInfo informacoesDoArquivo = new FileInfo(arq);

                listPrintFile.Add(new Entities.ControlePDF
                {
                    Pasta = informacoesDoArquivo.DirectoryName,
                    Arquivo = informacoesDoArquivo.Name,
                    DataCadastro = informacoesDoArquivo.CreationTime
                });
            }

            return listPrintFile;
        }
    }
}