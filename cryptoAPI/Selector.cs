using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace cryptoAPI
{
    public class Selector
    {

        

        public Selector()
        {

        }

        static async Task<string> GetAssetDataAsync(HttpClient cli, string key, string currency)
        {
            var data = string.Empty;

            string path = "https://api.lunarcrush.com/v2?data=assets&key=" + key + "&symbol=" + currency; 

            var response = await cli.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }

            return data;
        }
        /*
        async Task<string> GetTimeSeriesDataAsync(HttpClient cli, string key, string currency, int start, int end)
        {
            //
        }
        */
    }
}
