using RMIMS.Client.Model;
using System.Collections.Generic;

namespace RMIMS.Client.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
    }
}
