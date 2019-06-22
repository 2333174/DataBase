using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DB;

namespace Server
{
    class Program
    {
        //创建一个和客户端通信的套接字
        static Socket SocketWatch = null;
        //定义一个集合，存储客户端信息
        static Dictionary<string, Socket> ClientConnectionItems = new Dictionary<string, Socket> { };
        static Dictionary<int, Socket> Judges = new Dictionary<int, Socket>();//对应的客户端地址
        static Dictionary<int, string> MatchGroups = new Dictionary<int, string>();
        static Dictionary<string, int> MJudgesNum = new Dictionary<string, int>();//小组裁判数
        static Dictionary<string, int> MajorJudge = new Dictionary<string, int>();//小组主裁判 
        static List<Socket> ManagerSockets = new List<Socket>();//管理员
        static Dictionary<string, List<string>> SEJudgedGroup = new Dictionary<string, List<string>>(); // 项目被打分过的数目
        static List<Socket> TeamSockets = new List<Socket>();
        static Dictionary<Socket, int> TeamIDs = new Dictionary<Socket, int>();
        //static Dictionary<int, Socket> TeamIDSockets = new Dictionary<int, Socket>();
        static void Main(string[] args)
        {
            //端口号（用来监听的）
            int port = 6000;

            //string host = "127.0.0.1";
            //IPAddress ip = IPAddress.Parse(host);
            IPAddress ip = IPAddress.Any;

            //将IP地址和端口号绑定到网络节点point上  
            IPEndPoint ipe = new IPEndPoint(ip, port);

            //定义一个套接字用于监听客户端发来的消息，包含三个参数（IP4寻址协议，流式连接，Tcp协议）  
            SocketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //监听绑定的网络节点  
            SocketWatch.Bind(ipe);
            //将套接字的监听队列长度限制为20  
            SocketWatch.Listen(20);


            //负责监听客户端的线程:创建一个监听线程  
            Thread threadwatch = new Thread(WatchConnecting);
            //将窗体线程设置为与后台同步，随着主线程结束而结束  
            threadwatch.IsBackground = true;
            //启动线程     
            threadwatch.Start();

            Console.WriteLine("开启监听......");
            Console.WriteLine("点击输入任意数据回车退出程序......");
            Console.ReadKey();

            SocketWatch.Close();
        }

        //监听客户端发来的请求  
        static void WatchConnecting()
        {
            Socket connection = null;

            //持续不断监听客户端发来的请求     
            while (true)
            {
                try
                {
                    connection = SocketWatch.Accept();
                }
                catch (Exception ex)
                {
                    //提示套接字监听异常
                    Console.WriteLine(ex.Message);
                    break;
                }

                //客户端网络结点号  
                string remoteEndPoint = connection.RemoteEndPoint.ToString();
                //添加客户端信息  
                ClientConnectionItems.Add(remoteEndPoint, connection);
                byte[] arrServerRecMsg = new byte[1024 * 1024];
                int length = connection.Receive(arrServerRecMsg);
                //将机器接受到的字节数组转换为人可以读懂的字符串     
                string strSRecMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);
                string[] teamMessage = strSRecMsg.Split(':');
                if (strSRecMsg == "管理")
                {
                    ManagerSockets.Add(connection);
                }
                else if (teamMessage[0] == "报名")
                {
                    TeamSockets.Add(connection);
                    //TeamIDSockets.Add(int.Parse(teamMessage[1]),connection);
                    TeamIDs.Add(connection, int.Parse(teamMessage[1]));
                }
                else
                {
                    Judges.Add(int.Parse(strSRecMsg), connection);
                }
                //显示与客户端连接情况
                Console.WriteLine("\r\n[客户端\"" + remoteEndPoint + strSRecMsg + "\"建立连接成功！ 客户端数量：" + ClientConnectionItems.Count + "]");

                //获取客户端的IP和端口号  
                IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;

                //让客户显示"连接成功的"的信息  
                string sendmsg = "[" + "本地IP：" + clientIP + " 本地端口：" + clientPort.ToString() + " 连接服务端成功！]";
                byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendmsg);
                connection.Send(arrSendMsg);

