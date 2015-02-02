using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.VLazerCom;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.VLazerCom
{
    public class VLazerComWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<VLazerComWebPageType, IVLazerComWebPageContentParser> Parsers;

        static VLazerComWebPageContentParser()
        {
            Parsers =
                new IVLazerComWebPageContentParser[]
                    {
                        new VLazerComMainPageContentParser(),
                        new VLazerComCatalogPageContentParser(),
                        new VLazerComProductPageContentParser(),
                    }
                    .ToDictionary(x => x.PageType, x => x);
        }

        public override WebCompany Company
        {
            get { return VLazerComCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var pageType = (VLazerComWebPageType)page.Type;
            IWebPageContentParser parser = Parsers[pageType];
            return parser.Parse(page, content, context);
        }
    }
}