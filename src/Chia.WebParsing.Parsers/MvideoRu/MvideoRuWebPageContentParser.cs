using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.MvideoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.MvideoRu
{
    public class MvideoRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<MvideoRuWebPageType, IMvideoRuWebPageContentParser> Parsers;

        static MvideoRuWebPageContentParser()
        {
            Parsers =
                new IMvideoRuWebPageContentParser[]
                    {
                        new MvideoRuMainPageContentParser(),
                        new MvideoRuRazdelsListPageContentParser(),
                        new MvideoRuRazdelPageContentParser(),
                        new MvideoRuCatalogPageContentParser(),
                        new MvideoRuProductPageContentParser(),
                        new MvideoRuShopsListPageContentParser()
                    }
                    .ToDictionary(x => x.Type, x => x);
        }

        public override WebCompany Company
        {
            get { return MvideoRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (MvideoRuWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}