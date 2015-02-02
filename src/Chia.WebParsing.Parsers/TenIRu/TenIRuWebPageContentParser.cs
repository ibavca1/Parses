using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TenIRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TenIRu
{
    public class TenIRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<TenIRuWebPageType, ITenIRuWebPageContentParser> Parsers;

        static TenIRuWebPageContentParser()
        {
            Parsers =
                new ITenIRuWebPageContentParser[]
                    {
                        new TenIRuMainPageContentParser(),
                        new TenIRuRazdelPageContentParser(),
                        new TenIRuCatalogPageContentParser(),
                        new TenIRuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return TenIRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (TenIRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}