using System;

namespace cryptoAPI.models
{
    public class AssetData
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public double Percent_change_24h { get; set; }
        public double Percent_change_7d { get; set; }
        public double Percent_change_30d { get; set; }
        public int Time { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volatility { get; set; }
        public int Tweets { get; set; }
        public int Tweet_spam { get; set; }
        public int Tweet_followers { get; set; }
        public int Tweet_quotes { get; set; }
        public int Tweet_retweets { get; set; }
        public int Tweet_replies { get; set; }
        public int Tweet_favorites { get; set; }

    }

}
