using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.DnsShopRu
{
    internal static class DnsShopRuConstants
    {
        public const int Id = 351;

        public const string Name = "DNS";

        public const string SiteUri = "http://www.dns-shop.ru";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool ProductsArticles = true;

        public const bool AvailabilityInShops = true;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly DnsShopRuCity[] SupportedCities = DnsShopRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("����������","��������","������ � ������������ ���������"),
new WebSiteMapPath("����������","��������","������������"),
new WebSiteMapPath("����������","���������","�����"),
new WebSiteMapPath("����, ������� � ������������","����"),
new WebSiteMapPath("����, ������� � ������������","����������� ������� ������� � ����������","�����"),
new WebSiteMapPath("����, ������� � ������������","���������������� �������","�������� �������"),
new WebSiteMapPath("����, ������� � ������������","���������������� �������","������"),
new WebSiteMapPath("����, ������� � ������������","���������������� �������","�����"),
new WebSiteMapPath("����, ������� � ������������","���������� ���������","�������"),
new WebSiteMapPath("����, ������� � ������������","���������� ���������","������"),
new WebSiteMapPath("����, ������� � ������������","���������� ���������","������"),
new WebSiteMapPath("������ � ������� ��� ����","����������"),
new WebSiteMapPath("������ � ������� ��� ����","������������� ������� � ��������� �����","����������� ������������"),
new WebSiteMapPath("������ � ������� ��� ����","������������� ������� � ��������� �����","����������� ����������"),
new WebSiteMapPath("������ � ������� ��� ����","������������� ������� � ��������� �����","����������������� � ������������ �����"),
new WebSiteMapPath("������������� � ��","������ � ������� ����������"),
new WebSiteMapPath("������������� � ��","�������"),
new WebSiteMapPath("������� � ��������","����������"),
new WebSiteMapPath("��������, ���������� � ����������� �����������","���������� ��� ���������","�������� ������"),
new WebSiteMapPath("��������, ���������� � ����������� �����������","���������� ��� ���������","������� ����������"),
new WebSiteMapPath("��������, ���������� � ����������� �����������","����������� �����������"),
new WebSiteMapPath("��������� � ����������","�������"),
new WebSiteMapPath("��������� � ����������","���� � ����������","�������� �� ����������"),
new WebSiteMapPath("��������� � ����������","����������","��������� � �������� �������"),
new WebSiteMapPath("��������� � ����������","����������","���� ���, ��������� ��������"),
new WebSiteMapPath("��������� � ����������","����������","�������� ��������, ������� �����-����"),
new WebSiteMapPath("��������, ����������� �����, �����������, ���������","���������� ��� ���������","�������� ������"),
new WebSiteMapPath("��������, ����������� �����, �����������, ���������","���������� ��� ���������","�������� ��� ��������� �������"),
new WebSiteMapPath("������������� � �������� ���������","����������"),
new WebSiteMapPath("��������� ���������","�������� (������������, ����)"),
new WebSiteMapPath("��������� ���������","���������","��������� ��������� ��� 3D-���������"),
new WebSiteMapPath("��������� ���������","�����������"),
new WebSiteMapPath("��������� ���������","�������� ��������"),
new WebSiteMapPath("������� ������������","����������"),
new WebSiteMapPath("������� ������������","������������� � ������������"),
new WebSiteMapPath("������� ������������","�������, �������, ������"),
new WebSiteMapPath("��������� � ������� ��������","���������� ��� ���������","��������, ����� ����"),
new WebSiteMapPath("��������� � ������� ��������","���������� ��� ���������","�������� ������"),
new WebSiteMapPath("��������� � ������� ��������","���������� ��� ���������","���������������� �������"),
new WebSiteMapPath("��������� � ������� ��������","���������� ��� ���������","����� �����"),
new WebSiteMapPath("��������� � ������� ��������","���������� ��� ���������","�������"),
new WebSiteMapPath("���� �� ������� � �����������","����������"),
new WebSiteMapPath("���� � �����������","���������� ��� �������������","������"),
new WebSiteMapPath("���� � �����������","���������� ��� �������������","�������� ������"),
new WebSiteMapPath("���� � �����������","���������� ��� �������������","�������� ������"),
new WebSiteMapPath("���� � �����������","���������� ��� �������������","������������ ��� �������"),
new WebSiteMapPath("���� � �����������","���������� ��� �������������","�����������"),
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                };
    }
}