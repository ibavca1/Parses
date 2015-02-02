using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.IonRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.IonRu
{
    public class IonRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<IonRuWebPageType, IIonRuWebPageContentParser> Parsers;

        static IonRuWebPageContentParser()
        {
            Parsers =
                new IIonRuWebPageContentParser[]
                    {
                        new IonRuMainPageContentParser(),
                        new IonRuCatalogPageContentParser(),
                        new IonRuProductPageContentParser(),
                        new IonRuShopsListPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return IonRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (IonRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}