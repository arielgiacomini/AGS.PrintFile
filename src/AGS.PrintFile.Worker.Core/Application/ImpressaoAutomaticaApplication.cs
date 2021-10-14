using AGS.PrintFile.Worker.Core.Command;
using AGS.PrintFile.Worker.Core.Entities;
using AGS.PrintFile.Worker.Core.Infrastructure;
using AGS.PrintFile.Worker.Core.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AGS.PrintFile.Worker.Core.Application
{
    public class ImpressaoAutomaticaApplication
    {
        private static AGSPrintFileConfiguration _config { get; set; }

        public ImpressaoAutomaticaApplication()
        {
        }

        public void Execute()
        {
            _config = AGSPrintFileConfiguration.LoadFile();

            AGSPrintFileLogger.Logger("Iniciando o método Execute()");

            while (true)
            {
                Thread.Sleep(5000);

                var arquivosFisicosInPath = ArquivoQuery.ArquivosParaImprimir();
                AGSPrintFileLogger.Logger($"Buscou os arquivos fisicos: {arquivosFisicosInPath}");

                var arquivosBancoDados = BancoDadosQuery.GetForAll();
                AGSPrintFileLogger.Logger($"Buscou os arquivos em banco de dados: {arquivosBancoDados}");

                var liberadosParaImpressao = ListaArquivosLiberadosParaImpressao(arquivosFisicosInPath, arquivosBancoDados);
                AGSPrintFileLogger.Logger($"Efetuou conferência das informações e liberou os seguintes arquivos: {liberadosParaImpressao}");

                foreach (var imprimir in liberadosParaImpressao)
                {
                    var impressaoRealizada = ImpressaoCommand.Imprimir(imprimir);
                    AGSPrintFileLogger.Logger($"O envio para impressora ocorreu? {impressaoRealizada}");

                    if (impressaoRealizada)
                    {
                        BancoDadosCommand.UpdatePrintFile(imprimir);
                        AGSPrintFileLogger.Logger($"Atualiza banco de dados após impressão. UpdatePrintFile()");

                        Thread.Sleep(Convert.ToInt32(_config.TempoEsperarParaMoverArquivo));

                        ArquivoCommand.MoverArquivoParaDiretorioJaImpressos(imprimir);
                        AGSPrintFileLogger.Logger($"Movendo arquivo do diretorio de origem para o já Impresso");
                    }
                }
            }
        }

        private static IList<ControlePDF> ListaArquivosLiberadosParaImpressao(IList<ControlePDF> arquivosFisicos, IList<ControlePDF> arquivosBancoDados)
        {
            IList<ControlePDF> devolutiva = new List<ControlePDF>();

            foreach (var arquivo in arquivosFisicos)
            {
                var checagem = arquivosBancoDados.Where(x => x.Arquivo == arquivo.Arquivo && x.Pasta == arquivo.Pasta).FirstOrDefault();

                if (checagem?.Impresso.Value.Equals(false) ?? false)
                {
                    devolutiva.Add(arquivo);
                }
                else if (checagem == null)
                {
                    var controlePDF = new ControlePDF
                    {
                        Pasta = arquivo.Pasta,
                        Arquivo = arquivo.Arquivo,
                        Impresso = false,
                        DataCadastro = arquivo.DataCadastro,
                        DataImpressao = null
                    };

                    BancoDadosCommand.SavePrintFile(controlePDF);

                    devolutiva.Add(controlePDF);
                }
                else
                {
                    var controlePDF = new ControlePDF
                    {
                        Pasta = arquivo.Pasta,
                        Arquivo = arquivo.Arquivo,
                        Impresso = true,
                        DataCadastro = arquivo.DataCadastro,
                        DataImpressao = DateTime.Now
                    };

                    BancoDadosCommand.UpdatePrintFile(controlePDF);

                    ArquivoCommand.MoverArquivoParaDiretorioJaImpressos(controlePDF);

                    devolutiva.Add(controlePDF);
                }
            }

            return devolutiva;
        }
    }
}