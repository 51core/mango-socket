using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;
using System.Net;
namespace Mango.Network.Client
{
    public class Program
    {
        private static readonly IConfigurationBuilder Configuration = new ConfigurationBuilder();
        private static IConfigurationRoot _configuration;
        private static byte[] buffer = new byte[1024];
        static void Main(string[] args)
        {
            try
            {
                //配置文件处理
                _configuration = Configuration.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables().Build();
                //读取数据配置文件信息
                string serverIP = _configuration.GetSection("ServerIPAddress").Value;
                int port = int.Parse(_configuration.GetSection("ServerPort").Value);
                //简单的写一个Socket端
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(IPAddress.Parse(serverIP), port);
                socket.Send(System.Text.Encoding.UTF8.GetBytes("hello,my socket."));
                Console.WriteLine(string.Format("connection success 服务器,{0}:{1}", serverIP, port));
                //简单异步接收服务器拿到的消息
                socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveCallback), socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("run error {0}", ex.Message));
            }
            Console.ReadLine();
        }
        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = ar.AsyncState as Socket;
                int REnd = socket.EndReceive(ar);
                if (REnd > 0)
                {
                    byte[] data = new byte[REnd];
                    Array.Copy(buffer, 0, data, 0, REnd);

                    //在此次可以对data进行按需处理
                    string info = System.Text.Encoding.UTF8.GetString(data);
                    Console.WriteLine(string.Format("接收到的消息:{0}", info));
                    //
                    socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveCallback), null);
                }
            }
            catch (SocketException ex)
            { }
        }
    }
}
