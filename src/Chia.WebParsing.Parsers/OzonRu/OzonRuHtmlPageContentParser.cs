using System.Globalization;
using HtmlAgilityPack;

namespace Chia.WebParsing.Parsers.OzonRu
{
    internal abstract class OzonRuHtmlPageContentParser: HtmlPageContentParser
    {
        protected static decimal ParsePrice(HtmlNode node)
        {
            HtmlNode priceNode =
                node.SelectSingleNode(@".//span[contains(@class,'eOzonPrice_main')]");
            HtmlNode priceCopecsNode =
                node.SelectSingleNode(@".//span[contains(@class,'eOzonPrice_submain')]");
            if (priceNode == null || priceCopecsNode == null)
                return 0;

            string priceString = priceNode.GetDigitsText();
            string priceCopecsString = priceCopecsNode.GetDigitsText();
            decimal price = decimal.Parse(priceString, NumberStyles.Number);
            decimal copecs = decimal.Parse(priceCopecsString, NumberStyles.Number);
            price = price + copecs / 100.0m;
            return price;
        }
    }
}