using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.Nord24Ru;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Nord24Ru
{
    public class Nord24RuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<Nord24RuWebPageType, INord24RuWebPageContentParser> Parsers;
       
        static Nord24RuWebPageContentParser()
        {
            Parsers =
                new INord24RuWebPageContentParser[]
                    {
                        new Nord24RuMainPageContentParser(),
                        new Nord24RuRazdelPageContentParser(),
                        new Nord24RuCatalogPageContentParser(),
                        new Nord24RuProductPageContentParser()
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return Nord24RuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (Nord24RuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}