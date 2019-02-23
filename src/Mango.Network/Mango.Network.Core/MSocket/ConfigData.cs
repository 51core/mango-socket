using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
namespace Mango.Network.Core.MSocket
{
    internal class ConfigData
    {
        /// <summary>
        /// 服务器端Socket对象
        /// </summary>
        internal static Socket ServerSocket { get; set; }
        /// <summary>
        /// 信号量
        /// </summary>
        internal static Semaphore MaxAcceptedClients { get; set; }
        /// <summary>
        /// 对象池
        /// </summary>
        internal static SocketAsyncEventArgsPool UserTokenPool { get; set; }
        /// <summary>
        /// 允许最大的客户端连接数
        /// </summary>
        internal static int MaxClientCount { get; set; }
        /// <summary>
        /// 当前连接的客户端数
        /// </summary>
        internal static int CurrentClientCount = 0;
        /// <summary>
        /// 用于每个I/O Socket操作的缓冲区大小 默认1024
        /// </summary>
        internal static int BufferSize { get; set; } = 1024;
        
    }
}
