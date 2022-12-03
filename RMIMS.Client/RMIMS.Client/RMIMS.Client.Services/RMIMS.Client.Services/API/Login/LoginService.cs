using Newtonsoft.Json;
using RMIMS.Client.Core.AOP.Aspects;
using RMIMS.Client.Core.API;
using RMIMS.Client.Core.Constants;
using RMIMS.Client.Model;
using RMIMS.Client.Services.Interfaces.API.Login;
using System;
using System.Threading.Tasks;

namespace RMIMS.Client.Services.API.Login
{
    [ExceptionAspect]
    public class LoginService : ILoginService
    {
        public async Task<ServiceResponse> Login(LoginBody user)
        {
            var tlogin = Task.Run(() =>
            {
                ServiceResponse token = Request.GetToken(RequestUrl.LoginUrl, JsonConvert.SerializeObject(user));
                return token;
            });
   
            var timeouttask = Task.Delay(3000);
            var completedTask = await Task.WhenAny(tlogin, timeouttask);
            if (completedTask == timeouttask)
            {
                return new ServiceResponse() { Success = timeouttask != null, Results = "登录超时" };
            }
            else
            {
                return tlogin.Result;
            }
        }
        public async Task<ServiceResponse> GetUserInfo()
        {
            var tlogin = await Task.Run(() =>
            {
                ServiceResponse resp = Request.GetData(RequestUrl.UserInfo);
                return resp;
            });

            return tlogin;
        }

        public void TestAop()
        {
            Console.WriteLine("TestAop");
        }
    }
}
