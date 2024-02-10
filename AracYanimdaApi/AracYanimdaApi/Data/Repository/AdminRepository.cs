using System.Data;

namespace AracYanimdaApi.Data.Repository
{
    public class AdminRepository
    {
        private readonly MySqlDatabaseConnection dbConnection;

        public AdminRepository()
        {
            this.dbConnection = new MySqlDatabaseConnection();
        }
        public DataTable Login(string email,string password)
        {
            string query = $"select * from admin where email='{email}' and password='{password}'";
            return dbConnection.ExecuteQuery(query);
        }
        public bool Register(string email, string password,string username)
        {
            try
            {
                string query = $"Insert Into admin (email,password,username) Values('{email}','{password}','{username}')";
                dbConnection.ExecuteNonQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
      
    }
}
