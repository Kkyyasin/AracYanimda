using System.Data;

namespace AracYanimdaApi.Data.Repository
{
    public class RezervasyonRepository
    {
        private readonly MySqlDatabaseConnection dbConnection;

        public RezervasyonRepository()
        {
            this.dbConnection = new MySqlDatabaseConnection();
        }
        public DataTable CreateRezervasyon(int vehicleId,int userId,string baslangic)
        {
            try
            {
                string query = $"Insert Into rezervasyon (vehicle_id,user_id,baslangic_tarihi,tur,durum) Values({vehicleId},{userId},'{baslangic}','Saatlik','Rezerve')";
                dbConnection.ExecuteQuery(query);
                string query2 = "SELECT LAST_INSERT_ID()";
                return dbConnection.ExecuteQuery(query2);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable CreateRezervasyon(int vehicleId, int userId,string bas,string son)
        {
            try
            {
                string query = $"Insert Into rezervasyon (vehicle_id,user_id,baslangic_tarihi,bitis_tarihi,tur,durum) Values({vehicleId},{userId},'{bas}','{son}','Gunluk','Rezerve')";
                dbConnection.ExecuteQuery(query);
                string query2 = "SELECT LAST_INSERT_ID()";
                return dbConnection.ExecuteQuery(query2); ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable Kontrol(int vehicleId,int user_id) {
            string query = $"Select Count(*) from rezervasyon where (user_id={user_id}  AND durum='Rezerve') OR (vehicle_id={vehicleId}  AND durum='Rezerve')";
            return dbConnection.ExecuteQuery (query);
        }
        public bool DurumGuncelle(string durum,int vehicle_id,int user_id)
        {
            try {
                string query = $"Update vehicles Set durum='{durum}' where vehicle_id={vehicle_id} ";
                string query2 = $"Update users Set durum='{durum}' where user_id={user_id} ";

                dbConnection.ExecuteNonQuery(query);
                dbConnection.ExecuteNonQuery(query2);
                return true;
            }
            catch
            (Exception ex)
            {
                return false;
            }
        }
        public DataTable GetRezervasyonAll()
        {
            string storedProcedure = "spGetCompleteRezervationDetails";
            return dbConnection.ExecuteQuery("CALL " + storedProcedure);
        }
        public DataTable GetByRezervasyonId(int rezervasyon_id)
        {
            string query = $"select * from rezervasyon where rezervation_id={rezervasyon_id}";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable GetByUserRezervasyonId(int rezervasyon_id)
        {
            string query = $"Select * from users inner join rezervasyon On rezervasyon.user_id=users.user_id where rezervasyon.rezervation_id={rezervasyon_id}"; 
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable GetByVehiclesId(int rezervasyon_id)
        {
            string query = $"Select * from vehicles inner join rezervasyon On rezervasyon.vehicle_id=vehicles.vehicle_id where rezervasyon.rezervation_id={rezervasyon_id}";
            return dbConnection.ExecuteQuery(query);
        }
         public DataTable GetByPaymentsRezervasyonId(int rezervasyon_id)
            {
                string query = $"Select * from payments inner join rezervasyon On rezervasyon.rezervation_id=payments.rezervation_id where rezervasyon.rezervation_id={rezervasyon_id}";
                return dbConnection.ExecuteQuery(query);
           }
        public DataTable RezerveSayisi(int max,int min)
        {
            string query = $"Select Count(*) from rezervasyon  group by rezervation_id,durum,miktar Having miktar>{min} AND miktar<{max} ";
            return dbConnection.ExecuteQuery(query);
        }
    }
}

