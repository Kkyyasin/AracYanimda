using AracYanimdaApi.Data.Repository;
using AracYanimdaApi.Models.Kullanici;
using AracYanimdaApi.Models.Odeme;
using AracYanimdaApi.Models.Rezervasyon;
using MySqlX.XDevAPI.Relational;
using System.Data;
using System.Data.Common;

namespace AracYanimdaApi.Data.Service
{
    public class UserService
    {

        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public IEnumerable<User> GetAllUsers()
        {
            DataTable dataTable = _userRepository.GetAllUsers(); // Örnek bir metot

            List<User> users = new List<User>();
            foreach (DataRow row in dataTable.Rows)
            {
                var user = new User
                {
                    UserId = Convert.ToInt32(row["user_id"]),
                    Ad = row["ad"].ToString(),
                    Soyad = row["soyad"].ToString(),
                    EPosta = row["e_posta"].ToString(),
                    Sifre = row["sifre"].ToString(),
                    Telefon = row["telefon"].ToString(),
                    SozlesmeKabul = Convert.ToBoolean(row["sozlesme_kabul"]),
                    KayitTarihi = Convert.ToDateTime(row["kayit_tarihi"]),
                    Durum = row["durum"].ToString()
                };
                users.Add(user);
            }

            return users;
        }
        public User GetUserById(int id)
        {
            DataTable userData= _userRepository.GetUserById(id);
            if(userData != null && userData.Rows.Count > 0) {
                DataRow row = userData.Rows[0];
                var user = new User
                {
                    UserId = Convert.ToInt32(row["user_id"]),
                    Ad = row["ad"].ToString(),
                    Soyad = row["soyad"].ToString(),
                    EPosta = row["e_posta"].ToString(),
                    Sifre = row["sifre"].ToString(),
                    Telefon = row["telefon"].ToString(),
                    SozlesmeKabul = Convert.ToBoolean(row["sozlesme_kabul"]),
                    KayitTarihi = Convert.ToDateTime(row["kayit_tarihi"]),
                    Durum = row["durum"].ToString()
                };
                return user;
            }
            return null;
        }
        public User Authenticate(string eposta, string sifre)
        {
            DataTable userData = _userRepository.Authenticate(eposta, sifre);
            if (userData != null && userData.Rows.Count > 0)
            {
                // Kullanıcı bilgilerini al
                DataRow row = userData.Rows[0];

                // Burada User sınıfına dönüştürme işlemlerini yapın ve kullanıcıyı oluşturun
                var user = new User
                {
                    UserId = Convert.ToInt32(row["user_id"]),
                    Ad = row["ad"].ToString(),
                    Soyad = row["soyad"].ToString(),
                    EPosta = row["e_posta"].ToString(),
                    Sifre = row["sifre"].ToString(),
                    Telefon = row["telefon"].ToString(),
                    SozlesmeKabul = Convert.ToBoolean(row["sozlesme_kabul"]),
                    KayitTarihi = Convert.ToDateTime(row["kayit_tarihi"]),
                    Durum = row["durum"].ToString()
                };
                return user;
            }
            return null;
        }
        public bool Register(RegisterRequest registerRequest)
        {
            var ad = registerRequest.Ad;
            var soyad = registerRequest.Soyad;
            var Eposta = registerRequest.EPosta;
            var sifre = registerRequest.Sifre;
            var telefon = registerRequest.Telefon;
            var sozlesme_kabul = registerRequest.SozlesmeKabul;
            
            return _userRepository.Register(ad,soyad,Eposta,sifre,telefon,sozlesme_kabul);
        }
        public bool EmailKontrol(string eposta)
        {
            DataTable userData = _userRepository.EmailKontrol(eposta);
            if (userData != null && userData.Rows.Count > 0)
            { 
                return true;
            }
            return false;
        }
        public Licenses GetLicenseById(int id)
        {
           DataTable data= _userRepository.GetLicenseById(id);
            if(data!=null || data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                Licenses licenses=new Licenses()
                {
                    LicenseId = Convert.ToInt32(row["license_id"]),
                    EhliyetTipi = row["ehliyet_tipi"].ToString(),
                    EhliyetNo = row["ehliyet_no"].ToString(),
                    SonGecerlilikTarihi = row["son_gecerlilik_tarihi"].ToString()
                };
                return licenses;
            }
            return null;
        }
       
