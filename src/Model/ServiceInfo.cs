using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocelot.Dashboard.Model
{
    public class ServiceInfo
    {
        public string ID { get; set; }
        public string Service { get; set; }
        public string[] Tags { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }


    }
}
