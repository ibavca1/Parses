using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.OldiRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OldiRu
{
    public class OldiRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<OldiRuWebPageType, IOldiRuWebPageContentParser> Parsers;

        static OldiRuWebPageContentParser()
        {
            Parsers =
                new IOldiRuWebPageContentParser[]
                    {
                        new OldiRuMainPageContentParser(),
                        new OldiRuRazdelPageContentParser(),
                        new OldiRuCatalogPageContentParser(),
                        new OldiRuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return OldiRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (OldiRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}