using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.RegardRu
{
    internal static class RegardRuConstants
    {
        public const int Id = 420;

        public const string Name = "Regard.Ru";

        public const string SiteUri = "http://www.regard.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(2);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly RegardRuCity[] SupportedCities = RegardRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("���������� ��� ��������� ���������", "�������� �����"),
                    new WebSiteMapPath("���������� ��� ��������� ���������", "�������"),
                    new WebSiteMapPath("������������ ��� �����������", "����������"),
                    new WebSiteMapPath("������������ ��� �����������", "����� ��� ����������"),
                    new WebSiteMapPath("������� �������", "������������ (������������)"),
                    new WebSiteMapPath("������� �������", "�������� ��������"),
                    new WebSiteMapPath("������� �������", "������"),
                    new WebSiteMapPath("������� �������", "������������ ����� (�������)"),
                    new WebSiteMapPath("������ Flash", "����������"),
                    new WebSiteMapPath("��������", "CANON"),
                    new WebSiteMapPath("��������", "EPSON"),
                    new WebSiteMapPath("��������", "HP"),
                    new WebSiteMapPath("��������", "���������� � ���������")
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                         "����������� �����������",
                         "�������",
                         "��������� �����"
                };
    }
}