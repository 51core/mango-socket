using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Network.Core.MSocket
{
    public class EventManager
    {
        /// <summary>
        /// 客户端连接事件
        /// </summary>
        /// <param name="asyncUserToken"></param>
        /// <param name="currentClientCount"></param>
        public delegate void AcceptEventHandler(AsyncUserToken asyncUserToken,int currentClientCount);
        public event AcceptEventHandler Accept;
        public void OnAccept(AsyncUserToken asyncUserToken, int currentClientCount)
        {
            Accept?.Invoke(asyncUserToken, currentClientCount);
        }
        /// <summary>
        /// 消息接收事件
        /// </summary>
        /// <param name="asyncUserToken"></param>
        public delegate void ReceiveEventHandler(AsyncUserToken asyncUserToken);
        public event ReceiveEventHandler Receive;
        public void OnReceive(AsyncUserToken asyncUserToken)
        {
            Receive?.Invoke(asyncUserToken);
        }
    }
}
