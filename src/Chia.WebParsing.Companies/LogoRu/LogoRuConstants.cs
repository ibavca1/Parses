using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.LogoRu
{
    internal static class LogoRuConstants
    {
        public const int Id = -1;

        public const string Name = "Logo.Ru";

        public const string SiteUri = "http://www.logo.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = false;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly LogoRuCity[] SupportedCities = LogoRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
new WebSiteMapPath("������������� �������","����������������"),
new WebSiteMapPath("������������ �������","�������� ��� ������������ �������"),
new WebSiteMapPath("������������ �������","������������ ���������"),
new WebSiteMapPath("������������ �������","���� ��� PC"),
new WebSiteMapPath("������������ �������","������ ������������"),
new WebSiteMapPath("������� � ��������","�������� � ����������"),
new WebSiteMapPath("����������, �����-�����","�����������"),
new WebSiteMapPath("������� ��� ����","���������� � ��������"),
new WebSiteMapPath("������� ��� ����","������� �����"),
new WebSiteMapPath("������� ��� ����","���������� �����"),
new WebSiteMapPath("������� ��� ����","����� �������������"),
new WebSiteMapPath("������� ��� ����","������������ � ������� ��� ���������"),
new WebSiteMapPath("������� ��� ����","������� ��� �����"),
new WebSiteMapPath("������� ��� ����","����"),
new WebSiteMapPath("������� ��� �����","������� �����"),
new WebSiteMapPath("������� ��� �����","�������� ��� �������� �������"),
new WebSiteMapPath("����- � �����������","������"),
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                    "�����������"
                };
    }
}