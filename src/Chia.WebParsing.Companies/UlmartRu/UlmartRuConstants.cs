using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.UlmartRu
{
    internal static class UlmartRuConstants
    {
        public const int Id = 534;

        public const string Name = "Ulmart.Ru";

        public const string SiteUri = "http://www.ulmart.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportAvailabilityInShops = true;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly UlmartRuCity[] SupportedCities = UlmartRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            {
new WebSiteMapPath("����������","�������� ��� ������������� ����������"),
new WebSiteMapPath("����������","�������� ��� ��"),
new WebSiteMapPath("����������","��������� ��� ���"),
new WebSiteMapPath("����������","��������������"),
new WebSiteMapPath("����������","��������������"),
new WebSiteMapPath("����������","��������������"),
new WebSiteMapPath("����������","�������������� �������"),
new WebSiteMapPath("����������","���������, ����� �� �����, �������������"),
new WebSiteMapPath("����������","������� ��������� � ������"),
new WebSiteMapPath("����������","�������� ��� ������������ ������������"),
new WebSiteMapPath("����������","�����, ������������� � ���������"),
new WebSiteMapPath("����������","����������"),
new WebSiteMapPath("����������","����, �����"),
new WebSiteMapPath("����������","������������ � ������"),
new WebSiteMapPath("������� �������","���������� ��� ������� �������"),
new WebSiteMapPath("������� �������","������� �����"),
new WebSiteMapPath("���, ���� � ������","����������"),
new WebSiteMapPath("���, ���� � ������","������ �������"),
new WebSiteMapPath("���, ���� � ������","���� � ���"),
new WebSiteMapPath("���, ���� � ������","�������� ��������"),
new WebSiteMapPath("���, ���� � ������","���������� ����������"),
new WebSiteMapPath("���, ���� � ������","����������"),
new WebSiteMapPath("���, ���� � ������","��������"),
new WebSiteMapPath("���, ���� � ������","�����"),
new WebSiteMapPath("���, ���� � ������","��������� ��������"),
new WebSiteMapPath("���, ���� � ������","���������"),
new WebSiteMapPath("���, ���� � ������","��������� � ����������"),
new WebSiteMapPath("���, ���� � ������","������ ����"),
new WebSiteMapPath("���, ���� � ������","������ ��� ��������"),
new WebSiteMapPath("���, ���� � ������","������������� ������"),
new WebSiteMapPath("���, ���� � ������","��������� � ������"),
new WebSiteMapPath("���, ���� � ������","������� �����"),
new WebSiteMapPath("���, ���� � ������","������� ���������"),
new WebSiteMapPath("���, ���� � ������","����� ���"),
new WebSiteMapPath("���, ���� � ������","�����"),
new WebSiteMapPath("���, ���� � ������","�������������"),
new WebSiteMapPath("�������������","������, ������ � �����������"),
new WebSiteMapPath("�������������","��������� ������������"),
new WebSiteMapPath("�������������","����������� �����������"),
new WebSiteMapPath("���������� � ��������","����������� �����������"),
new WebSiteMapPath("���� � ����","���������� ������������"),
new WebSiteMapPath("���� � ����","�������� ������������"), 
new WebSiteMapPath("���� � ����","����������� �����������"), 
new WebSiteMapPath("��, �����, �����","DJ ������������"),
new WebSiteMapPath("���� � �����������","��������� ������������"),
new WebSiteMapPath("�������� ����� � ������"), 
new WebSiteMapPath("������ ��� ��������"),
new WebSiteMapPath("���������� � ���������"),  
new WebSiteMapPath("���������� ������� � ������"), 
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "�-Gift � ���� ��������",
                    "������������",
                    "���������� � ���������",
                    "������ ��� ��������",
                    "������",
                    "������ � ������ ����",
                    "������� ������",
                    "������, �����, ����",
                    "�������",
                    "����������",
                    "������",
                    "���������� �����������",
                    "���������� ��������",
                    "������� � �������",
                    "������ �����������",
                    "���������",
                    "������ �������� ������"
                };
    }
}