using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices;

namespace ASREP_roastable_users
{
    class R
    {
        public void GetASREPRoastable()
        {
            Forest f = Forest.GetCurrentForest();
            DomainCollection domains = f.Domains;

            foreach (Domain d in domains)
            {
                //domain.local
                string domainName = d.Name.ToString();
                string[] dcs = domainName.Split('.');

                for(int i = 0; i < dcs.Length; i++)
                {
                    dcs[i] = "DC=" + dcs[i];
                    Console.WriteLine(dcs[i]);
                }

                //DC=sirket, DC=local, DC=net
                //account > 4260352
                DirectoryEntry de = new DirectoryEntry(String.Format("LDAP://{0}",String.Join(",",dcs)));
                DirectorySearcher ds = new DirectorySearcher();
                ds.SearchRoot = de;
                ds.Filter = "(&(objectclass=user)(!(objectclass=computer))(useraccountcontrol>=66083))";
                
                foreach(SearchResult sr in ds.FindAll())
                {
                    Console.WriteLine("User: {0} from Domain: {1}", sr.Properties["samaccountname"][0],domainName);
                    Console.WriteLine("UserAccountControl:{0}",sr.Properties["useraccountcontrol"][0]);
                    Console.WriteLine();
                }
                

            }

        }

        static void Main(string[] args)
        {
            try
            {
                R c = new R();
                c.GetASREPRoastable();
            }catch { }
           
        }
    }
}
