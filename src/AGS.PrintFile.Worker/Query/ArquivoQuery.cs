using AGS.PrintFile.Worker.Entities;
using AGS.PrintFile.Worker.Infrastructure;
using System.Collections.Generic;
using System.IO;

namespace AGS.PrintFile.Worker.Query
{
    public static class ArquivoQuery
    {
        public static AGSPrintFileConfiguration _aGSPrintFileConfiguration = new AGSPrintFileConfiguration();

        public static IList<ControlePDF> ArquivosParaImprimir()
        {
            IList<ControlePDF> listPrintFile = new List<ControlePDF>();

            var diretorio = $@"{_aGSPrintFileConfiguration.DiretorioPrincipalAplicacao}{_aGSPrintFileConfiguration.DiretorioFilesParaImpressao}";

            string[] arquivos = Directory.GetFiles(diretorio);

            foreach (string arq in arquivos)
            {
                FileInfo informacoesDoArquivo = new FileInfo(arq);

                listPrintFile.Add(new ControlePDF
                {
                    Pasta = informacoesDoArquivo.DirectoryName,
                    Arquivo = informacoesDoArquivo.Name,
                    DataCadastro = informacoesDoArquivo.CreationTime,
                    Impresso = null,
                    DataImpressao = null
                });
            }

            return listPrintFile;
        }
    }
}