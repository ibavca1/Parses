using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TehnosilaRu
{
    internal static class TehnosilaRuConstants
    {
        public const int Id = 69;

        public const string Name = "Tehnosila.Ru";

        public const string SiteUri = "http://www.tehnosila.ru/";

        public const string UriMask = "http://{0}.tehnosila.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(3);

        public const bool SupportProductsArticle = true;

        public const bool SupportAvailabilityInShops = true;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly TehnosilaRuCity[] SupportedCities = TehnosilaRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("������������ �������", "���������� ��� ������������ �������"),
                    new WebSiteMapPath("��� ����, ���� � �������", "��� ��� �������"),
                    new WebSiteMapPath("��� ����, ���� � �������", "������ ����������"),
                    new WebSiteMapPath("��� ����, ���� � �������", "������� ������ � �������� ���������"),
                    new WebSiteMapPath("��� ����, ���� � �������", "������� ��� ����"),
                    new WebSiteMapPath("��� ����, ���� � �������", "�����������������"),
                    new WebSiteMapPath("����, ����, �����������", "���� ��� ������� ���������"),
                    new WebSiteMapPath("����, ����, �����������", "����������� �����������"),
                    new WebSiteMapPath("����������, ��������, ��������", "������ ��� ����������"),
                    new WebSiteMapPath("������� ��� ����", "������������ ����"),
                    new WebSiteMapPath("������� ��� �����", "���������� ��� �������� �������"),
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "������ ��� ������ � ������",
                    "�������������� ����",
                    "������ ��� �����",
                    "���������� �����",
                    "������� � ��������",
                    "���������������� ������",
                    "�������",
                    "������"
                };
    }
}