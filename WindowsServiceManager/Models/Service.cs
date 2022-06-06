using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceManager.Models
{
    public class Service
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string status { get; set; }
        public string account { get; set; }

        public Service(string name, string displayName, string status, string account)
        {
            this.name = name;
            this.displayName = displayName;
            this.status = status;
            this.account = account;
        }
    }
}
