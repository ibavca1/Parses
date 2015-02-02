using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.EnterRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EnterRu
{
    public class EnterRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<EnterRuWebPageType, IEnterRuWebPageContentParser> Parsers;
       
        static EnterRuWebPageContentParser()
        {
            Parsers =
                new IEnterRuWebPageContentParser[]
                    {
                        new EnterRuMainPageContentParser(),
                        new EnterRuRazdelPageContentParser(),
                        new EnterRuCatalogPageContentParser(),
                        new EnterRuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return EnterRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (EnterRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}