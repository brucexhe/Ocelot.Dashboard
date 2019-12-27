using Ocelot.Dashboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text.Json;
namespace Ocelot.Dashboard.Service
{
    public class ConsulService
    {
        public Task<List<ServiceInfo>> GetServiceInfoAsync()
        {

            return Task.Run(() =>
            {

                List<ServiceInfo> result = new List<ServiceInfo>();
                var json = "";
                try
                {
                    using (var http = new System.Net.WebClient())
                    {
                        json = http.DownloadString(new Uri("http://127.0.0.1:8500/v1/agent/services"));
                    }

                    Regex reg = new Regex("\"[A-Fa-f0-9]{8}(-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12}\":");

                    json = reg.Replace(json, "");


                    json = "[" + json.Substring(json.IndexOf('{') + 1, json.LastIndexOf('}') - 1) + "]";
                    result = JsonSerializer.Deserialize<List<ServiceInfo>>(json);

                }
                catch (Exception ex)
                {
                }
                return result;
            });
        }


    }
}
