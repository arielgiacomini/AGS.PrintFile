using AGS.PrintFile.Worker.Core.Infrastructure;
using System;
using System.Data.SQLite;

namespace AGS.PrintFile.Worker.Core.Command
{
    public class BancoDadosCommand
    {
        public static long SavePrintFile(Entities.ControlePDF printFile)
        {
            try
            {
                using (var cmd = AGSPrintFileDbContext.DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO ControlePDF (Pasta, Arquivo, Impresso, DataCadastro, DataImpressao) VALUES (@Pasta, @Arquivo, @Impresso, @DataCadastro, @DataImpressao)";
                    cmd.Parameters.AddWithValue("@Pasta", printFile.Pasta);
                    cmd.Parameters.AddWithValue("@Arquivo", printFile.Arquivo);
                    cmd.Parameters.AddWithValue("@Impresso", printFile.Impresso);
                    cmd.Parameters.AddWithValue("@DataCadastro", printFile.DataCadastro);
                    cmd.Parameters.AddWithValue("@DataImpressao", printFile.DataImpressao);
                    cmd.ExecuteNonQuery();

                    long rowID = cmd.Connection.LastInsertRowId;

                    return rowID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdatePrintFile(Entities.ControlePDF printFile)
        {
            try
            {
                using (var cmd = new SQLiteCommand(AGSPrintFileDbContext.DbConnection()))
                {
                    try
                    {
                        cmd.CommandText = "UPDATE ControlePDF SET Impresso = @Impresso, DataImpressao = @DataImpressao WHERE Id = @Id";
                        cmd.Parameters.AddWithValue("@Id", printFile.Id);
                        cmd.Parameters.AddWithValue("@Impresso", printFile.Impresso);
                        cmd.Parameters.AddWithValue("@DataImpressao", printFile.DataImpressao);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}