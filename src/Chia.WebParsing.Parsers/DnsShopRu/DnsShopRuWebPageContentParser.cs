using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.DnsShopRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DnsShopRu
{
    public class DnsShopRuWebPageContentParser : WebCompanyPageContentParser
    {
        private static readonly Dictionary<DnsShopRuWebPageType, IDnsShopRuWebPageContentParser> Parsers;

        static DnsShopRuWebPageContentParser()
        {
            Parsers = new IDnsShopRuWebPageContentParser[]
            {
                new DnsShopRuMainPageContentParser(),
                new DnsShopRuRazdelPageContentParser(),
                new DnsShopRuCatalogPageContentParser(),
                new DnsShopRuProductPageContentParser(),
                new DnsShopRuShopsListPageContentParser()
            }
                .ToDictionary(x => x.Type, x => x);
        }

        public override WebCompany Company
        {
            get { return DnsShopRuCompany.Instance; }
        }

        public override WebPageContentParsingResult Parse(WebPage page, WebPageContent content,
            WebPageContentParsingContext context)
        {
            var type = (DnsShopRuWebPageType)page.Type;
            IDnsShopRuWebPageContentParser parser = Parsers[type];
            return parser.Parse(page, content, context);
        }
    }
}