                //创建一个通信线程      
                Thread thread = new Thread(recv);
                //设置为后台线程，随着主线程退出而退出 
                thread.IsBackground = true;
                //启动线程     
                thread.Start(connection);
            }
        }

        /// <summary>
        /// 接收客户端发来的信息，客户端套接字对象
        /// </summary>
        /// <param name="socketclientpara"></param>
        static void recv(object socketclientpara)
        {
            Socket socketServer = socketclientpara as Socket;

            while (true)
            {
                //创建一个内存缓冲区，其大小为1024*1024字节  即1M     
                byte[] arrServerRecMsg = new byte[1024 * 1024];
                //将接收到的信息存入到内存缓冲区，并返回其字节数组的长度    
                try
                {
                    int length = socketServer.Receive(arrServerRecMsg);

                    //将机器接受到的字节数组转换为人可以读懂的字符串     
                    string strSRecMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);
                    if (ManagerSockets.IndexOf(socketServer) != -1)
                    {
                        string[] introductions = strSRecMsg.Split(',');
                        if (introductions[0] != "主裁判")
                        {
                            MJudgesNum.Add(introductions[introductions.Length - 1], introductions.Length - 1);//记录分裁判数
                            for (int i = 0; i < introductions.Length - 1; i++)
                            {
                                Judges[int.Parse(introductions[i])].Send(Encoding.UTF8.GetBytes("打分:" + introductions[introductions.Length - 1]));
                            }
                        }
                        else if((introductions[0] == "主裁判"))
                        {
                            MajorJudge.Add(introductions[2], int.Parse(introductions[1]));//管理发送“主裁判，1,010101”
                        }
                    }
                    else
                    {
                        string[] introductions = strSRecMsg.Split(':');//分裁判打完分 发送“分裁判打完分:010101”
                        if (introductions[0]== "分裁判打完分")
                        {
                            MJudgesNum[introductions[1]] = MJudgesNum[introductions[1]] - 1;
                            if (MJudgesNum[introductions[1]] == 0)
                            {
                                Judges[MajorJudge[introductions[1]]].Send(Encoding.UTF8.GetBytes("打分:" + introductions[1]));
                            }
                        }else if (introductions[0] == "主裁判打完分")
                        {
                            MajorJudge.Remove(introductions[1]);
                            if (!SEJudgedGroup.ContainsKey(introductions[1].Substring(0,4)))
                            {
                                SEJudgedGroup.Add(introductions[1].Substring(0, 4), new List<string>());
                                SEJudgedGroup[introductions[1].Substring(0, 4)].Add(introductions[1]);
                            }
                            else
                            {
                                string res = IsMatchGroupIn(introductions[1]);
                                if (res != null)
                                    SEJudgedGroup[res].Add(introductions[1]);
                                else
                                    throw new Exception("重复打分!");
                            }
                            if(CheckSEIsJudgesOver(introductions[1].Substring(0, 4)))
                            {
                                var prs = Ranking(introductions[1].Substring(0, 4));
                                if (introductions[1].Substring(3, 1) == "0")
                                {
                                    GymDBService dbs = new GymDBService();
                                    dbs.Promote(prs, 8, 8);
                                }
                                SortedSet<Socket> sockets = new SortedSet<Socket>();
                                foreach (var pr in prs)
                                {
                                    GymDBService dbs = new GymDBService();
                                    var targetathlete = dbs.GetAthleteByID(pr.AthleteID);
                                    if (TeamIDs.ContainsValue((int)targetathlete.TID))
                                    {
                                        foreach (var kvp in TeamIDs)
                                        {
                                            if(kvp.Value.Equals((int)targetathlete.TID))
                                                sockets.Add(kvp.Key);
                                        }
                                    }
                                }
                                foreach(var socket in sockets)
                                {
                                    socket.Send(Encoding.UTF8.GetBytes("更新界面"));
                                }
                            }
                        }
                    }
                    //将发送的字符串信息附加到文本框txtMsg上     
                    Console.WriteLine("\r\n[客户端：" + socketServer.RemoteEndPoint + " 时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "]\r\n" + strSRecMsg);

              
                    //发送客户端数据
                    if (ClientConnectionItems.Count > 0)
                    {
                        foreach (var socketTemp in ClientConnectionItems)
                        {
                            socketTemp.Value.Send(Encoding.UTF8.GetBytes(strSRecMsg));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    int t = -1;
                    if (ManagerSockets.IndexOf(socketServer) != -1) ManagerSockets.Remove(socketServer);
                    if (TeamSockets.IndexOf(socketServer) != -1)
                    {
                        TeamSockets.Remove(socketServer);
                        //TeamIDSockets.Remove(TeamIDs[socketServer]);
                        TeamIDs.Remove(socketServer);
                    }
                    foreach (var socketTemp in Judges)
                    {
                        if (socketTemp.Value == socketServer)
                        {
                            t = socketTemp.Key;
                            break;
                        }
                    }
                    if (t != -1) Judges.Remove(t);
                    ClientConnectionItems.Remove(socketServer.RemoteEndPoint.ToString());
                    //提示套接字监听异常  
                    Console.WriteLine("\r\n[客户端\"" + socketServer.RemoteEndPoint + "\"已经中断连接！ 客户端数量：" + ClientConnectionItems.Count + "]");
                    //关闭之前accept出来的和客户端进行通信的套接字 
                    socketServer.Close();
                    break;
                }
            }
        }

        private static string IsMatchGroupIn(string mgid)
        {
            List<string> keys = SEJudgedGroup.Keys.ToList();
            for (int i = 0; i < keys.Count(); i++)
            {
                if (SEJudgedGroup[keys[i]].Contains(mgid))
                    return keys[i];
            }
            return null;
        }

        private static bool CheckSEIsJudgesOver(string se)
        {
            GymDBService dbs = new GymDBService();
            List<string> target = dbs.GetGroupidBySportsEvent(se);
            target.Sort();
            List<string> query = SEJudgedGroup[se];
            query.Sort();
            if (target.SequenceEqual(query))
                return true;
            return false;
        }

        private static List<PersonalResult> Ranking(string se)
        {
            List<string> targetMGids = SEJudgedGroup[se];
            GymDBService dbs = new GymDBService();
            List<PersonalResult> prs = new List<PersonalResult>();
            foreach (var item in targetMGids)
            {
                List<PersonalResult> tpr = dbs.GetPersonalResultsByGroupID(item);
                prs.AddRange(tpr);
            }
            dbs.Ranking(prs);
            return prs;
            //dbs.Promote(prs, 8, 8);
        }
    }
}
