using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TehnoparkRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnoparkRu
{
    public class TehnoparkRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<TehnoparkRuWebPageType, ITehnoparkRuWebPageContentParser> Parsers;
       
        static TehnoparkRuWebPageContentParser()
        {
            Parsers =
                new ITehnoparkRuWebPageContentParser[]
                    {
                        new TehnoparkRuMainPageContentParser(),
                        new TehnoparkRuRazdelPageContentParser(),
                        new TehnoparkRuCatalogPageContentParser(),
                        new TehnoparkRuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return TehnoparkRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (TehnoparkRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}