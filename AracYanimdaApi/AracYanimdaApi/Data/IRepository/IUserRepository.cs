using System.Data;

namespace AracYanimdaApi.Data.IRepository
{
    public interface IUserRepository
    {
        DataTable GetAllUsers();
        DataTable GetUserById(int userId);
     
    }
}
