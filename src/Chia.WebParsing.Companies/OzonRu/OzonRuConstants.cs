using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.OzonRu
{
    internal static class OzonRuConstants
    {
        public const int Id = 604;

        public const string Name = "Ozon.Ru";

        public const string SiteUri = "http://www.ozon.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = false;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly WebCity[] SupportedCities = WebCities.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
new WebSiteMapPath("������� �������", "��������� � �������������"),
new WebSiteMapPath("������� �������","�����������������"), 
new WebSiteMapPath("������� �������","��� ������� � ��������","������ ����� � ����������","������� ��� ������ ����� � �����������"),
new WebSiteMapPath("������� �������","�������� �������","��������� � ���������","���������� ��� ��������� � ���������"),
new WebSiteMapPath("������� �������","�������� �������","��������� � ���������","������������ ������"),
new WebSiteMapPath("������� �������","�������� �������","��������� � ���������","������� ��� ���������"),
new WebSiteMapPath("������� �������","������� ��� ����","������������ � ����������","��������"),
new WebSiteMapPath("������� �������","������� ��� ����","������������ � ����������","�������� ������������ ��� ���� � ����"),
new WebSiteMapPath("�����������","����������","������ � �����������","USB-���� (������������)"),
new WebSiteMapPath("�����������","����������","������ � �����������","����-������"),
new WebSiteMapPath("�����������","����������","������ � �����������","������"),
new WebSiteMapPath("�����������","����������","������ � �����������","�����������"),
new WebSiteMapPath("�����������","����������","����������, ����, ��������� � ����","������� ��� ����"),
new WebSiteMapPath("�����������","����������","����","���� ��� ����������"),
new WebSiteMapPath("�����������","������������ � ���������������","GPS - �������"),
new WebSiteMapPath("�����������","������������ � ���������������","��������"),
new WebSiteMapPath("�����������","������������ � ���������������","������"),
new WebSiteMapPath("�����������","������������ � ���������������","�������� ������������ ��� ���� � ����"),
new WebSiteMapPath("�����������","������� ���������","Comfy","���� ��� Easy PC"),
new WebSiteMapPath("�����������","������� ���������","Nintendo Wii","��������� ��������"),
new WebSiteMapPath("�����������","������� ���������","Sony PlayStation 2","��������� ��������"),
new WebSiteMapPath("�����������","������� ���������","Sony PlayStation 3","��������� ��������"),
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "���������� �����������",
                    "����� � �����",
                    "�����",
                    "���� � ����",
                    "DVD � Blu-ray",
                    "������",
                    "���, ���, ���������",
                    "����� � �����",
                    "������� � ��������",
                    "������, �����, ����������",
                    "�������������, ����, �����",
                    "�����������, ������, ���������",
                    "���������� � �/� ������",
                    "OZON.digital",
                    "LUXE",
                    "��������� �����������"
                };
    }
}