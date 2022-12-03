using RMIMS.Client.Core.AOP.Aspects;
using RMIMS.Client.Core.API;
using RMIMS.Client.Model;
using System.Threading.Tasks;

namespace RMIMS.Client.Services.Interfaces.API.Login
{
    public interface ILoginService
    {
        void TestAop();
        Task<ServiceResponse> Login(LoginBody user);

        Task<ServiceResponse> GetUserInfo();
    }
}
