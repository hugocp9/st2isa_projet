using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using cryptoAPI.models;
using System.Collections.Generic;
using System.Linq;
using cryptoAPI.utils;

namespace cryptoAPI
{
    class Program
    {

        private static HttpClient client;
        private static Printer printer;
        private static string KEY = "r3em9s6hdcdd1tgd0g4g";

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

        static async Task<string> GetAssetDataEvoAsync(HttpClient cli, string key, string currency, int start, int end)
        {
            var data = string.Empty;

            string path = "https://api.lunarcrush.com/v2?data=assets&key=" + key + "&symbol=" + currency + "&time_series_indicators=open,close&interval=day&start=" + start + "&end=" + end;

            var response = await cli.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }

            return data;
        }

        static async Task<string> GetGlobalMetricsAsync(HttpClient cli, string key)
        {
            var data = string.Empty;

            string path = "https://api.lunarcrush.com/v2?data=global&key=" + key + "&data_points=0";

            var response = await cli.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }

            return data;
        }

        static void Main(string[] args)
        {
            client = new HttpClient();

            printer = new Printer
            {
                TableWidth = 120
            };
            

            Console.WriteLine("Crypto API Lunarcrush");
            Console.WriteLine("Welcome,");
            while (true)
            {
                
                Console.WriteLine("Type 'asset' to check the value of a cryptocurrency (in USD)");
                Console.WriteLine("Type 'global' to print an overview of the whole market");

                Console.WriteLine("Type 'exit' to close the program");

                args = Console.ReadLine().Split(' ');
                var command = args[0];
                

                switch (command)
                {
                    case "asset":
                        Console.WriteLine("Enter the symbol of the currency (BTC, LTC, ETH) :");
                        var currency = Console.ReadLine();
                        // recupération au format json
                        var jsonAsset = GetAssetDataAsync(client, KEY, currency).GetAwaiter().GetResult();

                        // Convertion en objet C#
                        var dataAsset = JObject.Parse(jsonAsset).SelectToken("data").ToObject<List<AssetData>>().First();

                        // Affichage
                        printer.Line();
                        printer.Row("Name", "Symbol", "Price");
                        printer.Line();
                        printer.Row(dataAsset.Name, dataAsset.Symbol, dataAsset.Price.ToString());
                        printer.Line();
                        Console.WriteLine();
                        printer.Line();
                        printer.Row("Pourcentage 24h", "Pourcentage 7j", "Pourcentage 30j");
                        printer.Line();
                        printer.Row(dataAsset.Percent_change_24h.ToString(), dataAsset.Percent_change_7d.ToString(), dataAsset.Percent_change_30d.ToString());
                        printer.Line();
                        printer.Row("Time", "Open", "Close", "High", "Low", "Volatility");
                        printer.Line();
                        printer.Row(dataAsset.Time.ToString(), dataAsset.Open.ToString(), dataAsset.Close.ToString(), dataAsset.High.ToString(), dataAsset.Low.ToString(), dataAsset.Volatility.ToString());
                        printer.Line();
                        Console.WriteLine();
                        printer.Line();
                        printer.Row("Twitter");
                        printer.Line();
                        printer.Row("Tweets", "Spams", "Tweet Followers", "Tweet Quotes", "Tweet Retweets","Tweet Replies", "Tweet Favorites");
                        printer.Line();
                        printer.Row(dataAsset.Tweets.ToString(), dataAsset.Tweet_spam.ToString(), dataAsset.Tweet_followers.ToString(), dataAsset.Tweet_quotes.ToString(), dataAsset.Tweet_retweets.ToString(), dataAsset.Tweet_replies.ToString(), dataAsset.Tweet_favorites.ToString());
                        printer.Line();
                        Console.WriteLine();

                        break;
                    case "global":

                        var jsonGlob = GetGlobalMetricsAsync(client, KEY).GetAwaiter().GetResult();

                        var dataGlob = JObject.Parse(jsonGlob).SelectToken("data").ToObject<List<GlobalMetrics>>().First();

                        printer.Line();
                        printer.Row("1h");
                        printer.Line();
                        printer.Row("Volume", "Market cap", "Social score", "Social volume", "Average sentiment");
                        printer.Line();
                        printer.Row(dataGlob.Volume_1h.ToString(), dataGlob.Market_cap_1h.ToString(), dataGlob.Social_score_1h.ToString(), dataGlob.Social_volume_1h.ToString(), dataGlob.Average_sentiment_1h.ToString());
                        printer.Line();
                        Console.WriteLine();
                        printer.Line();
                        printer.Row("24h");
                        printer.Line();
                        printer.Row("Volume", "Market cap", "Social score", "Social volume", "Average sentiment");
                        printer.Line();
                        printer.Row(dataGlob.Volume_24h.ToString(), dataGlob.Market_cap_24h.ToString(), dataGlob.Social_score_24h.ToString(), dataGlob.Social_volume_24h.ToString(), dataGlob.Average_sentiment_24h.ToString());
                        printer.Line();
                        Console.WriteLine();


                        break;

                    case "evolution":

                        var jsonEvo = GetAssetDataEvoAsync(client, KEY, "BTC", 1578524099, 1578726118).GetAwaiter().GetResult();
                        Console.WriteLine(jsonEvo);

                        //Console.WriteLine("Enter the symbol of the currency (BTC, LTC, ETH) :");
                        //string URL_EVOLUTION = "https://api.lunarcrush.com/v2?data=assets&key=r3em9s6hdcdd1tgd0g4g&symbol=";
                        //string SECOND_MORCEAU_URL = "&time_series_indicators=open,close";
                        //var cu = Console.ReadLine();
                        //URL_EVOLUTION += cu;
                        //URL_EVOLUTION += SECOND_MORCEAU_URL;
                        //var json_evo = GetExampleDataAsync(URL_EVOLUTION).GetAwaiter().GetResult();
                        //var data_evo = JObject.Parse(json_evo).SelectToken("data").ToObject<List<EvolutionPrice>>().First();
                        //printer.Line();
                        //printer.Row(data_evo.Name);
                        //printer.Line();
                        //printer.Row("time", "open", "close");
                        //foreach (TimeSery item in data_evo.timeSeries)
                        //{

                        //    printer.Line();
                        //    printer.Row(item.time.ToString(), item.open.ToString(), item.close.ToString());
                        //}
                        //printer.Line();
                        break;
                        
                    case "exit":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
