using System;
using System.Data.SQLite;

namespace AGS.PrintFile.Worker.Infrastructure
{
    public static class AGSPrintFileDbContext
    {
        private static SQLiteConnection SQLiteConnection;
        public static AGSPrintFileConfiguration _aGSPrintFileConfiguration = new AGSPrintFileConfiguration();

        public static SQLiteConnection DbConnection()
        {
            SQLiteConnection = new SQLiteConnection($"DataSource={_aGSPrintFileConfiguration.DiretorioPrincipalAplicacao}AGS.PrintFile.Worker.sqlite; Version=3;");
            SQLiteConnection.Open();
            return SQLiteConnection;
        }

        public static void CriarBancoSQLite()
        {
            try
            {
                SQLiteConnection.CreateFile($"{_aGSPrintFileConfiguration.DiretorioPrincipalAplicacao}AGS.PrintFile.Worker.sqlite");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS ControlePDF(Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Pasta VARCHAR(200), Arquivo VARCHAR(200), Impresso BIT, DataCadastro DATETIME, DataImpressao DATETIME)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}