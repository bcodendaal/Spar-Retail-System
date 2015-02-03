using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.ApiBroker
{
    public class ApiBrokerBase
    {
        protected IApiBrokerConfig config;

        public ApiBrokerBase(IApiBrokerConfig config)
        {
            this.config = config;
        }

        protected T Get<T>(string method)
        {
            return ExcecuteCall<T>(x => x.GetAsync(string.Format("{0}/{1}", config.ControllerSegment, method)).Result);
        }

        protected T ExcecuteCall<T>(Func<HttpClient, HttpResponseMessage> callMethod)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(config.EndPoint);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = callMethod.Invoke(client);
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        Trace.TraceError(response.Content.ReadAsStringAsync().Result);
                        return default(T);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return default(T);
            }
        }
    }
}
