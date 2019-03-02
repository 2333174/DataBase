using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
namespace Client
{
    class Problem
    {
        private static byte[] result = new byte[1024 * 1024];
        static Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    int ReceiveLength = ClientSocket.Receive(result);
                    Console.WriteLine("接收服务器消息：{0}", Encoding.UTF8.GetString(result, 0, ReceiveLength));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("出现异常了！！！");
                    Console.WriteLine(ex.Message);
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close();
                    break;
                }
            }
        }
        static void Main(string[] args)
        {
            int Port = 8333;
            IPAddress IP = IPAddress.Parse("127.0.0.1");
            try
            {
                ClientSocket.Connect(new IPEndPoint(IP, Port));
                Thread thread = new Thread(ReceiveMessage);
                thread.Start();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine("连接服务器失败，按回车键退出！");
                return;
            }
            Console.WriteLine("连接服务器成功！！！");
            //int ReceiveLength = ClientSocket.Receive(result);
            //Console.WriteLine("接收服务器消息：{0}", Encoding.UTF8.GetString(result, 0, ReceiveLength));
            while (true)
            {
                try
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("向服务器发送消息：");
                    string sb = Console.ReadLine();
                    string Client = ClientSocket.RemoteEndPoint.ToString();
                    string SendMessage = "接收客户端" + Client + "消息：" + DateTime.Now + "\n" + sb;
                    ClientSocket.Send(Encoding.ASCII.GetBytes(sb));
                }
                catch (Exception ex)
                {
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close();
                    break;
                }
            }
            Console.WriteLine("消息发送完毕！！！");
            Console.ReadLine();
        }
    }
}