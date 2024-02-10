using System.Data;

namespace AracYanimdaApi.Data.Repository
{
    public class TimerRepository
    {
        private readonly MySqlDatabaseConnection dbConnection;

        public TimerRepository()
        {
            this.dbConnection = new MySqlDatabaseConnection();
        }
        public bool KmArtir()
        {
            string query = $@"UPDATE rezervasyon
INNER JOIN vehicles ON vehicles.vehicle_id = rezervasyon.vehicle_id
INNER JOIN arac_fiyat ON vehicles.fiyat_id = arac_fiyat.fiyat_id
SET rezervasyon.km = rezervasyon.km + arac_fiyat.km_siniri_ucretsiz
WHERE rezervasyon.durum = 'Rezerve'";


            dbConnection.ExecuteNonQuery(query);
            return true;
         
        }
        public bool GunArtir()
        {


            string query = $@"UPDATE rezervasyon
SET rezervasyon.gun = rezervasyon.gun + 1
WHERE rezervasyon.durum = 'Rezerve'";


            dbConnection.ExecuteNonQuery(query);
            return true;

        }
        public DataTable YakitDurum(int vehicle_id)
        {
            string query = $"select yakit_durumu from vehicles where vehicle_id={vehicle_id}";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable YakitDurumGuncelle(int vehicle_id,string yakit_d)
        {
            string query = $"Update vehicles set yakit_durumu='{yakit_d}' where vehicle_id={vehicle_id}";
            return dbConnection.ExecuteQuery(query);
        }

        public DataTable AracListesi()
        {
            string query = "SELECT  vehicle_id FROM rezervasyon WHERE durum='Rezerve'";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable AracListesiSaatlik()
        {
            string query = "SELECT  vehicle_id FROM rezervasyon WHERE durum='Rezerve' AND tur='Saatlik'";
            return dbConnection.ExecuteQuery(query);
        }
        public DataTable AracListesiGunluk()
        {
            string query = "SELECT  vehicle_id FROM rezervasyon WHERE durum='Rezerve' AND tur= 'Gunluk' ";
            return dbConnection.ExecuteQuery(query);
        }
        public bool FiyatGuncelleKm(int vehicle_id)
        {
            string query = @"
    UPDATE payments 
    SET miktar = miktar + (
        SELECT 
            IF(r.km > af.km_siniri_ucretsiz, af.km_siniri_ucretsiz * af.ilave_km_ucreti, 0) 
        FROM 
            arac_fiyat af 
            INNER JOIN vehicles v ON v.fiyat_id = af.fiyat_id 
            INNER JOIN rezervasyon r ON r.vehicle_id = v.vehicle_id 
        WHERE 
            v.vehicle_id = " + vehicle_id + @" AND r.durum = 'Rezerve'
    ) 
    WHERE 
        rezervation_id IN (
            SELECT rezervation_id 
            FROM rezervasyon 
            WHERE vehicle_id = " + vehicle_id + @" AND durum = 'Rezerve'
        );";
            dbConnection.ExecuteNonQuery(query);


            return true;
        }
        public bool FiyatGuncelleSaat(int vehicleid)
        {
            string query = $"Update payments Set miktar=miktar+ " +
                $"(select arac_fiyat.fiyat_dakika from vehicles " +
                $"inner join arac_fiyat On arac_fiyat.fiyat_id=vehicles.vehicle_id" +
                $" where vehicles.vehicle_id={vehicleid}) " +
                $"where payments.rezervation_id IN (SELECT rezervation_id FROM rezervasyon WHERE vehicle_id = {vehicleid})";
                
             dbConnection.ExecuteNonQuery(query);
            return true;
        }
        public bool FiyatGuncelleGunluk(int vehicleid)
        {
            string query = $"Update payments Set miktar=miktar+ " +
                $"(select arac_fiyat.fiyat_gunluk from vehicles " +
                $"inner join arac_fiyat On arac_fiyat.fiyat_id=vehicles.vehicle_id" +
                $" where vehicles.vehicle_id={vehicleid}) " +
                $"where payments.rezervation_id IN (select rezervation_id from rezervasyon where vehicle_id={vehicleid})";

            dbConnection.ExecuteNonQuery(query);
            return true;
        }
        public bool YakitFiyatiEkle(int vehicle_id,decimal miktar)
        {
            string query = $"Update payments set miktar=miktar+{miktar} Where rezervation_id=(select rezervation_id from rezervasyon where vehicle_id={vehicle_id} AND durum='Rezerve')";

        dbConnection.ExecuteNonQuery(query);
            return true;
        }
    }
}
