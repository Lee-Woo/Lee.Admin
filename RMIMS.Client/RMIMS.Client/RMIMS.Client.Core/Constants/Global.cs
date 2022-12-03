using RMIMS.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMIMS.Client.Core.Constants
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class Global
    {
        public static string Token { get; set; } = "";
        public static User User { get; set; } = new User();
        public static List<Authority> Authorities { get; set; } = new List<Authority>();
    }
}
