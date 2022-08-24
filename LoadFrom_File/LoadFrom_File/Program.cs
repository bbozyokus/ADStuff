using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace LoadFrom_File
{
    class Program
    {
        public void LoadFromFile(string filepath)
        {
            Assembly assembly = Assembly.LoadFile(filepath);
            string[] newargs = { };
            assembly.EntryPoint.Invoke(null, new object[] { newargs });

        }
        static void Main(string[] args)
        {
            Thread t = new Thread(() =>
            {
                Program p = new Program();
                p.LoadFromFile(@"C:\Windows\System32\calc.exe");
            });
            t.Start();
            t.Join();
            Console.WriteLine("Press anything to continue");
            Console.ReadKey();
        }
    }
}
