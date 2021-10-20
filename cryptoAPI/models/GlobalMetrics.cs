using System;
namespace cryptoAPI.models
{
    public class GlobalMetrics
    {
        public long Volume_1h { get; set; }
        public long Market_cap_1h { get; set; }
        public int Social_score_1h { get; set; }
        public int Social_volume_1h { get; set; }
        public double Average_sentiment_1h { get; set; }
        public long Volume_24h { get; set; }
        public long Market_cap_24h { get; set; }
        public long Social_score_24h { get; set; }
        public int Social_volume_24h { get; set; }
        public double Average_sentiment_24h { get; set; }
    }
}
