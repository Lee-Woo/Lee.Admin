using MySqlX.XDevAPI.Common;
using RMIMS.Client.Core.API;
using RMIMS.Client.Model;
using RMIMS.Client.Services.Interfaces.API.Menu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMIMS.Client.Services.API.Menu
{
    public class RouterService : IRouterService
    {
        /// <summary>
        /// 获取菜单路由
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse> GetRouter()
        {
            var t = await Task.Run(() =>
            {
                return "{module:testedit}";
            });
            return new ServiceResponse() { Success = t != null, Results = t };
        }
    }
}
