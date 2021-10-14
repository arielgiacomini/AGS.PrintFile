using AGS.PrintFile.Worker.Core.Entities;
using AGS.PrintFile.Worker.Core.Infrastructure;
using System.Collections.Generic;
using System.IO;

namespace AGS.PrintFile.Worker.Core.Query
{
    public static class ArquivoQuery
    {
        private static AGSPrintFileConfiguration _config { get; set; }

        public static IList<ControlePDF> ArquivosParaImprimir()
        {
            _config = AGSPrintFileConfiguration.LoadFile();

            IList<ControlePDF> listPrintFile = new List<ControlePDF>();

            var diretorio = $@"{_config.DiretorioPrincipalAplicacao}{_config.DiretorioFilesParaImpressao}";

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