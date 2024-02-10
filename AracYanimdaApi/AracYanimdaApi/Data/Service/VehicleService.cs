using AracYanimdaApi.Data.IRepository;
using AracYanimdaApi.Data.Repository;
using AracYanimdaApi.Models.Arac;
using AracYanimdaApi.Models.Kullanici;
using System.Data;
using System.Data.Common;

namespace AracYanimdaApi.Data.Service
{
    public class VehicleService
    {
        private readonly VehicleRepository _vehiclerepository;
        public VehicleService()
        {
            _vehiclerepository = new VehicleRepository();
        }
        public IEnumerable<Arac> GetAllVehicles()
        {

            DataTable vehicledata = _vehiclerepository.GetAllVehicles(); // Örnek bir metot
            List<Arac> vehicles = new List<Arac>();
            if (vehicledata != null && vehicledata.Rows.Count > 0) {

                foreach (DataRow row in vehicledata.Rows)
                {
                    var vehicle = new Arac
                    {
                        VehicleId = Convert.ToInt32(row["VehicleId"]),
                        Marka = row["Marka"].ToString(),
                        Model = row["Model"].ToString(),
                        Yil = Convert.ToInt32(row["Yil"]),
                        Plaka = row["Plaka"].ToString(),
                        YakitTipi = row["YakitTipi"].ToString(),
                        YakitDurumu = row["YakitDurumu"].ToString(),
                        LocationId = Convert.ToInt32(row["LocationId"]),
                        FiyatId = Convert.ToInt32(row["FiyatId"])
                    };
                    vehicles.Add(vehicle);
                }
            return vehicles;
            }
            return null;
        }
        public bool Create(Arac arac)
        {
            var marka=arac.Marka;
            var model=arac.Model;
            var yil=arac.Yil;
            var plaka=arac.Plaka;
            var yakit_tipi=arac.YakitTipi;
            var yakit_durumu=arac.YakitDurumu;
            var location_id=arac.LocationId;
            var fiyat_id=arac.FiyatId;
            return _vehiclerepository.Create(marka,model,yil,plaka,yakit_tipi,yakit_durumu,location_id,fiyat_id);
        }
        public IEnumerable<AracKonum> GetAracKonum()
        {
            DataTable vehicledata = _vehiclerepository.GetAracKonum();
            List<AracKonum> konumlar= new List<AracKonum>();
            if (vehicledata != null && vehicledata.Rows.Count > 0)
            {

                foreach (DataRow row in vehicledata.Rows)
                {
                    var vehicle = new AracKonum
                    {
                        VehicleId = Convert.ToInt32(row["vehicle_id"]),
                        Marka = row["marka"].ToString(),
                        Model = row["model"].ToString(),
                        Latitude = Convert.ToDecimal(row["latitude"]),
                        Longitude = Convert.ToDecimal(row["longitude"])
                    };
                    konumlar.Add(vehicle);
                }
                return konumlar;
            }
            return null;
        }
        public int CreateLocation(Location location)
        {
            var latitude = location.Latitude;
            var longitude=location.Longitude;
            var adress = location.Address;
            var city=location.City;

            DataTable data=_vehiclerepository.CreateLocation(latitude, longitude, adress, city);    
            if(data.Rows.Count > 0) {
                return Convert.ToInt32(data.Rows[0][0]);
            }
            return 0;
        }
        public int CreateAracFiyat(AracFiyat aracFiyat)
        {
            var fiyat_g = aracFiyat.FiyatGunluk;
            var fiyat_d = aracFiyat.FiyatDakika;
            var km_s = aracFiyat.KmSiniriUcretsiz;
            var ilave = aracFiyat.IlaveKmUcreti;
            DataTable data =_vehiclerepository.CreateAracFiyat(fiyat_g,fiyat_d, km_s,ilave);
            if(data.Rows.Count > 0)
                return Convert.ToInt32(data.Rows[0][0]);
            return 0;
        }
       // public int CreateAracFiyat
       public List<String> AracMarka()
        {
            var list = new List<String>();
              DataTable data=_vehiclerepository.OtomatikAracMarka();
            foreach (DataRow row in data.Rows) {
                list.Add(row["marka"].ToString());
            }
            return list;
        }
        public List<String> AracModel(string marka)
        {
            var list = new List<String>();
            DataTable data = _vehiclerepository.OtomatikAracModel(marka);
            foreach (DataRow row in data.Rows)
            {
                list.Add(row["model"].ToString());
            }
            return list;
        }
        public List<String> AracYakit(string marka,string model)
        {
            var list = new List<String>();
            DataTable data = _vehiclerepository.OtomatikAracYakit(marka,model);
            foreach (DataRow row in data.Rows)
            {
                list.Add(row["yakit"].ToString());
            }
            return list;
        }
        public int AracFiyat(string marka, string model,string yakit)
        {
            DataTable data = _vehiclerepository.OtomatikAracFiyat(marka, model,yakit);
            return Convert.ToInt32(data.Rows[0][0]);
        }

