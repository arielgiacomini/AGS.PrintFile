﻿using AGS.PrintFile.Worker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace AGS.PrintFile.Worker.Query
{
    public class BancoDadosQuery
    {
        private static readonly AGSPrintFileConfiguration _aGSPrintFileConfiguration = new AGSPrintFileConfiguration();

        public static IList<Entities.ControlePDF> GetForAll()
        {
            IList<Entities.ControlePDF> listPrintFile = new List<Entities.ControlePDF>();

            string cs = _aGSPrintFileConfiguration.ArquivoBancoDados;

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
                                var impresso = (bool?)sqliteDataReader["Impresso"];
                                var dataCadastro = (DateTime)sqliteDataReader["DataCadastro"];
                                var dataImpressao = sqliteDataReader["DataImpressao"].ToString();

                                listPrintFile.Add(new Entities.ControlePDF
                                {
                                    Id = Convert.ToInt32(id),
                                    Pasta = pasta,
                                    Arquivo = arquivo,
                                    Impresso = impresso,
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