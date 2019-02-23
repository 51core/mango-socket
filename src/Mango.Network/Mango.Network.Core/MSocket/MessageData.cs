using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mango.Network.Core.MSocket
{
    public class MessageData
    {
        /// <summary>
        /// 消息ID由客户端自己生成唯一ID(GUID)
        /// </summary>
        public string MessageID { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// 发送用户
        /// </summary>
        public string SendUserID { get; set; }
        /// <summary>
        /// 接收用户
        /// </summary>
        public string ReceiveUserID { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageBody { get; set; }
    }
}
