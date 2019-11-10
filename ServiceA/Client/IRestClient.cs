using System.Threading.Tasks;

namespace ServiceA.Client
{
    public interface IRestHttpClient
    {
        Task<T> Get<T>(string url);
        Task<string> GetString(string url);
        Task<T2> Post<T1, T2>(string url, T1 request);
    }
}