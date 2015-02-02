using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.OnlinetradeRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OnlinetradeRu
{
    public class OnlinetradeRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<OnlinetradeRuWebPageType, IOnlinetradeRuWebPageContentParser> Parsers;

        static OnlinetradeRuWebPageContentParser()
        {
            Parsers =
                new IOnlinetradeRuWebPageContentParser[]
                    {
                        new OnlinetradeRuMainPageContentParser(),
                        new OnlinetradeRuRazdelsListPageContentParser(),
                        new OnlinetradeRuRazdelPageContentParser(),
                        new OnlinetradeRuCatalogPageContentParser(),
                        new OnlinetradeRuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return OnlinetradeRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (OnlinetradeRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}