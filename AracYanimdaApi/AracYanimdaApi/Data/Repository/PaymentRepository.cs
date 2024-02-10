using System.Data;

namespace AracYanimdaApi.Data.Repository
{
    public class PaymentRepository
    {

        private readonly MySqlDatabaseConnection dbConnection;

        public PaymentRepository()
        {
            this.dbConnection = new MySqlDatabaseConnection();
        }
        public bool CreatePayment(int rezervation_id, int payment_method_id)
        {
            try
            {
                string query = $"Insert Into payments (rezervation_id,payment_method_id,miktar) Values({rezervation_id},{payment_method_id},0)";
                dbConnection.ExecuteNonQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       
        public bool OdemeTamamla(int rezervation_id)
        {
            
                string query = $"Update payments Set odeme_durumu='Tamamlandi' where rezervation_id={rezervation_id}";
                string query2 = $"Update rezervasyon Set durum='Tamamlandi' where rezervation_id={rezervation_id}";
                string query3 = $"Update vehicles Set durum='Bosta' where vehicle_id=(select vehicle_id from rezervasyon where rezervation_id={rezervation_id})";
                string query4 = $"Update users Set durum='Bosta' where user_id=(select user_id from rezervasyon where rezervation_id={rezervation_id})";
                
                dbConnection.ExecuteNonQuery(query);
                dbConnection.ExecuteNonQuery(query2);
                dbConnection.ExecuteNonQuery(query3);
                dbConnection.ExecuteNonQuery(query4);
          
                return true;
          
        }
        public bool CreatePaymentMethod(int user_id,string adsoyad,string kart_numarasi,string cvv,string son_k)
        {
                string query = $"Insert Into paymentmethods (user_id,ad_soyad,kart_numarasi,cvv,son_kullanma) Values({user_id},'{adsoyad}','{kart_numarasi}','{cvv}','{son_k}')";
                dbConnection.ExecuteNonQuery(query);
                return true;
          
        }
        public DataTable GetPaymentMethods(int user_id)
        {
            string query = $"Select * from paymentmethods where user_id={user_id}";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable GetPayments()
        {
            string storedProcedure = "spGetPaymentDetails";
            return dbConnection.ExecuteQuery("CALL " + storedProcedure);
        }
        public DataTable GetBilgi(int rezervation_id)
        {
            string query = $"select payments.miktar,rezervasyon.km from payments inner join rezervasyon On payments.rezervation_id=rezervasyon.rezervation_id where rezervasyon.rezervation_id={rezervation_id}";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable Fatura(int payment_id)
        {
            string query = $"select * from payments inner join rezervasyon on rezervasyon.rezervation_id=payments.rezervation_id" +
                $" inner join paymentmethods on paymentmethods.payment_method_id=payments.payment_method_id " +
                $"inner join vehicles on rezervasyon.vehicle_id=vehicles.vehicle_id " +
                $"inner join users on rezervasyon.user_id=users.user_id where payments.payment_id={payment_id}";
            return dbConnection.ExecuteQuery(query);
        }
       
    }
}
