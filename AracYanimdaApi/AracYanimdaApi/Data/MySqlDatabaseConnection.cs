using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace AracYanimdaApi.Data
{
    public class MySqlDatabaseConnection
    {
        private readonly string _connectionString;
        public MySqlDatabaseConnection()
        {
            _connectionString = "Server=cloud-aracimyanimda.mysql.database.azure.com;Database=aracyanimda;Port=3306;Uid=cloudaracimyanimda;Pwd=Marmara1234.;SSL Mode=Required";
        }

        public DataTable ExecuteQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata yönetimi burada yapılabilir
                    throw ex;
                }
            }
        }
        public void ExecuteNonQuery(string query)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = new MySqlCommand(query, connection))
                    {
                        try
                        {
                            command.Transaction = transaction;
                            command.ExecuteNonQuery();

                          
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                         
                            transaction.Rollback();
                            throw;  
                        }
                    }
                }
            }
        }
    }
}
