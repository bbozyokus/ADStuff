using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;

namespace FindingMembers
{
    public class C1
    {
        public void GetMembers(string groupName,string domainName)
        {
            PrincipalContext p = new PrincipalContext(ContextType.Domain,domainName);
            GroupPrincipal gp = GroupPrincipal.FindByIdentity(p, groupName);

            foreach(Principal group in gp.GetMembers())
            {
                if (group.StructuralObjectClass == "user")
                {
                    Console.WriteLine("[+] User {0} is member of {1}", group.Name,groupName);
                }

                if (group.StructuralObjectClass == "group")
                {
                    Console.WriteLine("[+] Group: {0} is member of {1}", group.Name,groupName);
                    GetMembers(groupName, domainName);
                }
            }
        }

        public static void Main(string[] args)
        {
            string groupname = args[0];
            string domainName = args[1];

            C1 c = new C1();

            c.GetMembers(groupname, domainName);

        }
    }
}
