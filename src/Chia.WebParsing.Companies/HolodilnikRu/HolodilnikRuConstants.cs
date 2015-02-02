using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.HolodilnikRu
{
    internal static class HolodilnikRuConstants
    {
        public const int Id = 610;

        public const string Name = "Holodolnik.Ru";

        public const string SiteUri = "http://www.holodilnik.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly HolodilnikRuCity[] SupportedCities = HolodilnikRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("������� ������� ��� �����", "���������� � ��������� ���������", "����������"),
new WebSiteMapPath("������� ������� ��� ����","����������"),
new WebSiteMapPath("������� ������� ��� ����","������� ��� ������������� �������"),
new WebSiteMapPath("������� ������� ��� ����","������� ��� ����� �� �������","���������� �����"),
new WebSiteMapPath("������� ������� ��� ����","������� ��� ����� �� �������","������� ��� �����"),
new WebSiteMapPath("������� ������� ��� ����","������������������"),
new WebSiteMapPath("������� ������� ��� �����","���������� � ������������� ������"),
new WebSiteMapPath("������� ������� ��� �����","����������� ����"),
new WebSiteMapPath("������� ������� ��� �����","������������ ������� �������"),
new WebSiteMapPath("������� ������� ��� �����","�������� ������������","����������"),
new WebSiteMapPath("������� ������� ��� �����","�������� ������������","���� � ���"),
new WebSiteMapPath("������� ������� ��� �����","�������� ������ � ��������������","������� ��� �������� ���������"),
new WebSiteMapPath("������� ������� ��� �����","�������� ������ � ��������������","������ ������ ��� ���-�����"),
new WebSiteMapPath("������� ������� ��� �����","�������� ������ � ��������������","���� ��������"),
new WebSiteMapPath("������� ������� ��� �����","��������� � ������� ����","������ ��� ����"),
new WebSiteMapPath("������������� � ����������  ������","����������"),
new WebSiteMapPath("����������, DVD,   �����-������������","������������� ������������ � ����������","�������� ��������"),
new WebSiteMapPath("������������ � ������������","���������� � ������������� ������ ��� �������������"),
new WebSiteMapPath("������������ � ������������","��������������"),
new WebSiteMapPath("������������ � ������������","�������� ����� (���������)"),
new WebSiteMapPath("������������ � ������������","������� ���������� � ������� ����"),
new WebSiteMapPath("����������������, ��������","������������� ������������ � ����������","�������� ������ � ������"),
new WebSiteMapPath("����������������, ��������","������������� ������������ � ����������","������� ��� �����"),
new WebSiteMapPath("����������������, ��������","������������� ������������ � ����������","����������� �����������"),
new WebSiteMapPath("����������������, ��������","������������� ������������ � ����������","�������� ��� ����� �� ��������")
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                };
    }
}