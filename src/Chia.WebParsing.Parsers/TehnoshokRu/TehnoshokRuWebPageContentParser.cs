using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TehnoshokRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnoshokRu
{
    public class TehnoshokRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<TehnoshokRuWebPageType, ITehnoshokRuWebPageContentParser> Parsers;
       
        static TehnoshokRuWebPageContentParser()
        {
            Parsers =
                new ITehnoshokRuWebPageContentParser[]
                    {
                        new TehnoshokRuMainPageContentParser(),
                        new TehnoshokRuRazdelPageContentParser(),
                        new TehnoshokRuCatalogPageContentParser(),
                        new TehnoshokRuProductPageContentParser(),
                        new TehnoshokRuShopsListPageContentParser()
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return TehnoshokRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (TehnoshokRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}