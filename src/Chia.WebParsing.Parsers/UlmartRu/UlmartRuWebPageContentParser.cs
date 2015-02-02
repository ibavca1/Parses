using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.UlmartRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.UlmartRu
{
    public class UlmartRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<UlmartRuWebPageType, IUlmartRuWebPageContentParser> Parsers;
       
        static UlmartRuWebPageContentParser()
        {
            Parsers =
                new IUlmartRuWebPageContentParser[]
                    {
                        new UlmartRuMainPageContentParser(),
                        new UlmartRuRazdelPageContentParser(),
                        new UlmartRuCatalogPageContentParser(),
                        new UlmartRuProductPageContentParser(),
                        new UlmartRuShopsListPageContentParser(),
                        new UlmartRuShopPageContentParser()
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return UlmartRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (UlmartRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}