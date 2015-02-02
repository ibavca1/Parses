using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TechportRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TechportRu
{
    public class TechportRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<TechportRuWebPageType, ITechportRuWebPageContentParser> Parsers;

        static TechportRuWebPageContentParser()
        {
            Parsers =
                new ITechportRuWebPageContentParser[]
                    {
                        new TechportRuMainPageContentParser(),
                        new TechportRuCatalogPageContentParser(),
                        new TechportRuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return TechportRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (TechportRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}