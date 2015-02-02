using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TdPoiskRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TdPoiskRu
{
    public class TdPoiskRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<TdPoiskRuWebPageType, ITdPoiskRuWebPageContentParser> Parsers;
       
        static TdPoiskRuWebPageContentParser()
        {
            Parsers =
                new ITdPoiskRuWebPageContentParser[]
                    {
                        new TdPoiskRuMainPageContentParser(),
                        new TdPoiskRuRazdelPageContentParser(),
                        new TdPoiskRuCatalogPageContentParser(),
                        new TdPoiskRuProductPageContentParser(),
                        new TdPoiskRuShopsListPageContentParser()
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return TdPoiskRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (TdPoiskRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}