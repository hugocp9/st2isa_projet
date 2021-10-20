using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace cryptoAPI.Interfaces
{
    public interface ISelector : IServiceProvider
    {
        Task<string> GetAssetDataAsync(HttpClient cli, string key, string currency);

        Task<string> GetTimeSeriesDataAsync(HttpClient cli, string key, string currency, int start, int end);
    }
}
