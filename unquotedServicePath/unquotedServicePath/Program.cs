using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.ServiceProcess;

namespace unquotedServicePath
{
    class Program
    {
        static void Main(string[] args)
        {
            //HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\
            
            ServiceController[] scs = ServiceController.GetServices();

            foreach (ServiceController s in scs)
            {
                RegistryKey rkey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" +s.ServiceName);
                string path = rkey.GetValue("ImagePath").ToString();

                if (path[0] != '"' && !path.Contains("system32") && !path.Contains("System32"))
                {
                    Console.WriteLine(s.ServiceName);
                    Console.WriteLine(path);
                }
            }
            //Console.ReadKey();
        }
    }
}
