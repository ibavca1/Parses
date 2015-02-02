using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TelemaksRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TelemaksRu
{
    public class TelemaksRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<TelemaksRuWebPageType, ITelemaksRuWebPageContentParser> Parsers;
       
        static TelemaksRuWebPageContentParser()
        {
            Parsers =
                new ITelemaksRuWebPageContentParser[]
                    {
                        new TelemaksRuMainPageContentParser(),
                        new TelemaksRuRazdelPageContentParser(),
                        new TelemaksRuCatalogPageContentParser(),
                        new TelemaksRuProductPageContentParser(),
                        new TelemaksRuShopsListPageContentParser()
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return TelemaksRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (TelemaksRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}