﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;


namespace reverse_shell
{
    class Program
    {
        static void Main(string[] args)
        {
            /*IPHostEntry ipe = Dns.GetHostEntry("localhost");
            IPAddress[] ips = ipe.AddressList;

            foreach(IPAddress ip in ips)
            {
                Console.WriteLine(ip.ToString());
            }*/

            //Console.WriteLine(IPAddress.Any);


            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 1234);
            Socket server_socket = new Socket(IPAddress.Any.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            server_socket.Bind(ipe);
            server_socket.Listen(5);

            Socket client_socket = server_socket.Accept();
            Console.WriteLine("[+] Connection from: {0}", client_socket.RemoteEndPoint);

            Console.WriteLine("Enter message to send :");
            string msg; 
            msg= Console.ReadLine();

            client_socket.Send(Encoding.ASCII.GetBytes(msg));
            while (msg != "quit")
            {
               
                byte[] sb = new byte[2048];

                Array.Clear(sb, 0, sb.Length);
                client_socket.Receive(sb);
                Console.WriteLine(Encoding.ASCII.GetString(sb).TrimEnd('\0'));

                Console.WriteLine("Enter message to send :");
                msg = Console.ReadLine();
                client_socket.Send(Encoding.ASCII.GetBytes(msg));
            }
            server_socket.Close();
            //Console.ReadKey();
        }
    }
}
