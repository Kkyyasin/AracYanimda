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
    public class UserApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://aracimyanimda.azurewebsites.net"); // API'nin URL'si
            // Gerekirse, ek ayarlamalar yapabilirsiniz
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer YourAccessToken");
        }

        public async Task<List<UserListViewModel>> GetUserListJson()
        {
            var response = await _httpClient.GetAsync("/api/User"); // API'nin istenen endpoint'i
            if (response.IsSuccessStatusCode)
            {
                // Yanıt içeriğini okuyun
                string content = await response.Content.ReadAsStringAsync();

                // JSON'i deserialize edin
                List<UserListViewModel> userList = JsonConvert.DeserializeObject<List<UserListViewModel>>(content);

                // Listeyi kullanın veya View'e gönderin
                // ...

                return userList;
            }
            else
            {
                // Hata durumunu işleyin
                return null;
            }
        }
        public async Task<UserListViewModel> GetByUserId(int id)
        {
            try
            {
                string apiUrl = $"api/User/users/user?Id={id}";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    UserListViewModel user = JsonConvert.DeserializeObject<UserListViewModel>(content);
                    return user;
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
        public async Task<Licenses> GetByLicenseId(int id)
        {
            try
            {
                string apiUrl = $"api/User/users/license?Id={id}";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    Licenses user = JsonConvert.DeserializeObject<Licenses>(content);
                    return user;
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
        public async Task<List<RezervasyonDetay>> GetByRezervasyonId(int id)
        {
            try
            {
                string apiUrl = $"api/User/users/rezervasyon?Id={id}";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    List<RezervasyonDetay> list = JsonConvert.DeserializeObject<List<RezervasyonDetay>>(content);
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
        public async Task<List<PaymentMethod>> GetByPaymentMethodsId(int id)
        {
            try
            {
                string apiUrl = $"api/User/users/paymentmethods?Id={id}";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    List<PaymentMethod> list = JsonConvert.DeserializeObject<List<PaymentMethod>>(content);
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
        public async Task<List<Payment>> GetByPaymentsId(int id)
        {
            try
            {
                string apiUrl = $"api/User/users/payments?Id={id}";

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
        public async Task<string> UpdateEmail(int id, string email)
        {

            // Verileri JSON formatına dönüştür
            var requestData = new
            {
                Id = id,
                email = email
            };
            var jsonData = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API'ye istek gönder
            string apiUrl = $"api/User/users/email/update?Id={id}&email={email}";
            var response = await _httpClient.PostAsync(apiUrl, content);


            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            return null;
        }
        public async Task<string> UpdateTelefon(int id, string telefon)
        {

            // Verileri JSON formatına dönüştür
            var requestData = new
            {
                Id = id,
                tel = telefon
            };
            var jsonData = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API'ye istek gönder
            string apiUrl = $"api/User/users/phone/update?Id={id}&tel={telefon}";
            var response = await _httpClient.PostAsync(apiUrl, content);


            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            return null;
        }
        public async Task<string> UpdateDurum(int id, string durum)
        {

            // Verileri JSON formatına dönüştür
            var requestData = new
            {
                Id = id,
                durum = durum
            };
            var jsonData = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API'ye istek gönder
            string apiUrl = $"api/User/users/durum/update?Id={id}&durum={durum}";
            var response = await _httpClient.PostAsync(apiUrl, content);


            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            return null;
        }
        public async Task<string> KullaniciSil(int Id)
        {
            var json = JsonConvert.SerializeObject(Id);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/User/users/user/delete", data);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            return null;
        }
    }
}