        public List<RezervasyonDurun> GetRezervasyonById(int id)
        {
            DataTable data=_userRepository.GetByRezervasyon(id);
            List<RezervasyonDurun> reservationDetailsList = new List<RezervasyonDurun>();
            if (data != null || data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows) // rows, tablodan gelen verilerin olduğu bir koleksiyon olmalı
                {
                    RezervasyonDurun reservationDetails = new RezervasyonDurun
                    {
                        AracIsmi = row["arac_ismi"]?.ToString(),
                        Plaka = row["plaka"]?.ToString(),
                        BaslangicTarihi = row["baslangic_tarihi"].ToString(),
                        BitisTarihi = row["bitis_tarihi"].ToString(),
                        Miktar = Convert.ToDecimal(row["miktar"]),
                        RezervasyonDurum = row["durum"].ToString()
                    };
                    reservationDetailsList.Add(reservationDetails);

                }
                    return reservationDetailsList;
            }

            return null;
           
        }
        public List<PaymentMethod> GetPaymentMethodsById(int id)
        {
            DataTable data = _userRepository.GetByPaymentMethodsById(id);
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            if (data != null || data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows) // rows, tablodan gelen verilerin olduğu bir koleksiyon olmalı
                {
                 

                    // Veritabanından gelen veriyi PaymentMethod sınıfına ekleme
                    PaymentMethod paymentMethod = new PaymentMethod()
                    {
                        PaymentMethodId = Convert.ToInt32(row["payment_method_id"]),
                        UserId = Convert.ToInt32(row["user_id"]),
                        AdSoyad = row["ad_soyad"].ToString(),
                        KartNumarasi = row["kart_numarasi"].ToString(),
                    };

                   
                    // paymentMethod nesnesini listeye ekleme
                    paymentMethods.Add(paymentMethod);

                }
                return paymentMethods;
            }

            return null;
        }
        public List<Payment> GetPaymentById(int id)
        {
            DataTable data = _userRepository.GetByPayments(id);
            List<Payment> payments = new List<Payment>();
            if (data != null || data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows) // rows, tablodan gelen verilerin olduğu bir koleksiyon olmalı
                {
                    Payment payment = new Payment();

                  
                        payment.Miktar = Convert.ToDecimal(row["miktar"]);
                    if (row["odeme_tarihi"] != DBNull.Value)
                  
                        payment.OdemeTarihi = Convert.ToDateTime(row["odeme_tarihi"]);

                    payment.OdemeDurumu = row["odeme_durumu"].ToString();
                        
                   
                    payments.Add(payment);

                }
                return payments;
            }

            return null;
        }
        public bool UpdateEmail(int id, string email)
        {
            return _userRepository.UpdateEmail(id, email);
        }
        public bool UpdateTelefon(int id, string tel)
        {
            return _userRepository.UpdatePhone(id, tel);
        }
        public bool UpdateDurum(int id, string durum)
        {
            return _userRepository.UpdateDurum(id, durum);
        }
        public bool KullaniciSil(int id)
        {
            return _userRepository.KullaniciSil(id);
        }
        public int RezervasyonVarmi(int user_id)
        {
            var kontrol =Convert.ToInt32( _userRepository.RezervasyonVarmi(user_id).Rows[0][0]);
            if (kontrol != 0)
            {
                return kontrol;
            }
            return 0;

        }
        public bool EhliyetEkle(Licenses license)
        {
            var ehliyet_tipi = license.EhliyetTipi;
            var ehliyet_no = license.EhliyetNo;
            var son_gecerlilik = license.SonGecerlilikTarihi;
            var user_id = license.UserId;
            return _userRepository.EhliyetEkle(ehliyet_tipi, ehliyet_no, son_gecerlilik,user_id);
        }
        public Licenses EhliyetBilgileri(int id)
        {
            DataTable data = _userRepository.EhliyetBilgileri(id);
            List<Licenses > list = new List<Licenses>();
            if (data != null || data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                Licenses licenses = new Licenses()
                {
                    LicenseId = Convert.ToInt32(row["license_id"]),
                    EhliyetTipi = row["ehliyet_tipi"].ToString(),
                    EhliyetNo = row["ehliyet_no"].ToString(),
                    SonGecerlilikTarihi = row["son_gecerlilik_tarihi"].ToString()
                };
                return licenses;
            }
            return null;
        }
    }

};
