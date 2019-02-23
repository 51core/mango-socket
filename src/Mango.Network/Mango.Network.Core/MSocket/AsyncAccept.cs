using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
namespace Mango.Network.Core.MSocket
{
    public class AsyncAccept
    {
        private AsyncReceive _asyncReceive { get; set; }

        private EventManager _eventManager;
        public AsyncAccept(AsyncReceive asyncReceive,EventManager eventManager)
        {
            _asyncReceive = asyncReceive;
            _eventManager = eventManager;
        }
        /// <summary>
        /// 从客户端开始接受一个连接操作
        /// </summary>
        public void StartAccept(SocketAsyncEventArgs AsyncEventArgs)
        {
            if (AsyncEventArgs == null)
            {
                AsyncEventArgs = new SocketAsyncEventArgs();
                AsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
            }
            else
            {
                AsyncEventArgs.AcceptSocket = null;
            }
            ConfigData.MaxAcceptedClients.WaitOne();
            if (!ConfigData.ServerSocket.AcceptAsync(AsyncEventArgs))
            {
                ProcessAccept(AsyncEventArgs);
            }
        }

        /// <summary>
        /// accept 操作完成时回调函数
        /// </summary>
        /// <param name="sender">Object who raised the event.</param>
        /// <param name="e">SocketAsyncEventArg associated with the completed accept operation.</param>
        public void OnAcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
             ProcessAccept(e);
        }

        /// <summary>
        /// 监听Socket接受处理
        /// </summary>
        /// <param name="e"></param>
        public void ProcessAccept(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                Socket socket = e.AcceptSocket;//和客户端关联的socket
                if (socket.Connected)
                {
                    try
                    {
                        Interlocked.Increment(ref ConfigData.CurrentClientCount);//原子操作加1
                        AsyncUserToken userToken = ConfigData.UserTokenPool.Pop();
                        userToken.ConnectSocket = socket;
                        //调用连接事件
                        _eventManager.OnAccept(userToken, ConfigData.CurrentClientCount);
                        if (!socket.ReceiveAsync(userToken.ReceiveEventArgs))//投递接收请求
                        {
                            _asyncReceive.ProcessReceive(userToken.ReceiveEventArgs);
                        }
                    }
                    catch (SocketException ex)
                    {
                        throw ex;
                    }
                    //投递下一个接受请求
                    StartAccept(e);
                }
            }
        }
    }
}
