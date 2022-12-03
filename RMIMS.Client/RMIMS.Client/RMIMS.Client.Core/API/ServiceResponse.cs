using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMIMS.Client.Core.API
{
    [Serializable]
    public class ServiceResponse
    {
        private bool _success = false;
        private string _code = "";

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 操作成功与否
        /// </summary>
        public bool Success { get { return _success; } set { _success = value; } }

        /// <summary>
        /// 返回码
        /// </summary>
        public string Code { get { return _code; } set { _code = value; } }

        /// <summary>
        /// 返回的列表结果
        /// </summary>
        public dynamic Results { get; set; }

    }
}
