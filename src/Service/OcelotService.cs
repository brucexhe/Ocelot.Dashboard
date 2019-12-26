using Ocelot.Dashboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Ocelot.Dashboard.Service
{
    public class OcelotService
    {
        public Task<OcelotConfig> GetOcelotConfigAsync()
        {
            string json = "";
            using (var sr = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "ocelot.json"))
            {
                json = sr.ReadToEnd();
            }

            var result = JsonSerializer.Deserialize<OcelotConfig>(json);

            return Task.FromResult(result);
        }

        public Task<Result> SetOcelotConfigAsync(OcelotConfig config)
        {
            try
            {
                string json = JsonSerializer.Serialize(config);
                using (var sw = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "ocelot.json"))
                {
                    sw.Write(json);
                }

                return Task.FromResult(Result.Sucess());
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result.Fail(ex.Message));
            }

        }
    }
}
