using Newtonsoft.Json;
using RestSharp;
using RMIMS.Client.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace RMIMS.Client.Core.API
{
    public class Request
    {
        public static string TestRequest()
        {
            string reqToken = string.Empty;
            var client = new RestClient("https://www.baidu.com");
            var request = new RestRequest() { Method = Method.Get };
            var req = client.Execute(request);
            string code = ((int)req.StatusCode).ToString();
            return reqToken;
        }

        private static ServiceResponse Execute(RestClient client, RestRequest request)
        {
            string reqToken = string.Empty;
            var req = client.Execute(request);
            if (req.StatusCode == HttpStatusCode.OK)
            {
                if (req != null && !string.IsNullOrEmpty(req.Content))
                    reqToken = req.Content;

                return new ServiceResponse() { Success = true, Code = ((int)req.StatusCode).ToString(), Results = reqToken };
            }

            return new ServiceResponse() { Success = false, Code = ((int)req.StatusCode).ToString(), Message = req.ErrorMessage };
        }

        public static ServiceResponse GetToken(string loginRoute, string loginInfo)
        {
            var client = new RestClient(RequestUrl.BASE1_URL + loginRoute);
            var request = new RestRequest() { Method = Method.Post, RequestFormat = DataFormat.Json };
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddBody(loginInfo, "application/json");
            return Execute(client, request);
        }

        public static ServiceResponse GetData(string route, string paras = null)
        {
            var client = new RestClient(RequestUrl.BASE1_URL + route);
            var request = new RestRequest() { Method = Method.Get, RequestFormat = DataFormat.Json, Timeout = 3000 };
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddHeader("Authorization", "Bearer " + Global.Token);
            request.AddJsonBody(paras, "application/json");
            return Execute(client, request);
        }

    }
}
