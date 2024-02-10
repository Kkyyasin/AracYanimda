using MySql.Data.MySqlClient;
using System;
using System.Data;
namespace AracYanimdaApi.Data
{
    
    
    public class DatabaseUpdater
    {
        private Timer _timer;

        public void StartUpdating()
        {
            // Timer'ı oluştur ve çalışma aralığını belirle (örneğin her 1 saatte bir)
            _timer = new Timer(UpdateDatabase, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        }

        private void UpdateDatabase(object state)
        {
            
        }

        public void StopUpdating()
        {
            // Timer'ı durdur
            _timer?.Change(Timeout.Infinite, 0);
        }
    }

    // Kullanım örneği
    class Program
    {
        static void Main()
        {
            var updater = new DatabaseUpdater();
            updater.StartUpdating(); // Güncelleme işlemini başlat

            // Uygulama çalıştığı sürece güncellemeler devam eder, isterseniz durdurabilirsiniz
            // updater.StopUpdating();
        }
    }
}
