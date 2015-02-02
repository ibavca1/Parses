using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TechnonetRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TechnonetRu
{
    public class TechnonetRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<TechnonetRuWebPageType, ITechnonetRuWebPageContentParser> Parsers;
       
        static TechnonetRuWebPageContentParser()
        {
            Parsers =
                new ITechnonetRuWebPageContentParser[]
                    {
                        new TechnonetRuMainPageContentParser(),
                        new TechnonetRuRazdelPageContentParser(),
                        new TechnonetRuCatalogPageContentParser(),
                        new TechnonetRuProductPageContentParser(),
                        new TechnonetRuShopsListPageContentParser()
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return TechnonetRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (TechnonetRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}