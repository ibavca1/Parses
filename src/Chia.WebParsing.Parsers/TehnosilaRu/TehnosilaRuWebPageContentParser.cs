using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TehnosilaRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnosilaRu
{
    public class TehnosilaRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<TehnosilaRuWebPageType, ITehnosilaRuWebPageContentParser> Parsers;
       
        static TehnosilaRuWebPageContentParser()
        {
            Parsers =
                new ITehnosilaRuWebPageContentParser[]
                    {
                        new TehnosilaRuMainPageContentParser(),
                        new TehnosilaRuRazdelPageContentParser(),
                        new TehnosilaRuCatalogPageContentParser(),
                        new TehnosilaRuProductPageContentParser(),
                        new TehnosilaRuShopsListPageContentParser()
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return TehnosilaRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (TehnosilaRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}