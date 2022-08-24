using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WritableFiles
{
    public class Class1
    {

        public void GetWritable(string[] arg)
        {
            string root = arg[0];
            string filter="";
            if (arg.Length>1)
            {
                filter = arg[1];
            }
            else
            {
                filter = "*";
            }

            var dirs = Directory.EnumerateDirectories(root);
            foreach(string dir in dirs)
            {
                //Console.WriteLine(dir);
                try
                {
                    var files = Directory.EnumerateFiles(dir, filter, SearchOption.AllDirectories);

                    foreach(string file in files)
                    {
                        //Console.WriteLine(file);
                        try
                        {
                            FileStream fs = File.Open(file, FileMode.Open, FileAccess.ReadWrite);
                            Console.WriteLine("Write Access on {0}",file);
                        }
                        catch {}
                    }
                }
                catch{}
            }

        }
        static void Main(string[] args)
        {
            //Finding Writable Files

            string root = args[0];
            //string root = @"C:\";
            Class1 c = new Class1();
            c.GetWritable(args);

            //Console.ReadKey();
            
        }
    }
}
