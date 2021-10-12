using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace AGS.PrintFile.Worker.Query
{
    public class BancoDadosQuery
    {
        public static IList<Entities.ControlePDF> GetForAll()
        {
            IList<Entities.ControlePDF> listPrintFile = new List<Entities.ControlePDF>();

            string cs = @"URI=file:C:\AGS\AGS.PrintFile.Worker.sqlite";

            using (var con = new SQLiteConnection(cs))
            {
                con.Open();

                string stm = "SELECT * FROM ControlePDF;";

                using (var cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var id = rdr["Id"].ToString();
                            var pasta = (string)rdr["Pasta"];
                            var arquivo = (string)rdr["Arquivo"];
                            var impresso = (bool?)rdr["Impresso"];
                            var dataCadastro = (DateTime)rdr["DataCadastro"];
                            var dataImpressao = rdr["DataImpressao"].ToString();

                            listPrintFile.Add(new Entities.ControlePDF
                            {
                                Id = Convert.ToInt32(id),
                                Pasta = pasta,
                                Arquivo = arquivo,
                                Impresso = impresso,
                                DataCadastro = dataCadastro,
                                DataImpressao = dataImpressao != "" ? (DateTime?)rdr["DataImpressao"] : null
                            });
                        }
                    }
                }
            }

            return listPrintFile;
        }
    }
}