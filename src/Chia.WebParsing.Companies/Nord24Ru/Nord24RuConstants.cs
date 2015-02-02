using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.Nord24Ru
{
    internal static class Nord24RuConstants
    {
        public const int Id = 74;

        public const string Name = "Nord24.Ru";

        public const string SiteUri = "http://www.nord24.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = false;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly Nord24RuCity[] SupportedCities = Nord24RuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
new WebSiteMapPath("������������� �������","����������������"),
new WebSiteMapPath("������������� �������","����������"),
new WebSiteMapPath("������������ �������","�������� ��� ������������ �������"),
new WebSiteMapPath("������������ �������","������������ ���������"),
new WebSiteMapPath("������������ �������","������ ������������"),
new WebSiteMapPath("����������, �����-�����","�����������"),
new WebSiteMapPath("������� ��� ����","���������� � ��������"),
new WebSiteMapPath("������� ��� ����","������� �����"),
new WebSiteMapPath("������� ��� ����","���������� �����"),
new WebSiteMapPath("������� ��� ����","����� �������������"),
new WebSiteMapPath("������� ��� ����","������������ � ������� ��� ���������"),
new WebSiteMapPath("������� ��� �����","�������� ��� �������� �������"),
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                    "�����������"
                };
    }
}