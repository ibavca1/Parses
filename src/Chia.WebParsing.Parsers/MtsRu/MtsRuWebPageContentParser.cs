using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.MtsRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.MtsRu
{
    public class MtsRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<MtsRuWebPageType, IMtsRuWebPageContentParser> Parsers;

        static MtsRuWebPageContentParser()
        {
            Parsers =
                new IMtsRuWebPageContentParser[]
                    {
                        new MtsRuMainPageContentParser(),
                        new MtsRuRazdelPageContentParser(),
                        new MtsRuCatalogPageContentParser(),
                        new MtsRuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return MtsRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (MtsRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}