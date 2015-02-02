using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.VLazerCom
{
    internal static class VLazerComConstants
    {
        public const int Id = 196;

        public const string Name = "V-Lazer.Ru";

        public const string SiteUri = "http://shop.v-lazer.com";

        public const string SiteUriMask = "http://{0}.shop.v-lazer.com";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = true;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly WebCity[] SupportedCities = VLazerComCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("������������ �������","C����","����� ��� ������"),
new WebSiteMapPath("������������ �������","����������� �����������"),
new WebSiteMapPath("������������ �������","���������� ������ � ��������","USB �������������"),
new WebSiteMapPath("������","�������� ��������������"),
new WebSiteMapPath("������","������ �������� �����������"),
new WebSiteMapPath("������","������ �����"),
new WebSiteMapPath("������","������ ������"),
new WebSiteMapPath("������","�������� �������"),
new WebSiteMapPath("������","������ ��� ������"),
new WebSiteMapPath("������","������ ��� ������","�������"),
new WebSiteMapPath("������","����� ��� �������, ���������"),
new WebSiteMapPath("����������, �����, �����","����������","�������� ��� �����"),
new WebSiteMapPath("������� ��� ����","����� � �����������","���������� �����"),
new WebSiteMapPath("������� ��� ����","����� � �����������","�����������"),
new WebSiteMapPath("������� ��� ����","��������","����������"),
new WebSiteMapPath("������� ��� ����","������� ��� ������","���������� �����"),
new WebSiteMapPath("������� ��� �����","������������� ����","�������"),
new WebSiteMapPath("������� ��� �����","�������� ��� �����"),
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                };
    }
}