using AracYanimdaApi.Models.Kullanici;
using System.Data;

namespace AracYanimdaApi.Data.Repository
{
    public class UserRepository
    {
        private readonly MySqlDatabaseConnection dbConnection;

        public UserRepository()
        {
            this.dbConnection = new MySqlDatabaseConnection();
        }

        public DataTable GetAllUsers()
        {
            string storedProcedure = "spGetAllUsers";
            return dbConnection.ExecuteQuery("CALL " + storedProcedure);
        }

    
       
        public bool Register(string ad,string soyad,string eposta,string sifre,string telefon,bool sozlesme_kabul)
        {
            try
            {
                string query = $"INSERT INTO users (ad, soyad, e_posta, sifre, telefon, sozlesme_kabul) VALUES ('{ad}', '{soyad}', '{eposta}', '{sifre}', '{telefon}', {sozlesme_kabul})";
                dbConnection.ExecuteNonQuery(query);
                return true;
            }catch (Exception e)
            {
                return false;
            }
        }
        public DataTable EmailKontrol( string eposta)
        {
            string query = $"SELECT * FROM users WHERE e_posta = '{eposta}'";
              return  dbConnection.ExecuteQuery(query);
        }
        public DataTable GetUserById(int userId)
        {
            string query = $"SELECT * FROM users WHERE user_id = {userId}";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable GetLicenseById(int id)
        {
            string query = $"Select * From licenses where user_id={id} Limit 1";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable GetByRezervasyon(int id)
        {
            string query = $"SELECT CONCAT(vehicles.marka,' ', vehicles.model) AS arac_ismi,vehicles.plaka, rezervasyon.baslangic_tarihi, rezervasyon.bitis_tarihi ,rezervasyon.durum,payments.miktar FROM  rezervasyon INNER JOIN  vehicles ON vehicles.vehicle_id = rezervasyon.vehicle_id inner join payments on payments.rezervation_id = rezervasyon.rezervation_id WHERE  user_id = {id}";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable GetByPaymentMethodsById(int id)
        {
            string query = $"Select * from paymentmethods where user_id={id}";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable GetByPayments(int id)
        {
            string query = $"Select * from payments inner join rezervasyon ON rezervasyon.rezervation_id=payments.rezervation_id where rezervasyon.user_id={id}";
            return dbConnection.ExecuteQuery(query) ;
        }
        public DataTable RezervasyonVarmi(int user_id)
        {
            string query = $"Select rezervation_id from rezervasyon where user_id={user_id} AND durum='Rezerve'";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable Authenticate(string eposta, string password)
        {
            string query = $"SELECT * FROM users WHERE e_posta = '{eposta}' AND sifre = '{password}'";
            return dbConnection.ExecuteQuery(query);
        }
        public bool UpdateEmail(int id,string email)
        {
            try
            {
                string query = $"Update users Set e_posta='{email}' where user_id={id}";
                dbConnection.ExecuteNonQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdatePhone(int id, string telefon)
        {
            try
            {
                string query = $"Update users Set telefon='{telefon}' where user_id={id}";
                dbConnection.ExecuteNonQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateDurum(int id, string durum)
        {
            try
            {
                string query = $"Update users Set durum='{durum}' where user_id={id}";
                dbConnection.ExecuteNonQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool KullaniciSil(int id)
        {
            try
            {
                string query = $"Delete From users where user_id={id}";
                dbConnection.ExecuteNonQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EhliyetEkle(string ehliyet_tip,string ehliyet_no,string son_gecerlilik,int user_id)
        {
            try
            {
                string query = $"Insert Into licenses (ehliyet_tipi,ehliyet_no,son_gecerlilik_tarihi,user_id) Values('{ehliyet_tip}','{ehliyet_no}','{son_gecerlilik}',{user_id})";
                dbConnection.ExecuteNonQuery(query);
                return true;
            }
            catch { return false; }
        }
        public DataTable EhliyetBilgileri(int id)
        {
            string query = $"Select * from licenses where user_id={id}";
            return dbConnection.ExecuteQuery(query);
        }
     

    }
}
