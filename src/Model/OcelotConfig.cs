using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocelot.Dashboard.Model
{
    public class OcelotConfig
    {
        public List<ReRoute> ReRoutes { get; set; }
        public GlobalConfiguration GlobalConfiguration { get; set; }
    }

    public class ReRoute
    {
        public bool ReRoutesCaseSensitive { get; set; }
        public string DownstreamPathTemplate { get; set; }
        public string DownstreamScheme { get; set; }

        public LoadBalancerOptions LoadBalancerOptions { get; set; }
        public string ServiceName { get; set; }

        public string[] UpstreamHttpMethod { get; set; }

        public string UpstreamPathTemplate { get; set; }
        public bool UseServiceDiscovery { get; set; }
    }

    public class LoadBalancerOptions
    {
        public string Type { get; set; }
    }
    public class GlobalConfiguration
    {
        public string RequestIdKey { get; set; }
        public ServiceDiscoveryProvider ServiceDiscoveryProvider { get; set; }

    }
    public class ServiceDiscoveryProvider
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Type { get; set; }

    }
}
