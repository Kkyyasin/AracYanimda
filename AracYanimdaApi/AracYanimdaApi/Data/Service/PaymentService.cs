using AracYanimdaApi.Data.Repository;
using AracYanimdaApi.Models.Odeme;
using System.Data;
using System.Data.Common;

namespace AracYanimdaApi.Data.Service
{
    public class PaymentService
    {
        private readonly PaymentRepository _paymentRepository;

        public PaymentService()
        {
            _paymentRepository = new PaymentRepository();
        }
        public List<Payment> GetPayments()
        {
            DataTable data = _paymentRepository.GetPayments();  // Assumed method to fetch payment data
            List<Payment> list = new List<Payment>();
            if (data != null && data.Rows.Count > 0)  // Changed || to &&
            {
                foreach (DataRow row in data.Rows)
                {
                    Payment payment = new Payment()
                    {
                        PaymentId = row["PaymentId"] != DBNull.Value ? Convert.ToInt32(row["PaymentId"]) : 0,
                        RezervationId = row["RezervationId"] != DBNull.Value ? Convert.ToInt32(row["RezervationId"]) : 0,
                        PaymentMethodId = row["PaymentMethodId"] != DBNull.Value ? Convert.ToInt32(row["PaymentMethodId"]) : 0,
                        Miktar = row["Miktar"] != DBNull.Value ? Convert.ToDecimal(row["Miktar"]) : 0,
                        OdemeTarihi = row["OdemeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["OdemeTarihi"]) : DateTime.MinValue, // or some other default date
                        OdemeDurumu = row["OdemeDurumu"] != DBNull.Value ? row["OdemeDurumu"].ToString() : string.Empty
                    };
                    list.Add(payment);
                }
                return list;
            }
            return null;
        }

        public bool CreatePayment(int rezervation_id, int payment_method_id)
        {
         return _paymentRepository.CreatePayment(rezervation_id, payment_method_id);
        }
        public Bilgi GetBilgi(int rezervation_id)
        {
            DataTable data=_paymentRepository.GetBilgi(rezervation_id);
            if (data == null || data.Rows.Count>0)
            {
                DataRow row = data.Rows[0];
                Bilgi bilgi = new Bilgi()
                {
                    km = Convert.ToInt32(row["km"]),
                    miktar = Convert.ToDecimal(row["miktar"])
                };
                return bilgi;
            }
            return null;
        }
        public bool OdemeTamamla(int rezervation_id)
        {
            return _paymentRepository.OdemeTamamla(rezervation_id);
        }
        public bool CreatePaymentMethod(PaymentMethod payment_method)
        {
            return _paymentRepository.CreatePaymentMethod(payment_method.UserId, payment_method.AdSoyad, payment_method.KartNumarasi,payment_method.Cvv,payment_method.SonKullanma);
        }
        public List<PaymentMethod> GetPaymentMethods(int user_id)
        {
            DataTable data=_paymentRepository.GetPaymentMethods(user_id);
            List<PaymentMethod> list=new List<PaymentMethod>();
            if(data!=null || data.Rows.Count > 0)
            {
                foreach(DataRow row in data.Rows)
                {
                    PaymentMethod paymentMethod=new PaymentMethod()
                    {
                        PaymentMethodId = Convert.ToInt32(row["payment_method_id"]),
                        UserId = Convert.ToInt32(row["user_id"]),
                        AdSoyad = row["ad_soyad"].ToString(),
                        KartNumarasi = row["kart_numarasi"].ToString(),
                        Cvv = row["cvv"].ToString(),

                        SonKullanma = row["son_kullanma"].ToString()
                    };
                    list.Add(paymentMethod);
                }
                return list;
            }
            return null;
        }
        public Fatura GetFatura(int payment_id)
        {
            DataTable data=_paymentRepository.Fatura(payment_id);   
            if(data!=null || data.Rows.Count>0) {
            DataRow row = data.Rows[0];
                Fatura fatura = new Fatura()
                {
                    PaymentId = Convert.ToInt32(row["payment_id"]),
                    KullaniciIsmı = row["ad"].ToString() + " " + row["soyad"].ToString(),
                    AracIsmi = row["marka"].ToString() + " " + row["model"].ToString(),
                    BaslangicTarihi = row["baslangic_tarihi"].ToString(),
                    BitisTarihi = row["bitis_tarihi"].ToString(),
                    Miktar = Convert.ToDecimal(row["miktar"]),
                    Tur = row["tur"].ToString(),
                    AdSoyad = row["ad_soyad"].ToString(),
                    KartNumarasi = row["kart_numarasi"].ToString()
                };
                return fatura;   
                }
            return null;
        }
        

    }
}
