using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AracYanimdaWeb.Models;
using System.Text;
using System.IO.Compression;

using AracYanimdaWeb.Models.Admin;
namespace AracYanimdaWeb.Service
{
    public class AdminApiService
    {
        private readonly HttpClient _httpClient;

        public AdminApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://aracimyanimda.azurewebsites.net"); // API'nin URL'si
            // Gerekirse, ek ayarlamalar yapabilirsiniz
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer YourAccessToken");
        }
        public async Task<Admin> Login(Admin admin)
        {
            var json = JsonConvert.SerializeObject(admin);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Admin/login", data);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Admin a = JsonConvert.DeserializeObject<Admin>(responseContent);
                return a;
            }

            return null;
        }
        public async Task<string> Register(Admin admin)
        {
            var json = JsonConvert.SerializeObject(admin);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Admin/register", data);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            return null;
        }

    }
}