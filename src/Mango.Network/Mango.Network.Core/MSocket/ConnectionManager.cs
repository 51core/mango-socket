using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Network.Core.MSocket
{
    public class ConnectionManager
    {
        /// <summary>
        /// 保存客户端连接的对象
        /// </summary>
        public static AsyncUserToken ClientUserToken { get; set; }
    }
}
