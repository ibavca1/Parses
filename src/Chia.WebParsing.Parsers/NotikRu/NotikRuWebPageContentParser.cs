using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.NotikRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.NotikRu
{
    public class NotikRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<NotikRuWebPageType, INotikRuWebPageContentParser> Parsers;

        static NotikRuWebPageContentParser()
        {
            Parsers =
                new INotikRuWebPageContentParser[]
                    {
                        new NotikRuMainPageContentParser(),
                        new NotikRuRazdelPageContentParser(),
                        new NotikRuCatalogPageContentParser(),
                        new NotikRuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return NotikRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (NotikRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}