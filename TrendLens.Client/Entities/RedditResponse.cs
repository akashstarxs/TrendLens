namespace TrendLens.Client.Entities
{
    public class RedditResponse
    {

        public string kind { get; set; }
        public RedditData data { get; set; }

    }

    public class RedditData
    {
        public string modhash { get; set; }
        public int dist { get; set; }
        public RedditChild[] children { get; set; }
        public string after { get; set; }
        public string before { get; set; }
    }

    public class RedditChild
    {
        public string kind { get; set; }
        //created_utc
        public int created_utc { get; set; }
    }
}
