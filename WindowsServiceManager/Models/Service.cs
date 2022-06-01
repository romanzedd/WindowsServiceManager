using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceManager.Models
{
    public class Service
    {
        string name { get; set; }
        string displayName { get; set; }
        string status { get; set; }
        string account { get; set; }

        public Service(string name, string displayName, string status, string account)
        {
            this.name = name;
            this.displayName = displayName;
            this.status = status;
            this.account = account;
        }
    }
}
