using System;

namespace WebParsingFramework
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class WebSiteParsingOptions
    {
        public bool ProductArticle { get; set; }

        public bool AvailabiltyInShops { get; set; }

        public bool InformationAboutShops { get; set; }

        public WebPriceType PriceTypes { get; set; }

        public WebPagesFilter PagesFilter { get; set; }

        public string forClient { get; set; }
    }
}