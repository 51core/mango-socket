using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Mango.Network.Core.MSocket;
namespace Mango.Network.Server
{
    public class Program
    {
        private static readonly IConfigurationBuilder Configuration = new ConfigurationBuilder();
        private static IConfigurationRoot _configuration;
        static void Main(string[] args)
        {
            try
            {
                //配置文件处理
                _configuration = Configuration.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables().Build();
                //读取数据配置文件信息
                string serverIP= _configuration.GetSection("ServerIPAddress").Value;
                int port = int.Parse(_configuration.GetSection("ServerPort").Value);
                int maxConnectionCount = int.Parse(_configuration.GetSection("MaxConnectionCount").Value);
                //配置Sokcet服务器端
                SocketServer server = new SocketServer();
                //绑定事件
                server.EventManager.Accept += EventManager_Accept;
                server.EventManager.Receive += EventManager_Receive;
                //启动服务器
                server.Start(serverIP, port, maxConnectionCount);
                Console.WriteLine(string.Format("{0}:{1} 最大连接数:{2} start success", serverIP, port, maxConnectionCount));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("run error {0}", ex.Message));
            }
            Console.ReadLine();
        }

        private static void EventManager_Receive(AsyncUserToken asyncUserToken)
        {
            byte[] buffer = new byte[asyncUserToken.ReceiveBuffer.DataCount];
            Array.Copy(asyncUserToken.ReceiveBuffer.Buffer, 0, buffer, 0, buffer.Length);
            string info = System.Text.Encoding.UTF8.GetString(buffer);
            asyncUserToken.ReceiveBuffer.Clear();

            Console.WriteLine(string.Format("接收到的消息:{0}", info));
            AsyncSend.Send(asyncUserToken.SendEventArgs, System.Text.Encoding.UTF8.GetBytes("hello,my .net core."));
        }

        private static void EventManager_Accept(AsyncUserToken asyncUserToken, int currentClientCount)
        {
            Console.WriteLine(string.Format("客户端 {0} 连接到服务器,当前共有连接数:{1}", asyncUserToken.ConnectSocket.RemoteEndPoint.ToString(), currentClientCount));
        }
    }
}
