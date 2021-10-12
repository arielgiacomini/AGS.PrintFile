using AGS.PrintFile.Worker.Command;
using AGS.PrintFile.Worker.Entities;
using AGS.PrintFile.Worker.Query;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var arquivosFisicos = ArquivoQuery.ArquivosParaImprimir();

                var arquivosBancoDados = BancoDadosQuery.GetForAll();

                var disponiveisParaImpressao = ParaImprimirPosChecagem(arquivosFisicos, arquivosBancoDados);

                foreach (var imprimir in disponiveisParaImpressao)
                {
                    var impressaoRealizada = ImpressaoCommand.Imprimir3ponto0(imprimir);

                    if (impressaoRealizada)
                    {
                        BancoDadosCommand.UpdatePrintFile(imprimir);

                        ArquivoCommand.MoverParaJaImpressos(imprimir);
                    }
                }
            }
        }

        private IList<ControlePDF> ParaImprimirPosChecagem(IList<ControlePDF> arquivosFisicos, IList<ControlePDF> arquivosBancoDados)
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