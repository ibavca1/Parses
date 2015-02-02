using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.EurosetRu
{
    internal static class EurosetRuConstants
    {
        public const int Id = 40;

        public const string Name = "Euroset.Ru";

        public const string SiteUri = "http://euroset.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(10);

        public const bool SupportArticles = false;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly EurosetRuCity[] SupportedCities = EurosetRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
new WebSiteMapPath("��������������","����������","��� ������������� ����"),
new WebSiteMapPath("��������������","����������","��� ������������� ���"),
new WebSiteMapPath("��������������","����������","��� ���������"),
new WebSiteMapPath("��������������","����������","����� ��� ���������� �����"),
new WebSiteMapPath("��������������","���������� ��� ������� �������"),
new WebSiteMapPath("��������������","���������� � �������� �������"),
new WebSiteMapPath("������ �������","�����"),
new WebSiteMapPath("������ �������","�������������"),
new WebSiteMapPath("������ �������","�������������","������� � ���������"),
new WebSiteMapPath("������ �������","���������� �����������"),
new WebSiteMapPath("������ �������", "���������� ����������� � ����� ������"),
new WebSiteMapPath("����������������","����������","�������"),
new WebSiteMapPath("����������������","����������","��������� ��� ���������"),
new WebSiteMapPath("����������������","����������","�������� ��� �����"),
new WebSiteMapPath("����������������","���������"),
new WebSiteMapPath("��������","Luxury","�������"),
new WebSiteMapPath("��������","�������� ����� � Sim-�����"),
new WebSiteMapPath("��������","����������","�������� �����"),
new WebSiteMapPath("��������","����������","��������"),
new WebSiteMapPath("��������","����������","��������"),
new WebSiteMapPath("��������","����������","���������� ��������"),
new WebSiteMapPath("��������","����������","�������"),
new WebSiteMapPath("��������","���������"),
new WebSiteMapPath("��������� ����","�������� ������","���������� ��� �����"),
new WebSiteMapPath("��������� ����","�������� ������","����� �����������"),
new WebSiteMapPath("��������� ����","�������� ������","������� ��� �����"),
new WebSiteMapPath("��������� ����","�������� ������","��������, ��������, ���������"),
new WebSiteMapPath("��������� ����","���������","�����������"),
new WebSiteMapPath("��������� ����","���������","����������������� �����"),
new WebSiteMapPath("��������� ����","������","����������"),
new WebSiteMapPath("��������� ����","������","�������� �������"),
new WebSiteMapPath("��������� ����","������������������"),
new WebSiteMapPath("�����������","��� ����","����������","�������� ������"),
new WebSiteMapPath("�����������","���� � �����������","����������","�������� ������"),
new WebSiteMapPath("�����������","���� � �����������","������ �����"),
new WebSiteMapPath("�����������","���� � �����������","������ ��� ������"),
new WebSiteMapPath("�����������","���� � �����������","������"),
new WebSiteMapPath("�����������","���� � �����������","����������� ����"),
new WebSiteMapPath("�����������","����������� �������","����"),
new WebSiteMapPath("�����������","���� � �����","����������","��������� ��� ������/�������"),
new WebSiteMapPath("�����������","���� � �����","����������","�������"),

                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                    "��������� �����"
                };
    }
}