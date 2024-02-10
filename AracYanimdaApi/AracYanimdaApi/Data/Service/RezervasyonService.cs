using AracYanimdaApi.Data.Repository;
using AracYanimdaApi.Models.Arac;
using AracYanimdaApi.Models.Kullanici;
using AracYanimdaApi.Models.Odeme;
using AracYanimdaApi.Models.Rezervasyon;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace AracYanimdaApi.Data.Service
{
    public class RezervasyonService
    {
        private readonly RezervasyonRepository _rezervasyonRepository;
        private readonly PaymentService paymentService;

        public RezervasyonService()
        {
            _rezervasyonRepository = new RezervasyonRepository();
            paymentService = new PaymentService();
        }
        public int CreateRezervasyon(Rezervasyon rezervasyon,int payment_method_id)
        {
            var vehicle_id = rezervasyon.VehicleId;
            var user_id= rezervasyon.UserId;
            if (Convert.ToInt32(_rezervasyonRepository.Kontrol(vehicle_id, user_id).Rows[0][0])==0)
            {
                var formattedDate = rezervasyon.BitisTarihi;
                var formatteddata2 = rezervasyon.BaslangicTarihi;
                if (string.IsNullOrEmpty(formattedDate) || formattedDate == null)
                {
                    int rezervasyonid = Convert.ToInt32(_rezervasyonRepository.CreateRezervasyon(vehicle_id, user_id, formatteddata2).Rows[0][0]);
                    _rezervasyonRepository.DurumGuncelle("Rezerve", vehicle_id, user_id);
                    paymentService.CreatePayment(rezervasyonid, payment_method_id);
                    return rezervasyonid;
                }
                else
                {
                    int rezervasyonid = Convert.ToInt32(_rezervasyonRepository.CreateRezervasyon(vehicle_id, user_id, formatteddata2, formattedDate).Rows[0][0]);
                    _rezervasyonRepository.DurumGuncelle("Rezerve", vehicle_id, user_id);
                    paymentService.CreatePayment(rezervasyonid, payment_method_id);
                    return rezervasyonid;
                }
            }
            return 320;
        }
        public List<RezervasyonAll> GetRezervasyonAll()
        {
            DataTable data = _rezervasyonRepository.GetRezervasyonAll(); // Make sure this queries the view
            List<RezervasyonAll> list = new List<RezervasyonAll>();
            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    RezervasyonAll rezervasyon = new RezervasyonAll
                    {
                        RezervationId = Convert.ToInt32(row["RezervationId"]),
                        KullaniciIsmı = row["KullaniciIsmi"].ToString(), // Adjusted to match view column
                        AracIsmi = row["AracIsmi"].ToString(), // Adjusted to match view column
                        BaslangicTarihi = row["BaslangicTarihi"].ToString(),
                        BitisTarihi = row["BitisTarihi"].ToString(),
                        Durum = row["Durum"].ToString(),
                        Miktar = Convert.ToDecimal(row["Miktar"]),
                        OdemeDurumu = row["OdemeDurumu"].ToString(),
                        Tur = row["Tur"].ToString()
                    };
                    list.Add(rezervasyon);
                }
                return list;
            }
            return null;
        }

        public Rezervasyon GetByRezervasyonId(int rezervasyon_id)
        {
            DataTable data = _rezervasyonRepository.GetByRezervasyonId(rezervasyon_id);
            if (data != null || data.Rows.Count > 0) {
                DataRow row= data.Rows[0];
                Rezervasyon rezervasyon = new Rezervasyon
                {
                    RezervationId = Convert.ToInt32(row["rezervation_id"]),
                    BaslangicTarihi =row["baslangic_tarihi"].ToString(),
                    BitisTarihi = row["bitis_tarihi"].ToString(),
                    Durum = row["durum"].ToString(),
                    Tur = row["tur"].ToString()
                };
                return rezervasyon;
         }
            return null;
        }
        public User GetByUserRezervasyonId(int rezervasyon_id)
        {
            DataTable data = _rezervasyonRepository.GetByUserRezervasyonId(rezervasyon_id);
            if (data != null || data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                User user = new User
                {
                    UserId = Convert.ToInt32(row["user_id"]),
                    Ad = row["ad"].ToString(),
                    Soyad = row["soyad"].ToString(),
                    EPosta = row["e_posta"].ToString(),
                    
                    Telefon = row["telefon"].ToString(),
                    SozlesmeKabul = Convert.ToBoolean(row["sozlesme_kabul"]),
                    KayitTarihi = Convert.ToDateTime(row["kayit_tarihi"]),
                    Durum = row["durum"].ToString()
                };
                return user;
            }
            return null;
            }
        public Arac GetByVehiclesId(int rezervasyon_id)
        {
            DataTable data = _rezervasyonRepository.GetByVehiclesId(rezervasyon_id);
            if (data != null || data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                Arac arac=new Arac{
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
                return arac;
            }
            return null;
        }
        public Payment GetByPaymentsRezervasyonId(int rezervasyon_id)
        {
            DataTable data = _rezervasyonRepository.GetByPaymentsRezervasyonId(rezervasyon_id);
            if (data != null || data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                Payment payment = new Payment
                {
                     Miktar = Convert.ToDecimal(row["miktar"]),
                    OdemeDurumu = row["odeme_durumu"].ToString(),
                    OdemeTarihi = row["odeme_tarihi"] != DBNull.Value ? Convert.ToDateTime(row["odeme_tarihi"]) : default(DateTime),
                };
                return payment;
            }
            return null;
        }


    }
}
