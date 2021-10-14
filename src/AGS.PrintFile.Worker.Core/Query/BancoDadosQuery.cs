using AGS.PrintFile.Worker.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace AGS.PrintFile.Worker.Core.Query
{
    public class BancoDadosQuery
    {
        private static AGSPrintFileConfiguration _config { get; set; }

        public static IList<Entities.ControlePDF> GetForAll()
        {
            _config = AGSPrintFileConfiguration.LoadFile();

            IList<Entities.ControlePDF> listPrintFile = new List<Entities.ControlePDF>();

            string cs = _config.ArquivoBancoDados;

            using (var sqliteConnection = new SQLiteConnection(cs))
            {
                sqliteConnection.Open();

                string sqlQuery = "SELECT * FROM ControlePDF;";

                using (var cmd = new SQLiteCommand(sqlQuery, sqliteConnection))
                {
                    using (SQLiteDataReader sqliteDataReader = cmd.ExecuteReader())
                    {
                        while (sqliteDataReader.Read())
                        {
                            try
                            {
                                var id = sqliteDataReader["Id"].ToString();
                                var pasta = (string)sqliteDataReader["Pasta"];
                                var arquivo = (string)sqliteDataReader["Arquivo"];
                                var impresso = sqliteDataReader["Impresso"].ToString();
                                var dataCadastro = (DateTime)sqliteDataReader["DataCadastro"];
                                var dataImpressao = sqliteDataReader["DataImpressao"].ToString();

                                listPrintFile.Add(new Entities.ControlePDF
                                {
                                    Id = Convert.ToInt32(id),
                                    Pasta = pasta,
                                    Arquivo = arquivo,
                                    Impresso = Convert.ToBoolean(impresso != null && impresso != ""),
                                    DataCadastro = dataCadastro,
                                    DataImpressao = dataImpressao != "" ? (DateTime?)sqliteDataReader["DataImpressao"] : null
                                });
                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }
                        }
                    }
                }
            }

            return listPrintFile;
        }
    }
}