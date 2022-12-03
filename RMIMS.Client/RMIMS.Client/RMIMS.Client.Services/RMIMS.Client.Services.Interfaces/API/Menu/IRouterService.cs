using RMIMS.Client.Core.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMIMS.Client.Services.Interfaces.API.Menu
{
    public interface IRouterService
    {
        Task<ServiceResponse> GetRouter();
    }
}
