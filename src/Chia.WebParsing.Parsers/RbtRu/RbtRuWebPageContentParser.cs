using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.RbtRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.RbtRu
{
    public class RbtRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<RbtRuWebPageType, IRbtRuWebPageContentParser> Parsers;
       
        static RbtRuWebPageContentParser()
        {
            Parsers =
                new IRbtRuWebPageContentParser[]
                    {
                        new RbtRuMainPageContentParser(),
                        new RbtRuRazdelPageContentParser(),
                        new RbtRuCatalogPageContentParser(),
                        new RbtRuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return RbtRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (RbtRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            WebPageContentParsingResult result = parser.Parse(page, content, context);
            return result;
        }
    }
}