using AracYanimdaApi.Data.Repository;
using System.Data;

namespace AracYanimdaApi.Data.Service
{
    public class TimerService
    {
        private readonly TimerRepository _timerRepository ;
        public TimerService()
        {
            _timerRepository = new TimerRepository();
        }
        public List<int> AracListesi()
        {
            DataTable data = _timerRepository.AracListesi();
            List<int> list = new List<int>();
            if (data != null || data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    list.Add(Convert.ToInt32(row["vehicle_id"]));
                }
                return list;
            }
            return null;
        }
        public List<int> AracListesiSaatlik()
        {
            DataTable data = _timerRepository.AracListesiSaatlik();
            List<int> list = new List<int>();
            if (data != null || data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    list.Add(Convert.ToInt32(row["vehicle_id"]));
                }
                return list;
            }
            return null;
        }
        public List<int> AracListesiGunluk()
        {
            DataTable data = _timerRepository.AracListesiGunluk();
            List<int> list = new List<int>();
            if (data != null || data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    list.Add(Convert.ToInt32(row["vehicle_id"]));
                }
                return list;
            }
            return null;
        }
        public bool KmArtir()
        {
            return _timerRepository.KmArtir();
        }
        public bool GunArtir()
        {
            return _timerRepository.GunArtir();
        }
        public bool YakitGuncelle()
        {
            List<int> araclistesi=AracListesi();
            string yeni_durum;
            foreach(int arac in araclistesi)
            {
                string yakit_d = _timerRepository.YakitDurum(arac).Rows[0][0].ToString();
                if (yakit_d.Equals("Full"))
                    yeni_durum = "Yuzde 75 dolu";
                else if (yakit_d.Equals("Yuzde 75 dolu"))
                    yeni_durum = "Yarım Dolu";
                else if (yakit_d.Equals("Yarım Dolu"))
                    yeni_durum = "Çeyrek Dolu";
                else if (yakit_d.Equals("Çeyrek Dolu"))
                {
                    _timerRepository.YakitFiyatiEkle(arac, 250);
                    yeni_durum = "Full";
                }
                else
                {
                    yeni_durum = "Full";
                }
                _timerRepository.YakitDurumGuncelle(arac, yeni_durum);
            }
            return true;
        }
        public bool FiyatGuncellemeKm()
        {
            List<int> arac_listesi=AracListesi();
            if(arac_listesi==null)
                return false;
            foreach(int arac  in arac_listesi)
            {
               

                _timerRepository.FiyatGuncelleKm(arac);
            }
            return true;
        }
        public bool FiyatGuncellemeSaat()
        {
            List<int> rezervasyon_listesi = AracListesiSaatlik();
            if (rezervasyon_listesi == null)
                return false;
            foreach (int rezervasyon in rezervasyon_listesi)
            {     
                _timerRepository.FiyatGuncelleSaat(rezervasyon);
            }
            return true;
        }
        public bool FiyatGuncellemeGunluk()
        {
            List<int> rezervasyon_listesi = AracListesiGunluk();
            if (rezervasyon_listesi == null)
                return false;
            foreach (int rezervasyon in rezervasyon_listesi)
            {
                _timerRepository.FiyatGuncelleKm(rezervasyon);
            }
            return true;
        }
        public string RunSaat()
        {
            string message = "";
            if (KmArtir())
                message += "Km Arttırıldı...\n";
            if (YakitGuncelle())
                message += "Yakıt güncellendi...\n";
           if (FiyatGuncellemeKm())
                message += "Fiyat Km başına göre eklendi...\n";
            if (FiyatGuncellemeSaat())
                message += "Saatlik Ucret Alındı...\n";
            return message;
        }
        public string RunGunluk()
        {
            string message = "";
            
            if (FiyatGuncellemeGunluk())
                message += "Gunluk Ucret Alındı...\n";
            if (GunArtir())
                message += "Gunluk artirildi...\n";
            return message;
        }
    }
}
