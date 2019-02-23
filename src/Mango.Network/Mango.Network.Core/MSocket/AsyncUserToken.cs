using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Mango.Network.Core.MSocket
{
    public class AsyncUserToken
    {
        /// <summary>
        /// 用来接收数据的SocketAsyncEventArgs
        /// </summary>
        public SocketAsyncEventArgs ReceiveEventArgs { get; set; }
        /// <summary>
        /// 用来发送数据的SocketAsyncEventArgs
        /// </summary>
        public SocketAsyncEventArgs SendEventArgs { get; set; }
        /// <summary>
        /// 接收数据的缓冲区
        /// </summary>
        public byte[] AsyncReceiveBuffer { get; set; }

        /// <summary>
        /// 发送数据的缓冲区
        /// </summary>
        public byte[] AsyncSendBuffer { get; set; }
        /// <summary>
        /// 动态的接收缓冲区
        /// </summary>
        public BufferManager ReceiveBuffer { get; set; }

        /// <summary>
        /// 动态的发送缓冲区
        /// </summary>
        public BufferManager SendBuffer { get; set; }
        /// <summary>
        /// 连接的Socket对象
        /// </summary>
        public Socket ConnectSocket { get; set; }
        /// <summary>
        /// 构造函数(初始化需要的对象)
        /// </summary>
        public AsyncUserToken(int MacBufferSize)
        {
            //数据接收对象
            ReceiveEventArgs = new SocketAsyncEventArgs();
            ReceiveEventArgs.UserToken = this;
            AsyncReceiveBuffer = new byte[MacBufferSize];
            ReceiveEventArgs.SetBuffer(AsyncReceiveBuffer,0, AsyncReceiveBuffer.Length);
            ReceiveBuffer = new BufferManager(1024);
            //数据发送对象
            SendEventArgs = new SocketAsyncEventArgs();
            SendEventArgs.UserToken = this;
            AsyncSendBuffer = new byte[MacBufferSize];
            SendEventArgs.SetBuffer(AsyncSendBuffer,0, AsyncSendBuffer.Length);
            SendBuffer = new BufferManager(1024);
        }
    }
}
