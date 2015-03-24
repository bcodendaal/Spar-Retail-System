using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SparRetail.ApiBroker
{
    public class ApiBrokerBase
    {
        protected readonly IApiBrokerConfig config;
        protected readonly string controllerSegment;
        protected readonly ILog logger;

        public ApiBrokerBase(IApiBrokerConfig config, string controllerSegment)
        {
            this.config = config;
            this.controllerSegment = controllerSegment;
            
            logger = LogManager.GetLogger(this.GetType());
        }

        protected T Get<T>(string method)
        {
            return ExecuteCall<T>(x => x.GetAsync(string.Format("{0}/{1}", controllerSegment, method)).Result);
        }

        protected T Get<T>(string method, Dictionary<string,string> querystringValues)
        {
            var queryString = querystringValues.Select(x => x.Key + "=" + x.Value).Aggregate((x, y) => x + "&" + y);
            return ExecuteCall<T>(x => x.GetAsync(string.Format("{0}/{1}?{2}", controllerSegment, method, queryString)).Result);
        }

        protected TOutput Post<TOutput>(string method, object inputModel)
        {
            return ExecuteCall<TOutput>(x =>
            {

                return x.PostAsync(string.Format("{0}/{1}", controllerSegment, method),
                    new StringContent(JsonConvert.SerializeObject(inputModel),
                        Encoding.UTF8, "application/json")).Result;
            });
        }

        protected T ExecuteCall<T>(Func<HttpClient, HttpResponseMessage> callMethod)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    logger.Debug("Endpoint: " + config.EndPoint);
                    client.BaseAddress = new Uri(config.EndPoint);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response =  callMethod.Invoke(client);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        logger.Debug(string.Format("Call to {0} succesful", config.EndPoint));
                        string result = response.Content.ReadAsStringAsync().Result;
                        logger.Debug("Result received:");
                        logger.Debug(result);
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                    else
                    {
                        logger.Error(response.Content.ReadAsStringAsync().Result);
                        return default(T);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return default(T);
            }
        }
    }
}
