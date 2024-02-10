using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AracYanimdaWeb.Models;
using System.Text;
using System.IO.Compression;
namespace AracYanimdaWeb.Service
{
    public class RezervasyonApiService
    {
        private readonly HttpClient _httpClient;

        public RezervasyonApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://aracimyanimda.azurewebsites.net"); // API'nin URL'si
            // Gerekirse, ek ayarlamalar yapabilirsiniz
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer YourAccessToken");
        }
        public async Task<List<Rezervasyon>> GetRezervasyonAll()
        {
            var response = await _httpClient.GetAsync("/api/Rezervasyon"); // API'nin istenen endpoint'i
            if (response.IsSuccessStatusCode)
            {
                // Yanıt içeriğini okuyun
                string content = await response.Content.ReadAsStringAsync();

                // JSON'i deserialize edin
                List<Rezervasyon> list = JsonConvert.DeserializeObject<List<Rezervasyon>>(content);

                // Listeyi kullanın veya View'e gönderin
                // ...

                return list;
            }
            else
            {
                // Hata durumunu işleyin
                return null;
            }
        }
        public async Task<Rezervasyon> GetByRezervasyonId(int id)
        {
            try
            {
                string apiUrl = $"api/Rezervasyon/rezervasyon/?rezervasyon_id={id}";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    Rezervasyon rezervasyon = JsonConvert.DeserializeObject<Rezervasyon>(content);
                    return rezervasyon;
                }
                else
                {

                    return null;
                }
            }
            catch (Exception ex)
            {
                // Bir hata oluştuysa burada işleyebilir veya null döndürebilirsiniz
                Console.WriteLine("Hata oluştu: " + ex.Message);
                return null;
            }
        }
        public async Task<UserListViewModel> GetByRezervasyonUserId(int id)
        {
            try
            {
                string apiUrl = $"api/Rezervasyon/rezervasyon/user?rezervasyon_id={id}";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    UserListViewModel list = JsonConvert.DeserializeObject<UserListViewModel>(content);
                    return list;
                }
                else
                {

                    return null;
                }
            }
            catch (Exception ex)
            {
                // Bir hata oluştuysa burada işleyebilir veya null döndürebilirsiniz
                Console.WriteLine("Hata oluştu: " + ex.Message);
                return null;
            }
        }
        public async Task<VehicleListViewModel> GetByRezervasyonVehicleId(int id)
        {
            try
            {
                string apiUrl = $"api/Rezervasyon/rezervasyon/vehicle?rezervasyon_id={id}";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    VehicleListViewModel vehicle = JsonConvert.DeserializeObject<VehicleListViewModel>(content);
                    return vehicle;
                }
                else
                {

                    return null;
                }
            }
            catch (Exception ex)
            {
                // Bir hata oluştuysa burada işleyebilir veya null döndürebilirsiniz
                Console.WriteLine("Hata oluştu: " + ex.Message);
                return null;
            }
        }
        public async Task<Payment> GetByRezervasyonPaymentId(int id)
        {
            try
            {
                string apiUrl = $"api/Rezervasyon/rezervasyon/payment?rezervasyon_id={id}";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    Payment payment = JsonConvert.DeserializeObject<Payment>(content);
                    return payment;
                }
                else
                {

                    return null;
                }
            }
            catch (Exception ex)
            {
                // Bir hata oluştuysa burada işleyebilir veya null döndürebilirsiniz
                Console.WriteLine("Hata oluştu: " + ex.Message);
                return null;
            }
        }
        public async Task<List<Payment>> GetPayments()
        {
            try
            {
                string apiUrl = $"api/Payment";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    List<Payment> list = JsonConvert.DeserializeObject<List<Payment>>(content);
                    return list;
                }
                else
                {

                    return null;
                }
            }
            catch (Exception ex)
            {
                // Bir hata oluştuysa burada işleyebilir veya null döndürebilirsiniz
                Console.WriteLine("Hata oluştu: " + ex.Message);
                return null;
            }
        }
        public async Task<Fatura> GetFatura(int payment_id)
        {
            try
            {
                string apiUrl = $"api/Payment/fatura?payment_id=" + payment_id;

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    Fatura fatura = JsonConvert.DeserializeObject<Fatura>(content);
                    return fatura;
                }
                else
                {

                    return null;
                }
            }
            catch (Exception ex)
            {
                // Bir hata oluştuysa burada işleyebilir veya null döndürebilirsiniz
                Console.WriteLine("Hata oluştu: " + ex.Message);
                return null;
            }
        }
    }
}