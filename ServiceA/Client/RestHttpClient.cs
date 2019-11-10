using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceA.Client
{


    public class RestHttpClient : IRestHttpClient
    {
        protected static HttpClient httpClient = null;

        public virtual async Task<T> Get<T>(string url)
        {
            try
            {
                var client = GetHttpClient();
                var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode(); //throw the exception if the status code not OK 

                string content = await response.Content.ReadAsStringAsync();

                return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public virtual async Task<string> GetString(string url)
        {
            try
            {
                var client = GetHttpClient();
                var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode(); //throw the exception if the status code not OK 

                string content = await response.Content.ReadAsStringAsync();

                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public virtual async Task<T2> Post<T1, T2>(string url, T1 request)
        {
            try
            {
                var client = GetHttpClient();

                var requestContent = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(requestContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, httpContent);

                string content = await response.Content.ReadAsStringAsync();

                return await Task.Run(() => JsonConvert.DeserializeObject<T2>(content));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private HttpClient GetHttpClient()
        {
            if (httpClient == null)
            {
                httpClient = new HttpClient();

                // httpClient = new HttpClient(handler: new HttpClientHandler
                // {
                //     // 8888 = Fiddler standard port
                //     Proxy = new WebProxy(new Uri("http://127.0.0.1:8888")),
                //     UseProxy = true
                // });
            }

            return httpClient;
        }
    }
}