using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.UtinetRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.UtinetRu
{
    public class UtinetRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<UtinetRuWebPageType, IUtinetRuWebPageContentParser> Parsers;
       
        static UtinetRuWebPageContentParser()
        {
            Parsers =
                new IUtinetRuWebPageContentParser[]
                    {
                        new UtinetRuMainPageContentParser(),
                        new UtinetRuRazdelPageContentParser(),
                        new UtinetRuCatalogPageContentParser(),
                        new UtinetRuProductPageContentParser()
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return UtinetRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (UtinetRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}