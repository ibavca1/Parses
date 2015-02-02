using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.JustRu
{
    internal static class JustRuConstants
    {
        public const int Id = 617;

        public const string Name = "Just.Ru";

        public const string SiteUri = "http://www.just.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly WebCity[] SupportedCities = JustRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("Apple", "���������� ��� Apple"),
                    new WebSiteMapPath("Apple", "��������� ��� Mac"),
                    new WebSiteMapPath("Apple", "������ Apple"),
                    new WebSiteMapPath("����", "����������"),
                    new WebSiteMapPath("����", "����������������"),
                    new WebSiteMapPath("����", "�����"),
                    new WebSiteMapPath("����", "�����������"),
                    new WebSiteMapPath("����", "������������� ������c���"),
                    new WebSiteMapPath("����", "����"),
                    new WebSiteMapPath("������", "�����������"),
                    new WebSiteMapPath("������", "����������"),
                    new WebSiteMapPath("����", "Nintendo"),
                    new WebSiteMapPath("����", "PlayStation 3"),
                    new WebSiteMapPath("����", "PSP � Vita"),
                    new WebSiteMapPath("����", "Xbox 360"),
                    new WebSiteMapPath("����", "��������� �� �� Android � 32-bit"),
                    new WebSiteMapPath("����", "���������� ������������"),
                    new WebSiteMapPath("����", "������ � ������"),
                    new WebSiteMapPath("����", "������, ������, �����"),
                    new WebSiteMapPath("����", "����������"),
                    new WebSiteMapPath("����� �����", "����������� �����������")
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "JUST.������"
                };
    }
}