using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
namespace Mango.Network.Core.MSocket
{
    public class AsyncReceive
    {
        private EventManager _eventManager;
        public AsyncReceive(EventManager eventManager)
        {
            _eventManager = eventManager;
        }
        /// <summary>
        ///接收完成时处理函数
        /// </summary>
        /// <param name="e">与接收完成操作相关联的SocketAsyncEventArg对象</param>
        public void ProcessReceive(SocketAsyncEventArgs e)
        {
            AsyncUserToken userToken = e.UserToken as AsyncUserToken;
            if (userToken.ReceiveEventArgs.BytesTransferred > 0 && userToken.ReceiveEventArgs.SocketError == SocketError.Success)
            {
                Socket sock = userToken.ConnectSocket;
                //把收到的数据写入到缓存区里面
                userToken.ReceiveBuffer.WriteBuffer(e.Buffer, e.Offset, e.BytesTransferred);
                //调用消息处理事件
                _eventManager.OnReceive(userToken);
                Array.Clear(e.Buffer,0, e.BytesTransferred);
                //为接收下一段数据，投递接收请求，这个函数有可能同步完成，这时返回false，并且不会引发SocketAsyncEventArgs.Completed事件
                if (!sock.ReceiveAsync(userToken.ReceiveEventArgs))
                {
                    //同步接收时处理接收完成事件
                    ProcessReceive(userToken.ReceiveEventArgs); 
                }
            }
        }
        /// <summary>
        /// 当接收完成时调用此函数
        /// </summary>
        /// <param name="sender">激发事件的对象</param>
        /// <param name="e">与发送或接收完成操作相关联的SocketAsyncEventArg对象</param>
        public void OnIOCompleted(object sender, SocketAsyncEventArgs e)
        {
            AsyncUserToken userToken = e.UserToken as AsyncUserToken;
            lock (userToken)
            {
                ProcessReceive(e);
            }
        }
    }
}
