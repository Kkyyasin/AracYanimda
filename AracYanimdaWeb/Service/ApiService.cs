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
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://aracimyanimda.azurewebsites.net"); // API'nin URL'si
            // Gerekirse, ek ayarlamalar yapabilirsiniz
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer YourAccessToken");
        }


        public async Task<List<VehicleListViewModel>> GetVehicleListJson()
        {
            var response = await _httpClient.GetAsync("/api/Vehicle"); // API'nin istenen endpoint'i
            if (response.IsSuccessStatusCode)
            {

                // Yanıt içeriğini okuyun
                string content = await response.Content.ReadAsStringAsync();

                // JSON'i deserialize edin
                List<VehicleListViewModel> vehiclelist = JsonConvert.DeserializeObject<List<VehicleListViewModel>>(content);

                // Listeyi kullanın veya View'e gönderin
                // ...

                return vehiclelist;
            }
            else
            {
                return null;
            }
        }
        public async Task<int> CreateLocation(LocationListViewModel location)
        {
            var json = JsonConvert.SerializeObject(location);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/vehicle/konum/create", data);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return Convert.ToInt32(responseContent);
            }

            return 0;
        }

        public async Task<int> CreateAracFiyat(AracFiyat aracFiyat)
        {
            var json = JsonConvert.SerializeObject(aracFiyat);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/vehicle/fiyat/create", data);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return Convert.ToInt32(responseContent);
            }

            return 0;
        }
        public async Task<string> CreateVehicle(VehicleListViewModel arac)
        {
            var json = JsonConvert.SerializeObject(arac);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/vehicle/create", data);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            return null;
        }


        public async Task<int> GetFiyat(string marka, string model, string yakit)
        {
            try
            {
                // İstek için URL oluşturma
                string apiUrl = $"/api/Vehicle/fiyat?marka={marka}&model={model}&yakit={yakit}";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();

                    // JSON'i deserialize edin
                    string responseContent = await response.Content.ReadAsStringAsync();

                    return Convert.ToInt32(responseContent);
                }
                else
                {
                    // Yanıt başarısız olduysa null döndürün veya hata işleyin
                    Console.WriteLine("API'den veri alınamadı. Hata kodu: " + response.StatusCode);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                // Bir hata oluştuysa burada işleyebilir veya null döndürebilirsiniz
                Console.WriteLine("Hata oluştu: " + ex.Message);
                return 0;
            }
        }

        public async Task<VehicleListViewModel> GetByVehicle(int id)
        {
            try
            {

                string apiUrl = $"/api/Vehicle/vehicle/index?id={id}";

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
                    // Yanıt başarısız olduysa null döndürün veya hata işleyin
                    Console.WriteLine("API'den veri alınamadı. Hata kodu: " + response.StatusCode);
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
        public async Task<AracFiyat> GetByFiyat(int id)
        {
            try
            {
                string apiUrl = $"/api/Vehicle/vehicle/fiyat?id={id}";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    AracFiyat fiyat = JsonConvert.DeserializeObject<AracFiyat>(content);
                    return fiyat;
                }
                else
                {
                    // Yanıt başarısız olduysa null döndürün veya hata işleyin
                    Console.WriteLine("API'den veri alınamadı. Hata kodu: " + response.StatusCode);
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
        public async Task<List<Bakim>> GetByBakim(int id)
        {
            try
            {
                string apiUrl = $"/api/Vehicle/vehicle/bakim?id={id}";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    List<Bakim> bakim = JsonConvert.DeserializeObject<List<Bakim>>(content);
                    return bakim;
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
        public async Task<LocationListViewModel> GetByLocation(int id)
        {
            try
            {
                string apiUrl = $"/api/Vehicle/vehicle/location?id={id}";

                var response = await _httpClient.GetAsync(apiUrl); // API'nin istenen endpoint'i
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini okuyun
                    string content = await response.Content.ReadAsStringAsync();
                    LocationListViewModel konum = JsonConvert.DeserializeObject<LocationListViewModel>(content);
                    return konum;
                }
                else
                {
                    // Yanıt başarısız olduysa null döndürün veya hata işleyin
                    Console.WriteLine("API'den veri alınamadı. Hata kodu: " + response.StatusCode);
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
        public async Task<string> FiyatGuncelle(decimal gunlukFiyat, decimal dakikaFiyat, int id)
        {

            // Verileri JSON formatına dönüştür
            var requestData = new
            {
                FiyatGunluk = gunlukFiyat,
                FiyatDakika = dakikaFiyat,
                FiyatId = id
            };
            var jsonData = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API'ye istek gönder
            string apiUrl = "api/Vehicle/vehicle/fiyat/update";
            var response = await _httpClient.PostAsync(apiUrl, content);


            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            return null;
        }
        public async Task<string> BakimEkle(Bakim bakim)
        {
            var json = JsonConvert.SerializeObject(bakim);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/vehicle/vehicle/bakim/create", data);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            return null;
        }
        public async Task<string> BakimSil(int Id)
        {
            var json = JsonConvert.SerializeObject(Id);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/vehicle/vehicle/bakim/delete", data);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            return null;
        }
        public async Task<string> AracSil(int Id)
        {
            var json = JsonConvert.SerializeObject(Id);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Vehicle/vehicle/vehicle/delete", data);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            return null;
        }
    }
}
