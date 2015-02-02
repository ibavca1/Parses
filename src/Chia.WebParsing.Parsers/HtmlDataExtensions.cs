using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers
{
    //[DebuggerNonUserCode, DebuggerStepThrough]
    internal static class HtmlDataExtensions
    {
        public static Uri GetUri(this HtmlNode node, WebPage page)
        {
           return GetUri(node, page, "href");
        }

        public static Uri GetUri(this HtmlNode node, WebPage page, string attributeName)
        {
            string href = node.Attributes[attributeName].Value;
            href = HttpUtility.HtmlDecode(href).Replace('\n', '?');
            return page.GetUri(href);
        }

        public static string GetInnerText(this HtmlNode node, string def = null)
        {
            if (node == null)
                return def;

            string value = node.InnerText.Trim();
            value = HttpUtility.HtmlDecode(value);
            value = value.Replace("\n", "").Replace("\t", "");
            return value.Trim();
        }

        public static string GetAttributeText(this HtmlNode node, string attributeName, string def = null)
        {
            if (node == null || !node.Attributes.Contains(attributeName))
                return def;

            string value = node.Attributes[attributeName].Value.Trim();
            value = HttpUtility.HtmlDecode(value);
            return value.Trim();
        }

        public static string GetDigitsText(this HtmlNode node, string def = null)
        {
            if (node == null)
                return def;

            string value = node.GetInnerText().RemoveNonDigitChars();
            value = HttpUtility.HtmlDecode(value);
            return value.Trim();
        }

        public static decimal GetPrice(this HtmlNode node, decimal def = 0)
        {
            if (node == null)
                return def;

            string priceStringValue = node.GetInnerText();
            var builder = new StringBuilder();
            foreach (char ch in priceStringValue)
            {
                if (char.IsDigit(ch))
                    builder.Append(ch);
                if (ch == ',' || ch == '.')
                    builder.Append('.');
            }

            priceStringValue = builder.ToString();
            if (priceStringValue.EndsWith("."))
                priceStringValue = priceStringValue.Replace(".", "");
            decimal value;
            decimal.TryParse(priceStringValue, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
            return value;
        }

        public static decimal GetPrice(this HtmlNode node, string attribute, decimal def = 0)
        {
            if (node == null)
                return def;

            string priceStringValue = node.GetAttributeText(attribute);
            var builder = new StringBuilder();
            foreach (char ch in priceStringValue)
            {
                if (char.IsDigit(ch))
                    builder.Append(ch);
                if (ch == ',' || ch == '.')
                    builder.Append('.');
            }

            priceStringValue = builder.ToString();
            if (priceStringValue.EndsWith("."))
                priceStringValue = priceStringValue.Replace(".", "");
            decimal value;
            decimal.TryParse(priceStringValue, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
            return value;
        }
    }
}