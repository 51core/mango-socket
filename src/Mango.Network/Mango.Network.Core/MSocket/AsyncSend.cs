using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
namespace Mango.Network.Core.MSocket
{
    public class AsyncSend
    {
        /// <summary>
        /// 异步的发送数据
        /// </summary>
        /// <param name="e"></param>
        /// <param name="data"></param>
        public static void Send(SocketAsyncEventArgs e, byte[] data)
        {
            AsyncUserToken userToken = e.UserToken as AsyncUserToken;
            if (userToken.SendEventArgs.SocketError == SocketError.Success)
            {
                if (userToken.ConnectSocket.Connected)
                {
                    //设置发送数据
                    e.SetBuffer(data,0,data.Length);
                    //投递发送请求，这个函数有可能同步发送出去，这时返回false，并且不会引发SocketAsyncEventArgs.Completed事件
                    if (!userToken.ConnectSocket.SendAsync(userToken.SendEventArgs))
                    {
                        //同步发送时处理发送完成事件
                        ProcessSend(userToken.SendEventArgs);
                    }
                }
            }
        }
        /// <summary>
        /// 发送完成时处理函数
        /// </summary>
        /// <param name="e">与发送完成操作相关联的SocketAsyncEventArg对象</param>
        private static void ProcessSend(SocketAsyncEventArgs e)
        {
            AsyncUserToken userToken = e.UserToken as AsyncUserToken;
            if (userToken.SendEventArgs.SocketError == SocketError.Success)
            {
                //TODO
                if (e.Buffer != null)
                {
                    Array.Clear(e.Buffer, 0, e.Buffer.Length);
                    e.SetBuffer(null, 0, 0);
                }
            }
        }
        /// <summary>
        /// 当接收完成时调用此函数
        /// </summary>
        /// <param name="sender">激发事件的对象</param>
        /// <param name="e">与发送或接收完成操作相关联的SocketAsyncEventArg对象</param>
        public static void OnIOCompleted(object sender, SocketAsyncEventArgs e)
        {
            
        }
    }
}
