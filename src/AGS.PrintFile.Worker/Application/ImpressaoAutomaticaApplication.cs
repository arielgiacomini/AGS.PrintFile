using AGS.PrintFile.Worker.Command;
using AGS.PrintFile.Worker.Entities;
using AGS.PrintFile.Worker.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AGS.PrintFile.Worker.Application
{
    public class ImpressaoAutomaticaApplication
    {
        public ImpressaoAutomaticaApplication()
        {

        }

        public void Execute()
        {
            while (true)
            {
                var arquivosFisicosInPath = ArquivoQuery.ArquivosParaImprimir();

                var arquivosBancoDados = BancoDadosQuery.GetForAll();

                var liberadosParaImpressao = ListaArquivosLiberadosParaImpressao(arquivosFisicosInPath, arquivosBancoDados);

                foreach (var imprimir in liberadosParaImpressao)
                {
                    var impressaoRealizada = ImpressaoCommand.Imprimir(imprimir);

                    if (impressaoRealizada)
                    {
                        BancoDadosCommand.UpdatePrintFile(imprimir);

                        Thread.Sleep(60000);

                        ArquivoCommand.MoverParaJaImpressos(imprimir);
                    }
                }
            }
        }

        private IList<ControlePDF> ListaArquivosLiberadosParaImpressao(IList<ControlePDF> arquivosFisicos, IList<ControlePDF> arquivosBancoDados)
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
                    BancoDadosCommand.SavePrintFile(new ControlePDF
                    {
                        Pasta = arquivo.Pasta,
                        Arquivo = arquivo.Arquivo,
                        Impresso = false,
                        DataCadastro = arquivo.DataCadastro,
                        DataImpressao = null
                    });
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

                    ArquivoCommand.MoverParaJaImpressos(controlePDF);
                }
            }

            return devolutiva;
        }
    }
}