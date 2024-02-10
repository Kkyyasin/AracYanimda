using AracYanimdaApi.Data.Repository;
using System.Data.Common;
using System.Data;
using AracYanimdaApi.Models.Admin;

namespace AracYanimdaApi.Data.Service
{
    public class AdminService
    {
        private readonly AdminRepository _adminRepository;

        public AdminService()
        {
            _adminRepository = new AdminRepository();
        }
        public Admin Login(string email, string password)
        {
            DataTable data=_adminRepository.Login(email, password);
            if(data != null || data.Rows.Count>0) {
            DataRow row= data.Rows[0];
                Admin admin = new Admin
                {
                    AdminId = Convert.ToInt32(row["admin_id"]),
                    Email = row["email"].ToString(),
                    Username = row["username"].ToString(),
                    Password = row["password"].ToString()
                };
                return admin;
            }
            return null;
        }
        public bool Register(Admin admin)
        {
            return _adminRepository.Register(admin.Email, admin.Password, admin.Username);
        }
    
    }
}
