using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.JustRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.JustRu
{
    public class JustRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<JustRuWebPageType, IJustRuWebPageContentParser> Parsers;

        static JustRuWebPageContentParser()
        {
            Parsers =
                new IJustRuWebPageContentParser[]
                    {
                        new JustRuMainPageContentParser(),
                        new JustRuCatalogPageContentParser(),
                        new JustRuProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return JustRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (JustRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}