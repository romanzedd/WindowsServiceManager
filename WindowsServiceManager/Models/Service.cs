using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceManager.Models
{
    public class Service
    {
        internal string name { get; set; }
        internal string displayName { get; set; }
        internal string status { get; set; }
        internal string account { get; set; }

        public Service(string name, string displayName, string status, string account)
        {
            this.name = name;
            this.displayName = displayName;
            this.status = status;
            this.account = account;
        }
    }
}
