using System;

namespace Mango.Network.Core.MSocket
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 心跳包消息
        /// </summary>
        Heartbeat = 0,
        /// <summary>
        /// 连接消息
        /// </summary>
        Line=1,
        /// <summary>
        /// 文字消息
        /// </summary>
        Writing=2,
        /// <summary>
        /// 图片消息
        /// </summary>
        Image=3,
        /// <summary>
        /// 表情消息
        /// </summary>
        Emoj=4,
        /// <summary>
        /// 语音消息
        /// </summary>
        Voice=5,
        /// <summary>
        /// 视频消息
        /// </summary>
        Video=6,
        /// <summary>
        /// 关闭连接回执消息
        /// </summary>
        Close = 98,
        /// <summary>
        /// 成功回执消息
        /// </summary>
        Receipt=99
    }
}
