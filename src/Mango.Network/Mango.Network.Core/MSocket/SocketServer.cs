using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
namespace Mango.Network.Core.MSocket
{
    public class SocketServer
    {
        /// <summary>
        /// 异步连接处理对象
        /// </summary>
        private AsyncAccept _asyncAccept;
        /// <summary>
        /// 异步接收处理对象
        /// </summary>
        private AsyncReceive _asyncReceive;
        /// <summary>
        /// 服务器Socket对象
        /// </summary>
        private Socket _socket;
        /// <summary>
        /// 事件管理器,绑定相关事件(无需手动初始化)
        /// </summary>
        public EventManager EventManager { get; set; }

        public SocketServer()
        {
            EventManager = new EventManager();
        }
        /// <summary>
        /// 初始化基本设置信息
        /// </summary>
        private void Init(int maxConnectionCount)
        {
            _asyncReceive = new AsyncReceive(EventManager);
            _asyncAccept = new AsyncAccept(_asyncReceive, EventManager);
            //初始化配置项
            ConfigData.MaxClientCount = maxConnectionCount;
            
            ConfigData.UserTokenPool = new SocketAsyncEventArgsPool(ConfigData.MaxClientCount);
            ConfigData.MaxAcceptedClients = new System.Threading.Semaphore(ConfigData.MaxClientCount, ConfigData.MaxClientCount);
            //
            AsyncUserToken userToken;
            for (int i = 0; i < ConfigData.MaxClientCount; i++)
            {
                userToken = new AsyncUserToken(ConfigData.BufferSize);
                userToken.ReceiveEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(_asyncReceive.OnIOCompleted);
                userToken.SendEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(AsyncSend.OnIOCompleted);
                ConfigData.UserTokenPool.Push(userToken);
            }
        }
        /// <summary>
        /// 启动Socket服务
        /// </summary>
        /// <param name="Port">端口号</param>
        public void Start(string ip,int port,int maxConnectionCount)
        {
            //初始化
            Init(maxConnectionCount);
            //
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            // 创建监听socket
            _socket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            if (localEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
            {
                // 配置监听socket为 dual-mode (IPv4 & IPv6) 
                _socket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);
                _socket.Bind(new IPEndPoint(IPAddress.IPv6Any, localEndPoint.Port));
            }
            else
            {
                _socket.Bind(localEndPoint); 
            }
            // 开始监听
            _socket.Listen(ConfigData.MaxClientCount);
            ConfigData.ServerSocket = _socket;
            //在监听Socket上投递一个接受请求。
            _asyncAccept.StartAccept(null);
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            _socket.Dispose();
        }
    }
}