        public List<AracAll> GetAracAll()
        {
            DataTable data=_vehiclerepository.GetAracAll();
            List<AracAll> vehicles = new List<AracAll>();
            if (data != null && data.Rows.Count > 0)
            {

                foreach (DataRow row in data.Rows)
                {
                    var vehicle = new AracAll
                    {
                        VehicleId = Convert.ToInt32(row["vehicle_id"]),
                        Marka = row["marka"].ToString(),
                        Model = row["model"].ToString(),
                        Yil = Convert.ToInt32(row["yil"]),
                        Plaka = row["plaka"].ToString(),
                        YakitTipi = row["yakit_tipi"].ToString(),
                        YakitDurumu = row["yakit_durumu"].ToString(),
                        Latitude = Convert.ToDecimal(row["latitude"]),
                        Longitude = Convert.ToDecimal(row["longitude"]),
                        Address = row["address"].ToString(),
                        City = row["city"].ToString(),
                        FiyatGunluk = Convert.ToDecimal(row["fiyat_gunluk"]),
                        FiyatDakika = Convert.ToDecimal(row["fiyat_dakika"]),
                        KmSiniriUcretsiz = Convert.ToInt32(row["km_siniri_ucretsiz"]),
                        IlaveKmUcreti = Convert.ToInt32(row["ilave_km_ucreti"]),
                        Durum = row["durum"].ToString()
                     
                    };
                    vehicles.Add(vehicle);
                }

                return vehicles;
            }
            return null;
        }
        public Arac GetByVehicleId(int id)
        {
            DataTable data = _vehiclerepository.GetByVehicleId(id);
            Arac vehicle;


            if (data != null && data.Rows.Count > 0)
            {   

                DataRow row = data.Rows[0];
                 vehicle = new Arac()
                    {
                    VehicleId = Convert.ToInt32(row["vehicle_id"]),
                    Marka = row["marka"].ToString(),
                    Model = row["model"].ToString(),
                    Yil = Convert.ToInt32(row["yil"]),
                    Plaka = row["plaka"].ToString(),
                    YakitTipi = row["yakit_tipi"].ToString(),
                    YakitDurumu = row["yakit_durumu"].ToString(),
                    LocationId = Convert.ToInt32(row["location_id"]),
                    FiyatId = Convert.ToInt32(row["fiyat_id"])
                };
                return vehicle;
            }

            return null;
        }
        public AracFiyat GetByFiyatId(int id)
        {
            DataTable data = _vehiclerepository.GetByFiyatId(id);

            if (data != null && data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                AracFiyat vehicle = new AracFiyat()
                {
                    FiyatId = Convert.ToInt32(row["fiyat_id"]),
                    FiyatGunluk = Convert.ToDecimal(row["fiyat_gunluk"]),
                    FiyatDakika = Convert.ToDecimal(row["fiyat_dakika"]),
                    KmSiniriUcretsiz = Convert.ToInt32(row["km_siniri_ucretsiz"]),
                    IlaveKmUcreti = Convert.ToInt32(row["ilave_km_ucreti"])
                };
                return vehicle;
            }

            return null;
        }
        public Location GetByLocationId(int id)
        {
            DataTable data = _vehiclerepository.GetByLocationId(id);

            if (data != null && data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                Location konum = new Location()
                {
                    LocationId = Convert.ToInt32(row["location_id"]),
                    Latitude = Convert.ToDecimal(row["latitude"]),
                    Longitude = Convert.ToDecimal(row["longitude"]),
                    Address = row["address"].ToString(),
                    City = row["city"].ToString()
                };
                   
                return konum;
            }

            return null;
        }
        public List<Bakim> GetByBakımId(int id)
        {
            DataTable data = _vehiclerepository.GetByBakımId(id);
            List<Bakim> list = new List<Bakim>();
            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    Bakim bakim = new Bakim()
                    {
                        BakimId = Convert.ToInt32(row["bakim_id"]),
                        BakimAciklamasi = row["bakim_aciklamasi"].ToString(),
                        BakimTarihi = Convert.ToDateTime(row["bakim_tarihi"])
                    };
                    list.Add(bakim);
                }
                return list;
            }

            return null;
        }
        public bool FiyatGuncelle(decimal gunlukFiyat, decimal dakikaFiyat, int id)
        {
            if(gunlukFiyat>1000 && gunlukFiyat<0)
                return false;
            if (dakikaFiyat > 100 && dakikaFiyat < 0)
                return false;
            return _vehiclerepository.FiyatGuncelle(gunlukFiyat, dakikaFiyat, id);
        }
        public bool BakimEkle(int VehicleId, string bakimAciklamasi, DateTime bakimTarihi)
        {
            var BakımTarihi = bakimTarihi.ToString("yyyy-MM-dd HH:mm:ss");
            return _vehiclerepository.BakimEkle(VehicleId,bakimAciklamasi, BakımTarihi);
        }
        public bool BakimSil(int bakimId)
        {
            return _vehiclerepository.BakimSil(bakimId);
        }
        public bool AracSil(int vehicleId)
        {
            return _vehiclerepository.AracSil(vehicleId);
        }

    }
}
