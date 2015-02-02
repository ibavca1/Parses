using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.NewmansRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.NewmansRu
{
    public class NewmansRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<NewmansRuWebPageType, INewmansRuWebPageContentParser> Parsers;

        static NewmansRuWebPageContentParser()
        {
            Parsers =
                new INewmansRuWebPageContentParser[]
                    {
                        new NewmansRuMainPageContentParser(),
                        new NewmansRuRazdelPageContentParser(),
                        new NewmansRuCatalogPageContentParser(),
                        new NewmansRuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return NewmansRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (NewmansRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}