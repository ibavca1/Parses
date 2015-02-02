using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.CorpCentreRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CorpCentreRu
{
    public class CorpCentreRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<CorpCentreRuWebPageType, ICorpCentreRuWebPageContentParser> Parsers;
       
        static CorpCentreRuWebPageContentParser()
        {
            Parsers =
                new ICorpCentreRuWebPageContentParser[]
                    {
                        new CorpCentreRuMainPageContentParser(),
                        new CorpCentreRuRazdelsListPageContentParser(),
                        new CorpCentreRuRazdelPageContentParser(),
                        new CorpCentreRuCatalogPageContentParser(),
                        new CorpCentreRuProductPageContentParser(),
                        new CorpCentreRuShopsListPageContentParser()
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return CorpCentreRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (CorpCentreRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}