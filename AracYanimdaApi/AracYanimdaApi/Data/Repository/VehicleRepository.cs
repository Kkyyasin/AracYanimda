using AracYanimdaApi.Models.Arac;
using System.Data;

namespace AracYanimdaApi.Data.Repository
{
    public class VehicleRepository
    {
        private readonly MySqlDatabaseConnection dbConnection;

        public VehicleRepository()
        {
            this.dbConnection = new MySqlDatabaseConnection();
        }
        public DataTable GetAllVehicles()
        {
            string storedProcedure = "spGetVehiclesDetails";
            return dbConnection.ExecuteQuery("CALL " + storedProcedure);
        }
        public bool Create(string marka,string model,int yil,string plaka ,string yakit_tipi,string yakit_durumu,int location_id,int fiyat_id)
        {
            try
            {
                string query = $"INSERT INTO vehicles (marka, model, yil, plaka, yakit_tipi, yakit_durumu, location_id, fiyat_id) VALUES ('{marka}', '{model}', {yil}, '{plaka}','{yakit_tipi}', '{yakit_durumu}', {location_id},{fiyat_id})";
                 dbConnection.ExecuteNonQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public DataTable GetKonum()
        {
            string query = $"select vehicle_id,marka,model,latitude,longitude from vehicles inner join locations ON vehicles.location_id=locations.location_id";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable GetAracKonum()
        {
            string query = $"select vehicle_id,marka,model,latitude,longitude from vehicles inner join locations ON vehicles.location_id=locations.location_id";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable CreateLocation(Decimal latitude,Decimal longitude,string adress,string city)
        {
         
                string query = $"Insert Into locations (latitude,longitude,address,city) Values( {latitude},{longitude},'{adress}','{city}')";
                dbConnection.ExecuteQuery(query);
                string query2 = "SELECT LAST_INSERT_ID()";
                return dbConnection.ExecuteQuery(query2);
        }
        public DataTable CreateAracFiyat(Decimal fiyat_gunluk, Decimal fiyat_dakika, int km_siniri_ucretsiz , Decimal ilave_km)
        {

            string query = $"Insert Into arac_fiyat (fiyat_gunluk,fiyat_dakika,km_siniri_ucretsiz,ilave_km_ucreti) Values( {fiyat_gunluk},{fiyat_dakika},{km_siniri_ucretsiz},{ilave_km})";
            dbConnection.ExecuteQuery(query);
            string query2 = "SELECT LAST_INSERT_ID()";
            return dbConnection.ExecuteQuery(query2);
        }
        
        public DataTable OtomatikAracMarka()
        {
            string query = "select marka from otomobil_fiyatlari group by marka";
            return dbConnection.ExecuteQuery (query);
        }
        public DataTable OtomatikAracModel(string marka)
        {
            string query = $"select model from otomobil_fiyatlari where marka='{marka}' group by model";
            return dbConnection.ExecuteQuery(query);
        }

        public DataTable OtomatikAracYakit(string marka,string model)
        {
            string query = $"select yakit  from otomobil_fiyatlari where marka='{marka}' AND model='{model}' group by yakit" ;
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable OtomatikAracFiyat(string marka, string model,string yakit)
        {
            string query = $"select fiyat from otomobil_fiyatlari where marka='{marka}' AND model='{model}' AND yakit='{yakit}' LIMIT 1";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable GetAracAll()
        {
            string query = $"select * from vehicles inner join locations ON vehicles.location_id=locations.location_id inner join arac_fiyat ON arac_fiyat.fiyat_id=vehicles.fiyat_id";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable GetByVehicleId(int id)
        {
            string query= $"select * from vehicles where vehicle_id = {id}";
            return dbConnection.ExecuteQuery(query) ;
        }
        public DataTable GetByFiyatId(int id)
        {
            string query = $"select * from arac_fiyat where fiyat_id = {id}";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable GetByLocationId(int id)
        {
            string query = $"select * from locations where location_id = {id} ";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable GetByBakımId(int id)
        {
            string query = $"select * from bakimbilgileri where vehicle_id = {id}";
            return dbConnection.ExecuteQuery(query);
        }
        public bool FiyatGuncelle(decimal gunlukFiyat, decimal dakikaFiyat,int id)
        {
            try { 
            string query = $"Update arac_fiyat Set fiyat_gunluk={gunlukFiyat} , fiyat_dakika={dakikaFiyat} where fiyat_id=(select fiyat_id from vehicles where vehicle_id={id})";
            dbConnection.ExecuteNonQuery(query);
             return true;
            }
            catch (Exception ex) {
                return false;
            }
        }
        public bool BakimEkle(int VehicleId, string bakimAciklamasi, string bakimTarihi)
        {
            try
            {
                string query = $"Insert Into bakimbilgileri (bakim_tarihi,bakim_aciklamasi,vehicle_id) Values('{bakimTarihi}','{bakimAciklamasi}',{VehicleId})";
                dbConnection.ExecuteNonQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }    
        }
       public bool BakimSil(int bakimId)
        {
            try
            {
                string query = $"Delete From bakimbilgileri where bakim_id={bakimId}";
                dbConnection.ExecuteNonQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AracSil(int vehicleId)
        {
            try
            {
                string query = $"Delete From vehicles where vehicle_id={vehicleId}";
                dbConnection.ExecuteNonQuery(query);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}
